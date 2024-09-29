using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.Ibsdtakesguts;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Linq;


namespace EpiserverBH.Controllers.Ibsdtakesguts
{
    public class IbsdtakesgutsQuizElementBlockController : BlockComponent<IbsdtakesgutsQuizElementBlock>
    {
        private static IbsdtakesgutsQuizElementBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public IbsdtakesgutsQuizElementBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        protected override IViewComponentResult InvokeComponent(IbsdtakesgutsQuizElementBlock currentContent)
        {
            _block = currentContent;

            //return Index(currentContent);
            return View("/Views/Shared/Blocks/IbsdtakesgutsQuizElementBlock.cshtml", _block);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitQuiz(IbsdtakesgutsQuizInfo info)
        {
            bool isSuccess = false;
            string pdfFileName = string.Empty;
            string pdfPath = string.Empty;

            var fileName = "ibsd-discussion-guide-" + DateTime.Now.ToString("yyyymmdd-HHmmssff") + ".pdf";

            //TODO:: Bind the PDF form fields
            string folderToSave = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "IBSDtakesguts");
            string templatePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "IBSDtakesguts", "Template", "IbsdtakesgutsQuiz.pdf");
            if (System.IO.File.Exists(templatePath))
            {
                pdfFileName = System.IO.Path.Combine(folderToSave, string.Format(fileName));
                pdfPath = pdfFileName;
                PdfDocument document = new PdfDocument(new PdfReader(templatePath), new PdfWriter(pdfFileName));
                var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                var field1 = form.GetField("Selection1");
                var field2 = form.GetField("Selection2");
                var field3 = form.GetField("Selection3");
                var field4 = form.GetField("Selection4");
                var field5 = form.GetField("Selection5");
                var field6 = form.GetField("Selection6");
                var field6Option = form.GetField("Selection7");// for yes/no answer fields
                var field7 = form.GetField("Selection8");
                var field7Option = form.GetField("Selection9");// for yes/no answer fields
                var field8 = form.GetField("Selection10");

                if (field1 != null)
                {
                    field1.SetValue(info.QuesOne, info.QuesOne);

                }
                if (field2 != null)
                {
                    var quesTwoT = info.QuesTwo.Remove(info.QuesTwo.Length - 1, 1);
                    field2.SetValue(quesTwoT, quesTwoT);
                }
                if (field3 != null)
                {
                    field3.SetValue(info.QuesThree, info.QuesThree);
                }
                if (field4 != null)
                {
                    field4.SetValue(info.QuesFour, info.QuesFour);
                }
                if (field5 != null)
                {
                    field5.SetValue(info.QuesFive, info.QuesFive);
                }
                if (field6 != null)
                {
                    field6.SetValue(info.QuesSix, info.QuesSix);
                }
                //for yes/no answer fields
                if (field6Option != null)
                {
                    if (info.OptionOne != null)
                    {
                        field6Option.SetValue(info.OptionOne, info.OptionOne);
                    }
                }
                if (field7 != null)
                {
                    field7.SetValue(info.QuesSeven, info.QuesSeven);
                }
                //for yes/no answer fields
                if (field7Option != null)
                {
                    if (info.OptionTwo != null)
                    {
                        field7Option.SetValue(info.OptionTwo, info.OptionTwo);
                    }
                }
                if (field8 != null)
                {
                    field8.SetValue(info.QuesEight, info.QuesEight);
                }
                form.FlattenFields();
                document.Close();

                pdfFileName = System.IO.Path.GetFileName(pdfFileName);

                HtmlHelpers.UploadPdfinCms(pdfPath, "Quiz-PDF");
                System.IO.File.Delete(pdfPath);
                isSuccess = true;
            }

            //return Json(new { IsSuccess = isSuccess, PDFName = fileName }, JsonRequestBehavior.AllowGet);
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    PDFName = fileName
                }),

                ContentType = "application/json",
            };

        }

    }
    internal class JsonRequestBehavior
    {
        public static object AllowGet { get; internal set; }
    }
    public class IbsdtakesgutsQuizInfo
    {
        public string QuesOne { get; set; }
        public string QuesTwo { get; set; }
        public string QuesThree { get; set; }
        public string QuesFour { get; set; }
        public string QuesFive { get; set; }
        public string QuesSix { get; set; }
        public string QuesSeven { get; set; }
        public string QuesEight { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
    }
}
