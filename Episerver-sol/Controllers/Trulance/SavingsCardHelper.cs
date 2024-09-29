using EPiServer.Licensing.Services;
using EPiServer.Web.Mvc;
using iText.Kernel.Pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static EpiserverBH.Controllers.Trulance.SavingsInfo;
using System.Net.Mail;
using EpiserverBH.Helpers;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;

namespace EpiserverBH.Controllers.Trulance
{
    public class SavingsCardHelper
    {
        demographic dgInfo = new demographic();
        string FormName = string.Empty;
        private const string Cachekey = "trulance-savings-token";
        private const string ActivateCachekey = "trulance-activation-token";
        private const string PrintCachekey = "trulance-print-token";
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IHttpContextAccessor _IHttpContextAccessor;
        private static IMemoryCache _IMemoryCache;

        public SavingsCardHelper(IConfiguration _iConfiguration, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor _iHttpContextAccessor, IMemoryCache memoryCache)
        {
            _configuration = _iConfiguration;
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = _iHttpContextAccessor;
            _IMemoryCache = memoryCache;
        }


        public string GenerateToken(string savingscardAction)
        {
            string tokenCode = string.Empty;
            try
            {
                // store the authentication details in a string and pass it to Trial Card api
                string audienceId = string.Empty;
                if (savingscardAction == "download")
                {
                    audienceId = _configuration.GetValue<string>("CustomConfiguration:TrulanceAudienceId");
                }
                else
                {
                    audienceId = _configuration.GetValue<string>("CustomConfiguration:TrulanceCardAudienceId");
                }

                string grant_type = _configuration.GetValue<string>("CustomConfiguration:TrulanceGrantType");
                string username = _configuration.GetValue<string>("CustomConfiguration:TrulanceUserName");
                string password = _configuration.GetValue<string>("CustomConfiguration:TrulancePassword");
                string xmlKey = @"<RSAKeyValue><Modulus>lybn28urZ97DRnnQvuoTmCK9xUS91R6Sl6nkStIuq0pRCnOa1a/YVF3eAz/qz8j1McVep8BXlUEDAiVq+iZx4XfFZuaAVflRW255Jmta/clg74vDCp0Qa1tDkQSPuqH5aBsKPTmzjuU3BcRZGuKdMNxVSl+5dnNxDMkYFepdSgE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

                #region SaltPassword
                string secret;
                var saltrequest = (HttpWebRequest)WebRequest.Create(_configuration.GetValue<string>("CustomConfiguration:TrulanceSaltURL"));
                var saltpostData = "";
                var saltdata = Encoding.ASCII.GetBytes(saltpostData);
                saltrequest.Method = "POST";
                saltrequest.Proxy = WebRequest.DefaultWebProxy;
                saltrequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                saltrequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                saltrequest.Accept = "application/json";
                saltrequest.ContentType = "application/x-www-form-urlencoded";
                saltrequest.ContentLength = saltdata.Length;
                using (var stream = saltrequest.GetRequestStream())
                {
                    stream.Write(saltdata, 0, saltdata.Length);
                }
                var saltresponse = (HttpWebResponse)saltrequest.GetResponse();
                var saltresponseString = new StreamReader(saltresponse.GetResponseStream()).ReadToEnd();
                var saltModel = JsonConvert.DeserializeAnonymousType(saltresponseString, new { Salt = "" });

                secret = $"A.{saltModel.Salt}.{password}";
                var encPassword = string.Empty;
                using (var provider = new RSACryptoServiceProvider())
                {
                    provider.FromXmlString(xmlKey);
                    var encData = provider.Encrypt(Encoding.ASCII.GetBytes(secret), true);
                    encPassword = Convert.ToBase64String(encData);
                }
                #endregion


                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlKey);
                // encrypt the password
                //var encBytes = rsa.Encrypt(ASCIIEncoding.ASCII.GetBytes(password), true);
                //var encPassword = Convert.ToBase64String(encBytes);
                var request = (HttpWebRequest)WebRequest.Create(_configuration.GetValue<string>("CustomConfiguration:TrulanceTokenAddress"));
                var postData = "audience_id=" + audienceId;
                postData += "&grant_type=password";
                postData += "&client_id=" + _configuration.GetValue<string>("CustomConfiguration:TrulanceClientID");
                postData += "&username=" + username;
                postData += "&password=" + HttpUtility.UrlEncode(encPassword);
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.Proxy = WebRequest.DefaultWebProxy;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                request.Accept = "application/json";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                tokenCode = responseString;
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception- Generate Token", ex.Message, "websitealerts@bauschhealth.com");
            }
            return tokenCode;
        }


