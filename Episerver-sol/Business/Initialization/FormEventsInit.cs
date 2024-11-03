using EPiServer;
using EPiServer.Core;
using EPiServer.Find.Helpers.Text;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Events;
using EPiServer.Forms.Core.Internal;
using EPiServer.Forms.Core.Models;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EpiserverBH.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FormEventsInit : IInitializableModule, IConfigurableModule
    {
        private Injected<IFormRepository> _formRepository;
        private Injected<IContentLoader> _contentLoader;
        private static  IHttpContextAccessor _IHttpContextAccessor { get; }
        protected ServiceConfigurationContext _serviceConfigurationContext;

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            _serviceConfigurationContext = context;
        }
        public void Initialize(InitializationEngine context)
        {

            _serviceConfigurationContext.Services.AddSingleton<DataSubmissionService, ExtendedFormSubmission>();

        }

        private void Instance_FormsSubmitting(object sender, FormsEventArgs e)
        {
            // The event fires before the data is submitted so there is an opportunity to interact here
            //var formData = e.Data;



            var submission = e as FormsSubmittingEventArgs;
            var content = _contentLoader.Service.Get<IContent>(e.FormsContent.ContentLink);
            var localizable = content as ILocalizable;

            if (content == null || localizable == null || submission == null)
            {
                return;
            }


            #region Form Interceptor for Form "RadioButtonStepForm"
            //Intercept only forms that is required to be intercepted
            //Form Name with proper naming convention will help

            if (content.Name == "RadioButtonStepForm")
            {
                var formsId = new FormIdentity(e.FormsContent.ContentGuid, localizable.Language.Name);
                var friendlyNameInfos = _formRepository.Service.GetDataFriendlyNameInfos(formsId);
                var PDFFileURL = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "PDFFileUrl");

                if (PDFFileURL != null && !PDFFileURL.FriendlyName.IsNullOrWhiteSpace())
                {
                    var PDFFileURLKey = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == PDFFileURL.ElementId);

                    //if not last step of form submission return
                    //basically we are checking if hidden control on last step is correctly loaded
                    if (PDFFileURLKey.Key == null) return;
                    var PDFFileURLValue = "some pdf url comes here";
                    submission.SubmissionData.Data[PDFFileURLKey.Key] = "some pdf url comes here";// $"{PDFFileURLValue}";
                }

                StringBuilder sbAnswersToRadioBtnQuestions = new StringBuilder();
                sbAnswersToRadioBtnQuestions.Append("<ul>");

                var Radio1 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 1");
                if (Radio1 != null && !Radio1.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio1Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio1.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio1Key.Value + "</li>");
                }
                var Radio2 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 2");
                if (Radio2 != null && !Radio2.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio2Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio2.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio2Key.Value + "</li>");
                }
                var Radio3 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 3");
                if (Radio3 != null && !Radio3.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio3Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio3.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio3Key.Value + "</li>");
                }
                var Radio4 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 4");
                if (Radio4 != null && !Radio4.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio4Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio4.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio4Key.Value + "</li>");
                }
                var Radio5 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 5");
                if (Radio5 != null && !Radio5.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio5Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio5.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio5Key.Value + "</li>");
                }
                var Radio6 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 6");
                if (Radio6 != null && !Radio6.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio6Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio6.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio6Key.Value + "</li>");
                }
                var Radio7 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 7");
                if (Radio7 != null && !Radio7.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio7Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio7.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio7Key.Value + "</li>");
                }
                var Radio8 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 8");
                if (Radio8 != null && !Radio8.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio8Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio8.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio8Key.Value + "</li>");
                }
                var Radio9 = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "Radio 9");
                if (Radio9 != null && !Radio9.FriendlyName.IsNullOrWhiteSpace())
                {
                    var Radio9Key = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == Radio9.ElementId);
                    sbAnswersToRadioBtnQuestions.Append("<li>" + Radio9Key.Value + "</li>");
                }

                sbAnswersToRadioBtnQuestions.Append("</ul>");
                _IHttpContextAccessor.HttpContext.Session.SetString("AMD_AnswersToRadioBtnQuestions", sbAnswersToRadioBtnQuestions.ToString());
                //var PDFURL = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "PdfUrl");

                //if (PDFURL != null && !PDFURL.FriendlyName.IsNullOrWhiteSpace())
                //{
                //    var PDFURLKey = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == PDFURL.ElementId);

                //    //if not last step of form submission return
                //    //basically we are checking if hidden control on last step is correctly loaded
                //    if (PDFURLKey.Key == null) return;
                //    var PDFURLValue = "https://www.google.com/";
                //    submission.SubmissionData.Data[PDFURLKey.Key] = $"{PDFURLValue}";
                //}
                //var PDFTextURL = friendlyNameInfos.FirstOrDefault(x => x.FriendlyName == "PdfTextUrl");

                //if (PDFTextURL != null && !PDFTextURL.FriendlyName.IsNullOrWhiteSpace())
                //{
                //    var PDFTextURLKey = submission.SubmissionData.Data.FirstOrDefault(x => x.Key == PDFTextURL.ElementId);

                //    //if not last step of form submission return
                //    //basically we are checking if hidden control on last step is correctly loaded
                //    if (PDFTextURLKey.Key == null) return;
                //    var PDFTextURLValue = "some pdf url comes here";
                //    submission.SubmissionData.Data[PDFTextURLKey.Key] = $"{PDFTextURLValue}";
                //}

                //_serviceConfigurationContext.Services.AddSingleton<DataSubmissionService, ExtendedCampaignDataSubmissionService>();

            }

            #endregion

        }

        public void Uninitialize(InitializationEngine context) { }
    }
}