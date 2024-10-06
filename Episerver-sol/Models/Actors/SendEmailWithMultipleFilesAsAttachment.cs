using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Internal;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.Forms.Core.PostSubmissionActor;
using EPiServer.Forms.EditView;
using EPiServer.Forms.EditView.SpecializedProperties;
using EPiServer.Forms.Helpers.Internal;
using EPiServer.Forms.Implementation.Actors;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using EPiServer.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Http.Extensions;

namespace EpiserverBH.Models.Actors
{
    /*This is a custom actor to send emails with uploaded files as attachments rather than links
       Most code here (except SendEmailWithAttachments) is copied from episerver forms original SendEmailAfterSubmissionActor.
       This Actor can handle the multiple file element included in the project*/
    
    [ContentType(GUID = "{101E29E9-78E4-4B17-A568-91020324327B}",
        DisplayName = "Send Email After Submission(With Multiple Attachments)",
        Description = "Send Email After Submission(With Multiple Attachments)",
        GroupName = "Form Elements")]
    public class SendEmailWithMultipleFilesAsAttachment : SendEmailAfterSubmissionActor, IUIPropertyCustomCollection
    {
        private static readonly IHttpContextAccessor _IHttpContextAccessor;
        private static SmtpClient _smtpClient = new SmtpClient();
        private bool _sendMessageInHTMLFormat = false;
        private readonly PlaceHolderService _placeHolderService = new PlaceHolderService();
        private readonly Injected<IFormRepository> _formRepository;
        private readonly Injected<IFormDataRepository> _formDataRepository;

        public override object Run(object input)
        {
            IEnumerable<EmailTemplateActorModel> model = this.Model as IEnumerable<EmailTemplateActorModel>;
            if (model == null || model.Count<EmailTemplateActorModel>() < 1)
                return (object) null;            

            foreach (EmailTemplateActorModel emailConfig in model)
            {
                var files = new List<UploadedFile>();
                files = GetUploadedFiles();
                this.SendMessage(emailConfig, files);
            }
                
            return (object) null;
        }

        public virtual Type PropertyType
        {
            get { return typeof(PropertyEmailTemplateActorList); }
        }

        public SendEmailWithMultipleFilesAsAttachment()
        {
            this._sendMessageInHTMLFormat = this._formConfig.Service.SendMessageInHTMLFormat;
        }