        #region ReturnProperties
        /// <summary>
        /// rtnResult stores the response received from Trial Card service
        /// </summary>
        public class rtnResult
        {
            public string memberId { get; set; }
            public string errorMessage { get; set; }
            public string status { get; set; }
        }
        #endregion

        #region Messages
        public enum Messages
        {
            MemberNumberInvalid,
            FailureMemberNumberInvalid,
            FailureCardBatchExhausted,
            FailureActivationPeriodExpired,
            UnknownError,
            AccessDenied,
            Enrollment,
            InvalidActivationNumber,
            ActivatedNumber,
            Other
        }
        #endregion

        #region Token deserialization
        /// <summary>
        /// email savings card info to trial card services to generate trial card memeber number
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        private WebClient TokenDeserialize(string token)
        {
            WebClient wclient = new WebClient();
            string x_UserId = _configuration.GetValue<string>("CustomConfiguration:TrulanceUserId");

            ////Deserialize the string and store in to an Token object
            Token authTokenKey = JsonConvert.DeserializeObject<Token>(token);

            wclient.Headers.Add("Content-Type", @"application/json");
            wclient.Headers.Add("Authorization", @"Bearer " + authTokenKey.access_token + " ");
            wclient.Headers.Add("x-tc-userid", @"" + x_UserId + "");

            return wclient;
        }

        #endregion

        #region Print Savings card Info to trial card services
        /// <summary>
        /// email savings card info to trial card services to generate trial card memeber number
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dgInfo"></param>
        /// <returns></returns>

