using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.Trulance;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EpiserverBH.Controllers.Trulance
{
    public class TrulanceDocDiscGuideElementBlockController : BlockComponent<TrulanceDocDiscGuideElementBlock>
    {
        private static TrulanceDocDiscGuideElementBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IHttpContextAccessor _IHttpContextAccessor;
        private readonly IConfiguration _configuration;

        protected override IViewComponentResult InvokeComponent(TrulanceDocDiscGuideElementBlock currentContent)
        {
            _block = currentContent;
            return View("/Views/Shared/Blocks/TrulanceDocDiscGuideElementBlock.cshtml", _block);
        }

        public TrulanceDocDiscGuideElementBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor _iHttpContextAccessor, IConfiguration _iConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = _iHttpContextAccessor;
            _configuration = _iConfiguration;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePDF(Request request)
        {
            DDGinfo datainfo = new DDGinfo();
            var symptom = string.Empty;
            string[] dignosed = { "Not diagnosed with CIC or IBS-C", "Diagnosed with IBS-C", "Diagnosed with CIC" };
            string[] satisfaction = { "Not at all satisfied", "Not really satisfied", "Neutral", "Somewhat satisfied", "Very satisfied" };
            ViewDataDictionary myViewData = null;

            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    try
                    {
                        datainfo.strQuestion11 = request.Question11;
                        datainfo.strQuestion12 = request.Question12;
                        datainfo.strQuestion13 = request.Question13;
                        datainfo.strQuestion14 = request.Question14;
                        datainfo.strQuestion15 = request.Question15;
                        datainfo.strQuestion16 = request.Question16;

                        datainfo.strQuestion2 = request.Question2;

                        datainfo.strQuestion41First = request.Question41First;
                        datainfo.strQuestion41Next = request.Question41Next;

                        datainfo.strQuestion42First = request.Question42First;
                        datainfo.strQuestion42Next = request.Question42Next;

                        datainfo.strQuestion51 = request.Question51;

                        datainfo.strQuestion52 = request.Question52;

                        datainfo.strQuestion6 = request.Question6;

                        datainfo.strQuestion7 = request.Question7;

                        datainfo.strSlider = request.slidervalue;

                        string RadioButton = request.slidervalue;

                        if (!string.IsNullOrEmpty(datainfo.strQuestion11))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion11 + Environment.NewLine;
                        }

                        if (!string.IsNullOrEmpty(datainfo.strQuestion12))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion12 + Environment.NewLine;
                        }
                        
                        if (!string.IsNullOrEmpty(datainfo.strQuestion13))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion13 + Environment.NewLine;
                        }
                        
                        if (!string.IsNullOrEmpty(datainfo.strQuestion14))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion14 + Environment.NewLine;
                        }
                        
                        if (!string.IsNullOrEmpty(datainfo.strQuestion15))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion15 + Environment.NewLine;
                        }
                        
                        if (!string.IsNullOrEmpty(datainfo.strQuestion16))
                        {
                            symptom += "\u2022  " + datainfo.strQuestion16;
                        }

                        request.Symptoms = symptom.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");

                        string about = "\u2022  Dealing with symptoms for " + datainfo.strQuestion2 + Environment.NewLine + "\u2022  " + dignosed[Convert.ToInt32(datainfo.strQuestion7)];
                        request.About = about.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");

                        string pastraw = string.Empty;

                        if (!string.IsNullOrEmpty(datainfo.strQuestion41First))
                        {
                            string[] pastfirst = datainfo.strQuestion41First.Split('|');
                            for (int i = 0; i < pastfirst.Length; i++)
                            {
                                pastraw += "\u2022  " + pastfirst[i] + Environment.NewLine;
                            }
                        }

                        if (!string.IsNullOrEmpty(datainfo.strQuestion41Next))
                        {
                            string[] pastsecond = datainfo.strQuestion41Next.Split('|');
                            for (int i = 0; i < pastsecond.Length; i++)
                            {
                                pastraw += "\u2022  " + pastsecond[i] + Environment.NewLine;
                            }
                        }

                        string past = pastraw;

                        request.Past = pastraw.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");
                        if (request.Past.EndsWith("|"))
                        {
                            request.Past = request.Past.Substring(0, request.Past.Length - 1);
                        }

                        string currentlyCombine = string.Empty;
                        string currentlyraw = string.Empty;
                        if (!string.IsNullOrEmpty(datainfo.strQuestion42First))
                        {
                            string[] currentfirst = datainfo.strQuestion42First.Split('|');
                            for (int i = 0; i < currentfirst.Length; i++)
                            {
                                currentlyraw += "\u2022  " + currentfirst[i] + Environment.NewLine;
                            }
                        }

                        if (!string.IsNullOrEmpty(datainfo.strQuestion42Next))
                        {
                            string[] currentsecond = datainfo.strQuestion42Next.Split('|');
                            for (int i = 0; i < currentsecond.Length; i++)
                            {
                                currentlyraw += "\u2022  " + currentsecond[i] + Environment.NewLine;
                            }
                        }

                        string currently = currentlyraw;
                        request.Currently = currently.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");
                        if (request.Currently.EndsWith("|"))
                        {
                            request.Currently = request.Currently.Substring(0, request.Currently.Length - 1);
                        }

                        string successrow = string.Empty;
                        string successactual = datainfo.strQuestion6.Replace(", ", " / ");
                        string[] successvalue = successactual.Split('|');

                        for (int i = 0; i < successvalue.Length; i++)
                        {
                            successrow += "\u2022  " + successvalue[i].Replace(" /", ",") + Environment.NewLine;
                        }

                        string success = successrow;

                        request.Success = successrow.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");
                        if (request.Success.EndsWith("|"))
                        {
                            request.Success = request.Success.Substring(0, request.Success.Length - 1);
                        }

                        string[] statusraw;
                        string statusactual = string.Empty;
                        if (!string.IsNullOrEmpty(datainfo.strQuestion52))
                        {
                            statusraw = datainfo.strQuestion52.Split('|');
                            for (int i = 0; i < statusraw.Length; i++)
                            {
                                statusactual += "\u2022  " + statusraw[i] + Environment.NewLine;
                            }
                        }

                        int statusvalue = Convert.ToInt32(datainfo.strQuestion51);
                        string status;
                        string basetext;
                        if (statusvalue > 2)
                        {
                            status = "\u2022  " + satisfaction[statusvalue] + Environment.NewLine + "\u2022  May be interested in other treatment options";
                            basetext = "Based on your answers, it seems like you are headed in the right direction.";
                            statusactual = status;
                        }
                        else if (statusvalue <= 1)
                        {
                            status = statusactual;
                            basetext = "Based on your answers, it seems like you are unsatisfied with your current treatment.";
                        }
                        else if (statusvalue == 2)
                        {
                            status = statusactual;
                            basetext = "Based on your answers, it seems like you have parts of your treatment down, but you are still looking for something that works for you.";
                        }
                        else
                        {
                            status = satisfaction[statusvalue] + Environment.NewLine + "May be interested in other treatment options";
                            basetext = "Based on your answers, it seems like you are headed in the right direction.";
                        }

                        // If 52 is empty, we need to display status in the partial view html
                        request.Status = status.Replace("\u2022  ", "").Replace(Environment.NewLine, "|");
                        if (request.Status.EndsWith("|"))
                        {
                            request.Status = request.Status.Substring(0, request.Status.Length - 1);
                        }

                        request.StatusVal = statusvalue;

                        //PDF Generation Starts
                        string pdfTemplatePath = string.Empty;
                        string pdfFilePath = string.Empty;
                        string TemplatePdfName = "ddg.pdf";
                        string pdfFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "Trulance-DTC", "Template");
                        pdfTemplatePath = System.IO.Path.Combine(pdfFolderPath, TemplatePdfName);
                        string currentDateTime = DateTime.Now.ToString("MMddyyy-HHmmssff");
                        string pdfFileName = "cic-discussion-guide-" + currentDateTime + ".pdf";
                        string generatedPath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Trulance");
                        pdfFilePath = System.IO.Path.Combine(generatedPath, pdfFileName);


                        using (PdfDocument document = new PdfDocument(new PdfReader(pdfTemplatePath), new PdfWriter(pdfFilePath)))
                        {
                            var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                            var field1 = form.GetField("TextField7");
                            var field2 = form.GetField("Aboutme");
                            var field3 = form.GetField("Symptoms");
                            PdfButtonFormField field4 = form.GetField("RadioButton" + Convert.ToInt32(RadioButton)) as PdfButtonFormField;
                            var field5 = form.GetField("Past");
                            var field6 = form.GetField("Currently");
                            var field7 = form.GetField("Status");
                            var field8 = form.GetField("Successmeans");

                            field1?.SetValue("TextField7", basetext);
                            field2?.SetValue("Aboutme", about);
                            field3?.SetValue("Symptoms", symptom);
                            field4?.SetValue("0", true);
                            field5?.SetValue("Past", past);
                            field6?.SetValue("Currently", currently);
                            field7?.SetValue("Status", status);
                            field8?.SetValue("Successmeans", success);

                            form.FlattenFields();
                            document.Close();
                        }

                        HtmlHelpers.UploadPdfinCms(pdfFilePath, "DDG-PDF");
                        System.IO.File.Delete(pdfFilePath);

                        bool isSuccess = true;
                        request.PDFName = "/siteassets/DDG-PDF/" + pdfFileName;
                        request.IsSuccess = isSuccess;
                    }
                    catch (Exception ex)
                    {
                        //_logger.Log(NuGet.MessageLevel.Error, ex.StackTrace);
                        request.IsSuccess = false;
                        MailService.SendAlert(_configuration, "no-reply@valeantonline.com", "Episerver - Trulance Doctor Discussion Guide Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                    }

                    myViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
                    myViewData.Model = request;

                    return new PartialViewResult
                    {
                        ViewName = "_TrulanceDocDiscGuideResponseView",
                        ViewData = myViewData,
                    };

                }
            }

            return new PartialViewResult
            {
                ViewName = "_TrulanceDocDiscGuideResponseView",
                ViewData = myViewData,
            };
        }
    }

    public class DDGinfo
    {
        public string strQuestion11 { get; set; }
        public string strQuestion12 { get; set; }
        public string strQuestion13 { get; set; }
        public string strQuestion14 { get; set; }
        public string strQuestion15 { get; set; }
        public string strQuestion16 { get; set; }
        public string strQuestion2 { get; set; }
        public string strQuestion41First { get; set; }
        public string strQuestion41Next { get; set; }
        public string strQuestion42First { get; set; }
        public string strQuestion42Next { get; set; }
        public string strQuestion51 { get; set; }
        public string strQuestion52 { get; set; }
        public string strQuestion6 { get; set; }
        public string strQuestion7 { get; set; }
        public string strSlider { get; set; }
        public string filepath { get; set; }
    }

    public class Request
    {
        public string Question11 { get; set; }
        public string Question12 { get; set; }
        public string Question13 { get; set; }
        public string Question14 { get; set; }
        public string Question15 { get; set; }
        public string Question16 { get; set; }
        public string Question2 { get; set; }
        public string Question41First { get; set; }
        public string Question41Next { get; set; }
        public string Question42First { get; set; }
        public string Question42Next { get; set; }
        public string Question51 { get; set; }
        public string Question52 { get; set; }
        public string Question6 { get; set; }
        public string Question7 { get; set; }
        public string slidervalue { get; set; }
        public string PDFName { get; set; }
        public string About { get; set; }
        public string Past { get; internal set; }
        public string Currently { get; internal set; }
        public string Success { get; internal set; }
        public string Symptoms { get; set; }
        public bool IsSuccess { get; internal set; }
        public int StatusVal { get; set; }
        public string Status { get; set; }
    }

}