        private void SendMessage(EmailTemplateActorModel emailConfig, List<UploadedFile> files)
        {
            if (string.IsNullOrEmpty(emailConfig.ToEmails))
                return;

            string[] strArray = emailConfig.ToEmails.Split(",");

            if (strArray == null || ((IEnumerable<string>) strArray).Count<string>() == 0)
                return;

            try
            {
                IEnumerable<FriendlyNameInfo> friendlyNameInfos = this._formRepository.Service.GetFriendlyNameInfos(this.FormIdentity, typeof(IExcludeInSubmission));
                IEnumerable<PlaceHolder> subjectPlaceHolders = this.GetSubjectPlaceHoldersCustom(friendlyNameInfos);
                IEnumerable<PlaceHolder> bodyPlaceHolders = this.GetBodyPlaceHoldersCustom(friendlyNameInfos);
                PlaceHolderService placeHolderService = this._placeHolderService;
                string str = _placeHolderService.Replace(emailConfig.Subject, subjectPlaceHolders, false);
                var body = emailConfig.Body != null ? emailConfig.Body.ToHtmlString() : string.Empty;
                string content = _placeHolderService.Replace(body, bodyPlaceHolders, false);
                MailMessage message = new MailMessage();
                message.Subject = str;
                message.Body = this.RewriteUrls(content);
                message.IsBodyHtml = this._sendMessageInHTMLFormat;

                if (!string.IsNullOrEmpty(emailConfig.FromEmail))
                {
                    MailMessage mailMessage = message;
                    placeHolderService = this._placeHolderService;
                    MailAddress mailAddress = new MailAddress(placeHolderService.Replace(emailConfig.FromEmail, subjectPlaceHolders, false));
                    mailMessage.From = mailAddress;
                }

                foreach (string template in strArray)
                {
                    try
                    {
                        MailAddressCollection to = message.To;
                        placeHolderService = this._placeHolderService;
                        MailAddress mailAddress = new MailAddress(placeHolderService.Replace(template, bodyPlaceHolders, false));
                        to.Add(mailAddress);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                bool sendEmailsWithAttachments = SendEmailWithAttachments(friendlyNameInfos, _IHttpContextAccessor, this.SubmissionData, message, files);

                if (!sendEmailsWithAttachments)
                    _smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to send e-mail: {0}", ex);
            }
        }

        public bool SendEmailWithAttachments(IEnumerable<FriendlyNameInfo> friendlyNameInfos, IHttpContextAccessor httpRequestContext, Submission submissionData, MailMessage message, List<UploadedFile> files)
        {
            if (files.Any())
            {
                foreach (var file in files)
                {
                    string mime = MimeKit.MimeTypes.GetMimeType(file.Name);
                    Attachment attachment = new Attachment(file.InputStream, file.Name, mime);
                    message.Attachments.Add(attachment);
                }

                _smtpClient.Send(message);
                message.Dispose();

                return true;
            }

            return false;
        }

        public IEnumerable<DownloadFile> GetUrls(IEnumerable<string> elementIds, IDictionary<string, object> submissionDataDict)
        {
            IEnumerable<string> uploadElementFiles = GetUploadElements(submissionDataDict, elementIds);
            return ParseUrls(uploadElementFiles);
        }

        public static IEnumerable<DownloadFile> ParseUrls(IEnumerable<string> uploadElementFiles)
        {
            var result = new List<DownloadFile>();
            foreach (var uploadElementFile in uploadElementFiles)
            {
                var files = uploadElementFile.Split('|');
                foreach (var file in files)
                {
                    var fileparts = file.Split(new string[] {"#@"}, StringSplitOptions.None);
                    var downloadFile = new DownloadFile()
                    {
                        Name = fileparts.Last(),
                        Url = fileparts.First()
                    };
                    result.Add(downloadFile);
                }
            }

            return result;
        }

        public static IEnumerable<string> GetUploadElements(IDictionary<string, object> submissionDataDict, IEnumerable<string> elementIds)
        {
            return
                submissionDataDict.Where(o => elementIds.Any(elementId => elementId == o.Key) && o.Value.ToString() != string.Empty)
                    .Select(o => o.Value.ToString());
        }

        public static string GetFileName(string url)
        {
            Uri uri = new Uri(url);
            var name = Path.GetFileName(uri.LocalPath);
            name = name.Remove(0, name.IndexOf('_') + 1);         //Episerver will add an id and underscore before original filename
            return name;
        }

        protected virtual IEnumerable<PlaceHolder> GetSubjectPlaceHoldersCustom(IEnumerable<FriendlyNameInfo> friendlyNames)
        {
            List<PlaceHolder> placeHolderList = new List<PlaceHolder>();
            placeHolderList.AddRange(this.GetFriendlyNamePlaceHoldersCustom(_IHttpContextAccessor, this.SubmissionData, friendlyNames, false));
            return placeHolderList;
        }

        protected virtual IEnumerable<PlaceHolder> GetBodyPlaceHoldersCustom(IEnumerable<FriendlyNameInfo> friendlyNames)
        {
            List<PlaceHolder> placeHolderList = new List<PlaceHolder>();
            placeHolderList.AddRange(this.GetPredefinedPlaceHoldersCustom(friendlyNames));
            placeHolderList.AddRange(this.GetFriendlyNamePlaceHoldersCustom(_IHttpContextAccessor, this.SubmissionData, friendlyNames, this._sendMessageInHTMLFormat));
            return placeHolderList;
        }

        protected virtual IEnumerable<PlaceHolder> GetPredefinedPlaceHoldersCustom(IEnumerable<FriendlyNameInfo> friendlyNames)
        {
            return new List<PlaceHolder>()
            {
                new PlaceHolder("summary", this.GetFriendlySummaryTextCustom(friendlyNames))
            };
        }

        protected virtual string GetFriendlySummaryTextCustom(IEnumerable<FriendlyNameInfo> friendlyNames)
        {
            if (this.SubmissionData == null || this.SubmissionData.Data == null)
                return string.Empty;
            
            string separator = this._sendMessageInHTMLFormat ? "<br />" : Environment.NewLine;
            
            if (friendlyNames == null || friendlyNames.Count() == 0)
                return string.Join(separator,
                    this.SubmissionData.Data.Select(
                        x =>
                            string.Format("{0} : {1}", x.Key.ToLowerInvariant(),
                                string.IsNullOrEmpty(x.Value as string)
                                    ? string.Empty
                                    : WebUtility.HtmlEncode(x.Value as string))));
            return string.Join(separator,
                friendlyNames.Select(
                    x =>
                        string.Format("{0} : {1}", x.FriendlyName,
                            this.GetSubmissionDataFieldValueCustom(_IHttpContextAccessor, this.SubmissionData, x, true))));
        }

        public virtual IEnumerable<PlaceHolder> GetFriendlyNamePlaceHoldersCustom(IHttpContextAccessor requestBase, Submission submissionData, IEnumerable<FriendlyNameInfo> friendlyNames, bool performHtmlEncode)
        {
            if (friendlyNames != null && friendlyNames.Count<FriendlyNameInfo>() != 0)
            {
                foreach (FriendlyNameInfo friendlyName in friendlyNames)
                {
                    FriendlyNameInfo fname = friendlyName;
                    yield return
                        new PlaceHolder(fname.FriendlyName, this.GetSubmissionDataFieldValueCustom(requestBase, submissionData, fname, performHtmlEncode));
                }
            }
        }

        public virtual string GetSubmissionDataFieldValueCustom(IHttpContextAccessor requestBase, Submission submissionData, FriendlyNameInfo friendlyName, bool performHtmlEncode)
        {
            string empty = string.Empty;
            
            if (submissionData == null || submissionData.Data == null)
                return empty;

            string rawData = submissionData.Data.FirstOrDefault(x => x.Key.Equals(friendlyName.ElementId, StringComparison.OrdinalIgnoreCase)).Value as string;

            if (!string.IsNullOrEmpty(rawData) && friendlyName.FormatType.Equals((object) FormatType.Link))
            {
                IDictionary<string, string> postedFiles = this.GetPostedFilesCustom(rawData);
                if (postedFiles != null && postedFiles.Count > 0)
                    return performHtmlEncode
                        ? this.GetPostedFilesInHtmlFormat(requestBase, postedFiles)
                        : this.GetPostedFilesInPlainTextFormat(requestBase, postedFiles);
            }

            if (string.IsNullOrEmpty(rawData))
                return rawData;
            
            return performHtmlEncode ? WebUtility.HtmlEncode(rawData) : rawData;
        }

        private IDictionary<string, string> GetPostedFilesCustom(string rawData)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string fileSeperator = "|";
            string[] files = rawData.SplitBySeparator(fileSeperator);
            
            if (files.Length == 0)
                return dictionary;
            
            string nameLinkSeperator = "#@";
            
            foreach (string file in files)
            {
                int length = file.IndexOf(nameLinkSeperator);
                string name = file.Substring(length + nameLinkSeperator.Length);
                string link = file.Substring(0, length);
                dictionary.Add(link, name);
            }
            
            return dictionary;
        }

        private string GetPostedFilesInHtmlFormat(IHttpContextAccessor requestBase, IDictionary<string, string> postedFiles)
        {
            return
                postedFiles.Select(
                        x => string.Format("<a href=\"{0}{1}\">{2}</a>", requestBase.HttpContext.Request.GetBaseUrl(), x.Key, x.Value))
                    .ToStringWithSeparator(", ");
        }

        private string GetPostedFilesInPlainTextFormat(IHttpContextAccessor requestBase, IDictionary<string, string> postedFiles)
        {
            return
                postedFiles.Select(x => string.Format("{0}{1}", requestBase.HttpContext.Request.GetBaseUrl(), x.Key))
                    .ToStringWithSeparator(", ");
        }

        private List<UploadedFile> GetUploadedFiles()
        {
            var uploadedFiles = new List<UploadedFile>();

            try
            {
                for (int i = 0; i < this.HttpRequestContext.HttpContext.Request.Form.Files.Count; i++)
                {
                    IFormFile postedFile = this.HttpRequestContext.HttpContext.Request.Form.Files[i];
                    //postedFile.OpenReadStream().Position = 0;
                    //postedFile..OpenReadStream()..Seek(0, SeekOrigin.Begin);
                    using (var memoryStream = new MemoryStream())
                    {
                        postedFile.OpenReadStream().CopyTo(memoryStream);
                        uploadedFiles.Add(
                        new UploadedFile()
                        {
                            Name = postedFile.FileName,
                            Type = MimeKit.MimeTypes.GetMimeType(postedFile.FileName),
                            InputStream = memoryStream
                        });
                        //return memoryStream.ToArray();
                    }
                    //MemoryStream memoryStream = new MemoryStream();
                    //postedFile.InputStream.CopyTo(memoryStream);
                    //memoryStream.Position = 0;
                    //memoryStream.Seek(0, SeekOrigin.Begin);

                    
                }
            }
            catch (Exception ex)
            {
                PostSubmissionActorBase._logger.Error("Failed to get uploaded files: {0}", ex);
            }

            return uploadedFiles;
        }

    }

    public class DownloadFile
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public string GetAbsoluteUrl(IHttpContextAccessor httpRequestBase)
        {
            var isSecure = false;
            try
            {
                isSecure = httpRequestBase.HttpContext.Request.IsHttps;
            }
            catch
            {
                //httpRequestBase.IsSecureConnection can throw Argument exceptions 
            }
            return (isSecure ? "https://" : "http://") + httpRequestBase.HttpContext.Request.Host.Host + ":" + httpRequestBase.HttpContext.Request.Host.Port + this.Url;
        }
    }

    public class UploadedFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public MemoryStream InputStream { get; set; }
    }
}