        public rtnResult emailSavingsCardInfo(string token, demographic dgInfo)
        {
            rtnResult result = new rtnResult();
            try
            {
                WebClient wclient = new WebClient();
                wclient = TokenDeserialize(token);
                string URL = string.Empty;
                URL = _configuration.GetValue<string>("CustomConfiguration:TrulanceBasicEnrollmentURL") + dgInfo.ProgramId;
                var tcDemographics = string.Empty;
                ////create a nested list by combingin TrialCard_Info and demographics
                tcDemographics = getdemographics(dgInfo);
                string tresult = wclient.UploadString(URL, "POST", tcDemographics);
                EmailSavingsCardResult tcResult = new EmailSavingsCardResult();
                tcResult = JsonConvert.DeserializeObject<EmailSavingsCardResult>(tresult);
                if (tcResult.success == "True" || tcResult.success == "true")
                {
                    EmailSavingsCardInfo tInfo = new EmailSavingsCardInfo();
                    tInfo = tcResult.data;
                    result.status = tcResult.success;
                    result.memberId = tInfo.memberNumber;
                }
                else
                {
                    result.status = tcResult.success;
                    result.memberId = "";
                    result.errorMessage = tcResult.messages[0];
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                             
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(new Exception(FormName, ex));
            }
            return result;
        }
        #endregion

        #region Print Savings card Info to trial card services
        /// <summary>
        /// print savings card info to trial card services to generate trial card memeber number
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dgInfo"></param>
        /// <returns></returns>
        public rtnResult printSavingsCardInfo(string token, demographic dgInfo)
        {
            rtnResult result = new rtnResult();
            try
            {
                WebClient wclient = new WebClient();
                wclient = TokenDeserialize(token);
                string URL = string.Empty;
                URL = _configuration.GetValue<string>("CustomConfiguration:TrulanceCreateCardURL") + dgInfo.ProgramId;
                string tresult = wclient.UploadString(URL, "POST", "");
                TrialCardResult tcResult = new TrialCardResult();
                tcResult = JsonConvert.DeserializeObject<TrialCardResult>(tresult);
                if (tcResult.success == "True" || tcResult.success == "true")
                {
                    List<TrialCardInfo> tInfo = new List<TrialCardInfo>();
                    foreach (var items in tcResult.data.Values)
                    {
                        tInfo.AddRange(items);
                    }
                    result.status = tcResult.success;
                    result.memberId = tInfo[0].memberNumber;
                }
                else
                {
                    result.status = tcResult.success;
                    result.memberId = "";
                    result.errorMessage = tcResult.messages[0];
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");

            }
            return result;
        }
        #endregion

        #region Activate Savings card Info to trial card services
        /// <summary>
        /// Activate savings card info to trial card services to generate trial card memeber number
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dgInfo"></param>
        /// <returns></returns>

        public rtnResult activateSavingsCardInfo(string token, demographic dgInfo)
        {
            rtnResult result = new rtnResult();
            try
            {
                WebClient wclient = new WebClient();
                wclient = TokenDeserialize(token);
                string memberstatusURL = string.Empty;
                string URL = string.Empty;
                memberstatusURL = _configuration.GetValue<string>("CustomConfiguration:TrulanceMemberNumberStatusURL") + dgInfo.ProgramId + "?memberNumber=" + dgInfo.memberNumber;

                string memberstatus = wclient.DownloadString(memberstatusURL);
                var memstatus = JsonConvert.DeserializeObject<dynamic>(memberstatus);
                wclient.Dispose();
                if (memstatus.success == "true")
                {
                    var memberStatusId = memstatus.data.memberNumberStatusId; // Any value that is not equal to 1 indicates the card id is active
                    var memberStatus = memstatus.data.memberNumberStatus;
                    if ((memberStatusId != "1") || (memberStatus != "Inactive"))
                    {
                        result.status = "false";
                        result.memberId = "";
                        result.errorMessage = "This ID number is already activated and ready to use.";
                    }
                    else
                    {
                        URL = _configuration.GetValue<string>("CustomConfiguration:TrulanceCardActivationURL") + dgInfo.ProgramId;
                        wclient = TokenDeserialize(token);
                        var tcDemographics = string.Empty;
                        tcDemographics = @"{""memberNumber"": """ + dgInfo.memberNumber + @""" }";
                        string tresult = string.Empty;
                        tresult = wclient.UploadString(URL, "POST", tcDemographics);
                        TrialCardActivateResult tcResult = new TrialCardActivateResult();
                        var tcResult1 = Newtonsoft.Json.JsonConvert.DeserializeObject(tresult).ToString();
                        tcResult1 = tcResult1.TrimStart('\"');
                        tcResult1 = tcResult1.TrimEnd('\"');
                        tcResult1 = tcResult1.Replace("\\", "");
                        //tcResult = JsonConvert.DeserializeObject<List<TrialCardResult>>(tcResult1);
                        // var tcResult12 = JsonConvert.DeserializeObject<TrialCardResult>(tresult);
                        tcResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TrialCardActivateResult>(tcResult1);
                        if (tcResult.success == "True" || tcResult.success == "true")
                        {
                            result.status = tcResult.success;
                        }
                        else
                        {
                            result.status = tcResult.success;
                            result.memberId = "";
                            result.errorMessage = tcResult.messages[0];
                        }
                    }
                }
                else
                {
                    result.status = "false";
                    result.memberId = "";
                    result.errorMessage = "Oops! Something went wrong. Please try again soon or visit Trulance.com/savings to get a new card.";
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(new Exception(FormName, ex));
            }
            return result;
        }
        #endregion

        #region GeneratePDF
        /// <summary>
        /// Generate PDF after successfull generation of Trial card member number and status is true and savings card action is download
        /// </summary>
        /// <param name="memberNumber"></param>
        /// <param name="TruInfo"></param>
        public void GeneratePDFDoc(string memberNumber, TrulanceInfo TruInfo)
        {

            var pdfFolderPath = string.Format("Assets/Trulance-DTC/Template/{0}", TruInfo.hdnPDFTemplateName);
            var templateFilePath = Path.Combine(_hostingEnvironment.WebRootPath, pdfFolderPath);

            //string pdfFolderPath = HttpContext.Current.Server.MapPath("~/Assets/Trulance-DTC/Template");
            //string templateFilePath = Path.Combine(pdfFolderPath, TruInfo.hdnPDFTemplateName);
            
            string FilePath = System.IO.Path.Combine(TruInfo.hdnPDFFolderPath, "trulance-savings-card-" + memberNumber + ".pdf");
            PdfDocument document = new PdfDocument(new PdfReader(templateFilePath), new PdfWriter(FilePath));
            var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);
            var field = form.GetField("txtID");

            if (field != null)
            {
                field.SetValue(memberNumber, memberNumber);

            }

            form.FlattenFields();
            document.Close();

            HtmlHelpers.UploadPdfinCms(FilePath, "TrailCard");
            System.IO.File.Delete(FilePath);
        }

        #endregion

        #region set demographic input
        /// <summary>
        /// set json input for demographic value
        /// </summary>
        /// <param name="dgDetailList"></param>
        /// <returns></returns>
        public string getdemographics(demographic dgDetailList)
        {
            var tcDemographis = "";
            try
            {

                tcDemographis = @"{
                                    ""demographics"": {
                                                        ""personId"": " + dgDetailList.personId + @",
                                                        ""firstName"": """ + dgDetailList.firstName + @""",
                                                        ""lastName"": """ + dgDetailList.lastName + @""",
                                                        ""gender"": ""M"",
                                                        ""dateOfBirth"": """ + dgDetailList.dateOfBirth + @""",
                                                        ""email"": """ + dgDetailList.email + @""",
                                                        ""mobilePhone"": """ + dgDetailList.mobilephone + @""",
                                                        ""homePhone"": """ + dgDetailList.homephone + @""",
                                                        ""address1"": """ + dgDetailList.address1 + @""",
                                                        ""address2"": """ + dgDetailList.address2 + @""",
                                                        ""city"": """ + dgDetailList.city + @""",
                                                        ""state"": """ + dgDetailList.state + @""",
                                                        ""postalCode"": """ + dgDetailList.postalCode + @""",
                                                        ""memberNumber"": """ + dgDetailList.memberNumber + @""",
                                                        ""country"": ""US"",
                                                        ""communicationTypes"": """ + dgInfo.communicationTypes + @"""
                                                        }";
                if (dgDetailList.Survey)
                {
                    tcDemographis += @",
                                            ""survey"": [{
                                                          ""surveyId"": 0,
                                                          ""questionId"": """ + dgDetailList.QuestionId + @""",
                                                          ""answerKey"": """ + dgDetailList.AnswerId + @""",
                                                          ""answerText"": """ + dgDetailList.AnswerText + @"""
                                                        }]
                                  }";
                }
                else
                {
                    tcDemographis += @"}";
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");
            }
            return tcDemographis;
        }
        #endregion

      
        #region SetProperties
        /// <summary>
        /// store the form data value in to demographic info 
        /// </summary>
        /// <param name="context"></param>
        public void SetProperties(TrulanceInfo context)
        {
            try
            {
                //ContextField value;
                dgInfo.personId = 0;
                dgInfo.firstName = "BauschHealthTest";
                dgInfo.lastName = "Trulance";
                dgInfo.gender = "M";
                dgInfo.dateOfBirth = "";
                if (context.FormName == "Email")
                {
                    dgInfo.email = context.EmailAddress;
                }
                else
                {
                    dgInfo.email = string.Empty;
                }
                dgInfo.mobilephone = string.Empty;
                dgInfo.homephone = string.Empty;
                dgInfo.address1 = "NA";
                dgInfo.address2 = string.Empty;
                dgInfo.city = string.Empty;
                dgInfo.state = string.Empty;
                dgInfo.postalCode = string.Empty;
                if (context.FormName == "Activation")
                {
                    if (!string.IsNullOrEmpty(context.CardID))
                    {
                        dgInfo.memberNumber = context.CardID;
                    }
                    else
                    {
                        dgInfo.memberNumber = "";
                    }
                }
                else
                {
                    dgInfo.memberNumber = "";
                }
                dgInfo.country = "US";
                dgInfo.communicationTypes = 2;
                dgInfo.QuestionId = 0;
                dgInfo.AnswerId = 0;
                dgInfo.AnswerText = string.Empty;
                dgInfo.Survey = false;
                dgInfo.ProgramId = context.hdnProgramId;

                if (context.FormName == "Print")
                {
                    dgInfo.pdfFolderPath = context.hdnPDFFolderPath;
                    dgInfo.pdfTemplateName = context.hdnPDFTemplateName;

                    //if (context.DataStore.TryGetValue("hdnPDFFolderPath", out value))
                    //{
                    //    dgInfo.pdfFolderPath = HttpContext.Current.Server.MapPath(value.ToString());
                    //}
                    //if (context.DataStore.TryGetValue("hdnPDFTemplateName", out value))
                    //{
                    //    dgInfo.pdfTemplateName = value.ToString();
                    //}
                }
                else
                {
                    dgInfo.pdfFolderPath = string.Empty;
                    dgInfo.pdfTemplateName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");

            }
        }
        #endregion

        public rtnResult Execute(TrulanceInfo context)
        {
            rtnResult objResponse = new rtnResult();

            if (string.IsNullOrEmpty(context.FormName))
            {
                //string url = HttpContext.Current.Request.Url.Host;
                string url = _IHttpContextAccessor.HttpContext.Request.Host.ToString();

                string message = string.Empty;
                message += "Please check the following field property naming conventions as mentioned:";
                message += "Episerver - Trulance "+ context.FormName;
                MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance" + FormName + " Form Exception", message, "websitealerts@bauschhealth.com");

                //DotNetNuke.Services.Exceptions.Exceptions.LogException(new Exception(url + "::::" + message));
                return null;
            }

            string authToken = string.Empty;
            string savingscardAction = context.SavingCardAction;
            FormName = context.FormName;
            SetProperties(context);

            //Check whether the Token Authentication key exists in Cache
            if (savingscardAction == "download")
            {
                ///authToken = (string)HttpRuntime.Cache.Get(Cachekey);
                authToken = (string)_IMemoryCache.Get(Cachekey);
            }
            else if (savingscardAction == "print")
            {
                ///authToken = (string)HttpRuntime.Cache.Get(PrintCachekey);
                authToken = (string)_IMemoryCache.Get(PrintCachekey);
            }
            else
            {
                ///authToken = (string)HttpRuntime.Cache.Get(ActivateCachekey);
                authToken = (string)_IMemoryCache.Get(ActivateCachekey);
            }

            if (string.IsNullOrEmpty(authToken))
            {
                //Generate Token Authentication key
                authToken = GenerateToken(savingscardAction); // GenerateTokenAuthenticationKey(scAction).Result;
                if (savingscardAction == "download")
                {
                    ///HttpRuntime.Cache.Insert(Cachekey, authToken, null, DateTime.Now.AddMinutes(10d), System.Web.Caching.Cache.NoSlidingExpiration);

                    _IMemoryCache.Set(Cachekey, authToken, DateTime.Now.AddMinutes(10d));
                }
                else if (savingscardAction == "print")
                {
                    ///HttpRuntime.Cache.Insert(PrintCachekey, authToken, null, DateTime.Now.AddMinutes(10d), System.Web.Caching.Cache.NoSlidingExpiration);

                    _IMemoryCache.Set(PrintCachekey, authToken, DateTime.Now.AddMinutes(10d));
                }
                else
                {
                    ///HttpRuntime.Cache.Insert(ActivateCachekey, authToken, null, DateTime.Now.AddMinutes(10d), System.Web.Caching.Cache.NoSlidingExpiration);

                    _IMemoryCache.Set(ActivateCachekey, authToken, DateTime.Now.AddMinutes(10d));
                }
            }

            //send savings card information to Trial card services and store the return data in returnResult
            if (savingscardAction == "download")
            {
                objResponse = emailSavingsCardInfo(authToken, dgInfo);
            }
            else if (savingscardAction == "print")
            {
                objResponse = printSavingsCardInfo(authToken, dgInfo);
            }
            else
            {
                objResponse = activateSavingsCardInfo(authToken, dgInfo);
            }

            /*
            ContextField MemberCardId = new ContextField();
            MemberCardId.Name = "MemberCardId";
            MemberCardId.Id = "MemberCardId";
            MemberCardId.ValueObj = objResponse.memberId;

            ContextField leadId = new ContextField();
            leadId.Name = "LeadID";
            leadId.Id = "LeadID";
            leadId.ValueObj = objResponse.memberId;

            ContextField customerror = new ContextField();
            customerror.Name = "ErrorMsg";
            customerror.Id = "ErrorMsg";
            customerror.ValueObj = objResponse.errorMessage;
            */

            if (objResponse.status == "True" || objResponse.status == "true")
            {
                if (savingscardAction == "print")
                {
                    string pdf = string.Empty;
                    try
                    {
                        GeneratePDFDoc(objResponse.memberId, context);
                        //store trial card member number in cookies if the status is true


                        ///HttpCookie objDeleteCookie = new HttpCookie("TrulanceSavingsCardNum");
                        var objDeleteCookie = new CookieOptions();
                        objDeleteCookie.Expires = DateTime.Now.AddDays(-1d);
                        _IHttpContextAccessor.HttpContext.Response.Cookies.Append("TrulanceSavingsCardNum", "TrulanceSavingsCardNum", objDeleteCookie);


                        ///HttpCookie savingscardId = new HttpCookie("TrulanceSavingsCardNum", objResponse.memberId);
                        ///HttpContext.Current.Response.Cookies.Add(savingscardId);
                        var savingscardId = new CookieOptions();

                        savingscardId.HttpOnly = false;
                        savingscardId.Expires = DateTime.Now.AddHours(1);
                        
                        _IHttpContextAccessor.HttpContext.Response.Cookies.Append("TrulanceSavingsCardNum", objResponse.memberId, savingscardId);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance"+ FormName+" Form Exception", ex.Message, "websitealerts@bauschhealth.com");

                        //DotNetNuke.Services.Exceptions.Exceptions.LogException(new Exception(FormName, ex));
                    }
                }

                if (string.IsNullOrEmpty(context.LeadID))
                {
                    context.LeadID = objResponse.memberId;
                }
            }
            else
            {
                
            }

            return objResponse;
        }


    }

}