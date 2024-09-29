using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Pdfa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers
{
    public class SADQuizModuleController : BlockComponent<SADQuizModuleElementBlock>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        //public object Server { get; private set; }
        //public object JsonRequestBehavior { get; private set; }

        public SADQuizModuleController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        protected override IViewComponentResult InvokeComponent(SADQuizModuleElementBlock currentContent)
        {
            return View(currentContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePDF(QuizInfo qi)
        {
            bool isSuccess = false;
            string errorMessage = string.Empty;
            string fileName = string.Empty;

            if (qi != null)
            {
                string pdfFilePath = string.Empty;
                string pdfFileName = string.Empty;
                string pdfTemplatePath = string.Empty;

                pdfFilePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Aplenzin");
                pdfTemplatePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "Aplenzin-DTC", "SAD-Quiz", "pdf", "APLENZIN-SAD-Quiz-Responses.pdf");

                pdfFileName = "APLENZIN-SAD-Quiz-Responses-" + DateTime.Now.ToString("yyyymmdd-HHmmssff") + ".pdf";
                pdfFilePath = System.IO.Path.Combine(pdfFilePath, pdfFileName);

                using (PdfDocument document = new PdfDocument(new PdfReader(pdfTemplatePath), new PdfWriter(pdfFilePath)))
                {   
                    var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                    PopulateFields(qi.Q1, form);
                    PopulateFields(qi.Q2, form);
                    PopulateFields(qi.Q3, form);
                    PopulateFields(qi.Q4, form);
                    PopulateFields(qi.Q5, form);
                    PopulateFields(qi.Q6, form);
                    PopulateFields(qi.Q7, form);
                    PopulateFields(qi.Q8, form);
                    PopulateFields(qi.Q9, form);
                    PopulateFields(qi.Q10, form);
                    PopulateFields(qi.Q11, form);
                    PopulateFields(qi.Q12, form);
                    PopulateFields(qi.Q13, form);

                    form.FlattenFields();
                    document.Close();
                }

                HtmlHelpers.UploadPdfinCms(pdfFilePath, "SAD-Quiz-PDF");
                isSuccess = true;
                System.IO.File.Delete(pdfFilePath);
                fileName = pdfFileName;

            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { IsSuccess = isSuccess, PDFName = fileName, ErrorMessage = errorMessage }),
                ContentType = "application/json",
            };

        }

        

        

    private void PopulateFields(string responseForFields, PdfAcroForm form)
        {
            if (form != null && !string.IsNullOrWhiteSpace(responseForFields))
            {
                if (responseForFields.Contains(","))
                {
                    string[] multivalues = responseForFields.Split(new string[] { "," }, StringSplitOptions.None);
                    // Loop over the array.
                    foreach (string mv in multivalues)
                    {
                        var field = form.GetField(mv);
                        if (field != null)
                        {
                            field.SetValue(mv, "X");
                        }
                    }
                }
                else
                {
                    var field = form.GetField(responseForFields);
                    if (field != null)
                    {
                        field.SetValue(responseForFields, "X");
                    }
                }
            }
        }
    }

   
}