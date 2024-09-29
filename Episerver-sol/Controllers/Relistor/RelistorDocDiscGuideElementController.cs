using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.Relistor;
using iText.Kernel.Pdf;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using EPiServer.Find;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace EpiserverBH.Controllers.Relistor
{
    public class RelistorDocDiscGuideElementController : BlockComponent<RelistorDocDiscGuideElementBlock>
    {
        private static RelistorDocDiscGuideElementBlock _block;
        string token = string.Empty;
        // GET: RelistorFormulary
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public RelistorDocDiscGuideElementController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }

        protected override IViewComponentResult InvokeComponent(RelistorDocDiscGuideElementBlock currentContent)
        {
            _block = currentContent;

            //return Index(currentContent);
            return View("/Views/Shared/Blocks/RelistorDocDiscGuideElementBlock.cshtml", _block);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePDF(DDGinfo datainfo)
        {
            bool isSuccess = false;
            string pdfFilePath = string.Empty;
            string pdfFileName = string.Empty;
            string pdfTemplatePath = string.Empty;
            string pdfreadfile = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];


                var host = _IHttpContextAccessor.HttpContext.Request.Host.Host;
                var port = _IHttpContextAccessor.HttpContext.Request.Host.Port;
                var schema = _IHttpContextAccessor.HttpContext.Request.Scheme;
                string url;
                if (host.Contains("localhost"))
                {
                    url = schema + "://" + host + ":" + port + "/";

                }
                else
                {
                    url = schema + "://" + host + "/";
                }

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string AnsVal1 = "";

                        if (!string.IsNullOrEmpty(datainfo.Ques1_01))
                        {
                            AnsVal1 += datainfo.Ques1_01 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques1_02))
                        {
                            AnsVal1 += datainfo.Ques1_02 + ", ";
                        }
                        //if (!string.IsNullOrEmpty(datainfo.Ques1_03))
                        //{
                        //    AnsVal1 += datainfo.Ques1_03 + ", ";
                        //}
                        //if (!string.IsNullOrEmpty(datainfo.Ques1_04))
                        //{
                        //    AnsVal1 += datainfo.Ques1_04 + ", ";
                        //}
                        //if (!string.IsNullOrEmpty(datainfo.Ques1_05))
                        //{
                        //    AnsVal1 += datainfo.Ques1_05 + ", ";
                        //}
                        //if (!string.IsNullOrEmpty(datainfo.Ques1_06))
                        //{
                        //    AnsVal1 += datainfo.Ques1_06 + ", ";
                        //}
                        if (!string.IsNullOrEmpty(datainfo.Ques1_03))
                        {
                            AnsVal1 += datainfo.Ques1_03;
                        }


                        AnsVal1 = AnsVal1.Trim().EndsWith(",") ? AnsVal1.Trim().Remove(AnsVal1.Trim().Length - 1, 1) : AnsVal1.Trim();

                        string AnsVal2 = "";

                        if (!string.IsNullOrEmpty(datainfo.Ques2_01))
                        {
                            AnsVal2 += datainfo.Ques2_01 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques2_02))
                        {
                            AnsVal2 += datainfo.Ques2_02 + ", ";
                        }


                        AnsVal2 = AnsVal2.Trim().EndsWith(",") ? AnsVal2.Trim().Remove(AnsVal2.Trim().Length - 1, 1) : AnsVal2.Trim();

                        string AnsVal3 = "";

                        if (!string.IsNullOrEmpty(datainfo.Ques3_01))
                        {
                            AnsVal3 += datainfo.Ques3_01 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques3_02))
                        {
                            AnsVal3 += datainfo.Ques3_02 + ", ";
                        }



                        AnsVal3 = AnsVal3.Trim().EndsWith(",") ? AnsVal3.Trim().Remove(AnsVal3.Trim().Length - 1, 1) : AnsVal3.Trim();


                        string AnsVal4 = "";

                        if (!string.IsNullOrEmpty(datainfo.Ques4_01))
                        {
                            AnsVal4 += datainfo.Ques4_01 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques4_02))
                        {
                            AnsVal4 += datainfo.Ques4_02 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques4_03))
                        {
                            AnsVal4 += datainfo.Ques4_03 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques4_04))
                        {
                            AnsVal4 += datainfo.Ques4_04 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques4_05))
                        {
                            AnsVal4 += datainfo.Ques4_05 + ", ";
                        }

                        AnsVal4 = AnsVal4.Trim().EndsWith(",") ? AnsVal4.Trim().Remove(AnsVal4.Trim().Length - 1, 1) : AnsVal4.Trim();

                        string AnsVal5 = "";

                        if (!string.IsNullOrEmpty(datainfo.Ques5_01))
                        {
                            AnsVal5 += datainfo.Ques5_01 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_02))
                        {
                            AnsVal5 += datainfo.Ques5_02 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_03))
                        {
                            AnsVal5 += datainfo.Ques5_03 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_04))
                        {
                            AnsVal5 += datainfo.Ques5_04 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_05))
                        {
                            AnsVal5 += datainfo.Ques5_05 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_06))
                        {
                            AnsVal5 += datainfo.Ques5_06 + ", ";
                        }
                        if (!string.IsNullOrEmpty(datainfo.Ques5_07))
                        {
                            AnsVal5 += datainfo.Ques5_07;
                        }

                        AnsVal5 = AnsVal5.Trim().EndsWith(",") ? AnsVal5.Trim().Remove(AnsVal5.Trim().Length - 1, 1) : AnsVal5.Trim();

                        //PDF Generation Starts
                        pdfFilePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Relistor-DTC");
                        pdfTemplatePath = url + "siteassets/pdf/";
                        //pdfTemplatePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Relistor-DTC");
                        pdfreadfile = "relistor_interactive_guide_pdf_form.pdf";
                        pdfTemplatePath = Path.Combine(pdfTemplatePath, pdfreadfile);
                        string currentDateTime = DateTime.Now.ToString("MMddyyy-HHmmssff");
                        pdfFileName = "Relistor-DTC-Discussion_Guide-" + currentDateTime + ".pdf";
                        pdfFilePath = System.IO.Path.Combine(pdfFilePath, pdfFileName);

                        using (PdfDocument document = new PdfDocument(new PdfReader(pdfTemplatePath), new PdfWriter(pdfFilePath)))
                        {
                            var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                            var field1 = form.GetField("Prescription opioid pain medicines taken");
                            var field2 = form.GetField("Number of bowel movements");
                            var field3 = form.GetField("Reduction of bowel movements");
                            var field4 = form.GetField("Potential OIC symptoms");
                            var field5 = form.GetField("Relief methods tried");

                            if (field1 != null)
                            {
                                field1.SetValue("Prescription opioid pain medicines taken", AnsVal1);
                            }

                            if (field2 != null)
                            {
                                field2.SetValue("Reduction of bowel movements", AnsVal3);
                            }
                            if (field3 != null)
                            {
                                field3.SetValue("Number of bowel movements", AnsVal2);
                            }
                            if (field4 != null)
                            {
                                field4.SetValue("Potential OIC symptoms", AnsVal4);
                            }
                            if (field5 != null)
                            {
                                field5.SetValue("Relief methods tried", AnsVal5);
                            }

                            form.FlattenFields();
                            document.Close();
                        }

                        datainfo.filepath = pdfFilePath;

                        HtmlHelpers.UploadPdfinCms(pdfFilePath, "RelistorDDG");
                        isSuccess = true;

                        System.IO.File.Delete(pdfFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver - Relistor-DTC - GeneratePDF Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                isSuccess = false;

            }

            // return Json(new { IsSuccess = isSuccess, PDFName = pdfFileName }, JsonRequestBehavior.AllowGet);
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    PDFName = pdfFileName
                }),

                ContentType = "application/json",
            };

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void SendMail(string filename, string recepient)
        {
            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];



                var host = _IHttpContextAccessor.HttpContext.Request.Host.Host;
                var port = _IHttpContextAccessor.HttpContext.Request.Host.Port;
                string url;
                if (host.Contains("localhost"))
                {
                    url = "https://" + host + ":" + port + "/";

                }
                else
                {
                    url = "https://" + host + "/";
                }


                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        // var smtpClient = new SmtpClient();

                        string hosts = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Host");
                        string fromAddress = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:NotificationEmailAddress");
                        string userName = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:UserName");
                        string password = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Password");

                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = hosts;
                        smtpClient.Port = Convert.ToInt16(Configuration.GetValue<int>("EPiServer:CMS:Smtp:Network:Port"));
                        smtpClient.Credentials = new NetworkCredential(userName, password);
                        smtpClient.EnableSsl = true;

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("no-reply@valeantonline.com"),
                            Subject = "Relistor Discussion Guide Results",
                            Body = "<p>Hello,</p> <br> <p>Please see attached for the results of your Discussion Guide for opioid-induced constipation. Bring this to your healthcare provider to help start a conversation with them.  </p><br><p>Also attached is the full Prescribing Information for Relistor tablets and Relistor injections, including medication guide.</p><br><p>Best regards,</p><p>The Relistor team</p><br><br><p>REL.0144.USA.20 V4.0</p> ",
                            IsBodyHtml = true,
                        };

                        mailMessage.To.Add(recepient);
                        string path = Path.Combine(url + "siteassets/pdf/");
                        string path1 = Path.Combine(url + "siteassets/RelistorDDG/");


                        WebClient myClient = new WebClient();
                        byte[] bytes = myClient.DownloadData(Path.Combine(path1, filename));
                        byte[] bytes2 = myClient.DownloadData(Path.Combine(path, "relistor-pi.pdf"));

                        using (MemoryStream Pdf1 = new MemoryStream(bytes))
                        {
                            System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(Pdf1, new System.Net.Mime.ContentType(MediaTypeNames.Application.Pdf));
                            attachment1.Name = filename;
                            mailMessage.Attachments.Add(attachment1);

                            using (MemoryStream Pdf2 = new MemoryStream(bytes2))
                            {
                                System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(Pdf2, new System.Net.Mime.ContentType(MediaTypeNames.Application.Pdf));
                                attachment2.Name = "relistor-pi.pdf";
                                mailMessage.Attachments.Add(attachment2);
                                smtpClient.Send(mailMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver - Relistor-DTC - SendMail Function Exception", ex.Message, "websitealerts@bauschhealth.com");
            }
        }
    }


    public class DDGinfo
    {

        public string Ques1_01 { get; set; }
        public string Ques1_02 { get; set; }
        public string Ques1_03 { get; set; }
        public string Ques1_04 { get; set; }
        public string Ques1_05 { get; set; }
        public string Ques1_06 { get; set; }
        public string Ques1_07 { get; set; }

        public string Ques2_01 { get; set; }
        public string Ques2_02 { get; set; }

        public string Ques3_01 { get; set; }
        public string Ques3_02 { get; set; }


        public string Ques4_01 { get; set; }
        public string Ques4_02 { get; set; }
        public string Ques4_03 { get; set; }
        public string Ques4_04 { get; set; }
        public string Ques4_05 { get; set; }

        public string Ques5_01 { get; set; }
        public string Ques5_02 { get; set; }
        public string Ques5_03 { get; set; }
        public string Ques5_04 { get; set; }
        public string Ques5_05 { get; set; }
        public string Ques5_06 { get; set; }
        public string Ques5_07 { get; set; }

        public string filepath { get; set; }
    }
}