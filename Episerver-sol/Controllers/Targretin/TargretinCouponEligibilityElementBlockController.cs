using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.Targretin;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using EpiserverBH.Models.Blocks.HKLocationBlock;

namespace EpiserverBH.Controllers.Targretin
{
     public class TargretinCouponEligibilityElementBlockController : BlockComponent<TargretinCouponEligibilityElementBlock>
    {
        private static TargretinCouponEligibilityElementBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;

        public TargretinCouponEligibilityElementBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }
        protected override IViewComponentResult InvokeComponent(TargretinCouponEligibilityElementBlock currentContent)
        {
            _block = currentContent;
             return View("/Views/Shared/Blocks/TargretinCouponEligibilityElementBlock.cshtml", _block);
        }


        [HttpPost]
        public IActionResult SignUpUser([FromBody] UserCouponEligibility info)
        {
            bool isSuccess = false;            
            string strSOAPURL = Configuration.GetValue<string>("CustomConfiguration:TargretinSoapURL");
            string strUsername = Configuration.GetValue<string>("CustomConfiguration:TargretinUserName");
            string strPassword = Configuration.GetValue<string>("CustomConfiguration:TargretinPassword");
            string strExecutionStatus = string.Empty;
            string strCouponNumber = string.Empty;
            string strGroupNumber = string.Empty;
            string errorText = string.Empty;

            if (!strSOAPURL.Equals(string.Empty) && !strUsername.Equals(string.Empty) && !strPassword.Equals(string.Empty))
            {
                try
                {
                    int intAgeInYears = 0;
                    int intAgeInMonths = 0;
                    int intAgeInDays = 0;

                    DateTime dtBirthDate = DateTime.ParseExact(info.BirthDay, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    CalculateAge(dtBirthDate, out intAgeInYears, out intAgeInMonths, out intAgeInDays);

                    bool blIsCommercial = false;
                    bool blIsGovernment = false;
                    bool blIsConfirmation = false;
                    int intAge = 0;

                    if (info.Commercial == "Yes")
                    {
                        blIsCommercial = true;
                    }
                    else
                    {
                        blIsCommercial = false;
                    }
                    if (info.Government == "Yes")
                    {
                        blIsGovernment = true;
                    }
                    else
                    {
                        blIsGovernment = false;
                    }
                    if (info.Confirmation == "Yes")
                    {
                        blIsConfirmation = true;
                    }
                    else
                    {
                        blIsConfirmation = false;
                    }


                    intAge = intAgeInYears;

                    if (intAge >= 18 && blIsCommercial && !blIsGovernment && blIsConfirmation)
                    {
                        try
                        {
                            var _url = "https://www.tripleiesampling.com/MMHealthPartnerInterfaceWS/PartnerInterfaceWS.asmx";
                            var _action = "http://www.tripleiesampling.com/PartnerInterfaceWS/GetNextAvailableDocumentNumberAndActivate";

                            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
                            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                            // begin async call to web request.
                            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                            // suspend this thread until call is complete. You might want to
                            // do something usefull here like update your UI.
                            asyncResult.AsyncWaitHandle.WaitOne();

                            // get the response from the completed web request.
                            string soapResult;
                            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                            {
                                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                                {
                                    soapResult = rd.ReadToEnd();
                                }
                                
                            }
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(soapResult);
                            XElement objResponse = XElement.Load(new XmlNodeReader(doc));
                            
                            foreach (XNode node in objResponse.Elements().Elements().Elements().Nodes())
                            {
                                strExecutionStatus = ((XElement)node).Value;
                                strCouponNumber = ((XElement)node.NextNode).Value;
                                strGroupNumber = ((XElement)node.NextNode.NextNode).Value;

                                isSuccess = true;
                                break;
                            }


                           
                        }
                        catch (Exception ex)
                        {
                            //MailService.SendAlert("no-reply@valeantonline.com", "Episerver - Targretin Coupon Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                            errorText = ex.Message;
                        }


                    }
                    else
                    {
                        // formPanel.Visible = false;
                        //ineligiblePanel.Visible = true;
                        //eligiblePanel.Visible = false;

                    }
                }
                catch (Exception ex)
                {
                    //MailService.SendAlert("no-reply@valeantonline.com", "Episerver - Targretin Coupon Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                    errorText = ex.Message;
                    isSuccess = false;
                }
            }
            else
            {
                isSuccess = false;
                errorText = "Web Service Configuration Missing...! Please configure to continue..!";
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    ErrorText = errorText,
                    ExecutionStatus = strExecutionStatus,
                    CouponNumber = strCouponNumber,
                    GroupNumber = strGroupNumber

                }),

                ContentType = "application/json",
            };
        }
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
           
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
            @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:par=""http://www.tripleiesampling.com/PartnerInterfaceWS/"">
   <soapenv:Header>
      <par:ESamplingSoapHeader>
         <par:UserName>MMHTargretinSHCProd</par:UserName>
         <par:Password>6r2SVMNKyGGrSoPDplKQ</par:Password>
      </par:ESamplingSoapHeader>
   </soapenv:Header>
   <soapenv:Body>
      <par:GetNextAvailableDocumentNumberAndActivate>
         <par:groupNumber>OH8401031</par:groupNumber>
      </par:GetNextAvailableDocumentNumberAndActivate>
   </soapenv:Body>
</soapenv:Envelope>");
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        void CalculateAge(DateTime dtDateOfBirth, out int intNoOfYears, out int intNoOfMonths, out int intNoOfDays)
        {
            DateTime dtCurrentDate = DateTime.Now;

            intNoOfDays = dtCurrentDate.Day - dtDateOfBirth.Day;
            intNoOfMonths = dtCurrentDate.Month - dtDateOfBirth.Month;
            intNoOfYears = dtCurrentDate.Year - dtDateOfBirth.Year;

            if (intNoOfDays < 0)
            {
                intNoOfDays += DateTime.DaysInMonth(dtCurrentDate.Year, dtCurrentDate.Month);
                intNoOfMonths--;
            }

            if (intNoOfMonths < 0)
            {
                intNoOfMonths += 12;
                intNoOfYears--;
            }
        }
        public class UserCouponEligibility
        {
            public string BirthDay { get; set; }
            public string ZipCode { get; set; }
            public string Commercial { get; set; }
            public string Government { get; set; }
            public string Confirmation { get; set; }
        }

    }
}
