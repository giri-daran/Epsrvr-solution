using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Pdfa;
using EpiserverBH.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;


namespace EpiserverBH.Controllers.Bauschhealthpap
{
    public class PAPModel
    {
        public string State { get; set; }
        public string Product { get; set; }
        public int HouseHoldNumber { get; set; }
        public string IsEligible { get; set; }
        public string PDFName { get; set; }
        public int income { get; set; }
        public int OOPThreshold { get; set; }
        public string error { get; set; }
    }

    public class PAPController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public PAPController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }


        [HttpGet]
        public IActionResult IsEligible()
        {
            PAPModel objpapModel = new PAPModel();
            string prodt = objpapModel.Product;
            try
            {

                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "GET")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();

                        string _SQLCommand = string.Empty;
                        string s = string.Empty, Product = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        objpapModel.State = _IHttpContextAccessor.HttpContext.Request.Query["state"].ToString();
                        objpapModel.HouseHoldNumber = Convert.ToInt32(HttpContext.Request.Query["HouseHoldNumber"]);
                        objpapModel.Product = _IHttpContextAccessor.HttpContext.Request.Query["Product"].ToString();
                       
                        int AddtionalMembercost = 5140;

                        if (objpapModel.State == "Alaska")
                        {
                            AddtionalMembercost = 6430;
                            s = "AK";
                        }
                        else if (objpapModel.State == "Hawaii")
                        {
                            AddtionalMembercost = 5910;
                            s = "HI";
                        }
                        else
                        {
                            AddtionalMembercost = 5140;
                            s = "All";
                        }

                        if (objpapModel.Product != "Targretin" && objpapModel.Product != "Siliq" && objpapModel.Product != "Syprine" && objpapModel.Product != "Demser" && objpapModel.Product != "Cuprimine")
                        {
                            Product = "All";
                        }
                        else
                        {
                            Product = objpapModel.Product;
                        }

                        if (objpapModel.HouseHoldNumber > 8)
                        {
                            using (SqlConnection connection = new SqlConnection(ConStringnew))
                            {
                                _SQLCommand = string.Format("SELECT Income FROM BH_Poverty_Guidelines WHERE State='{0}' AND Product='{1}' AND Householdsize='{2}'", s, Product, 8);
                                connection.Open();
                                var cmd = connection.CreateCommand();
                                cmd.CommandText = _SQLCommand;
                                cmd.CommandType = System.Data.CommandType.Text;

                                var reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    objpapModel.income = Convert.ToInt32(reader["Income"]) + ((objpapModel.HouseHoldNumber - 8) * AddtionalMembercost);
                                    objpapModel.OOPThreshold = Convert.ToInt32(Convert.ToInt32(reader["Income"]) + (((objpapModel.HouseHoldNumber - 8) * AddtionalMembercost) * 0.05));
                                }

                                connection.Close();
                                connection.Dispose();
                            }
                        }
                        else
                        {
                            using (SqlConnection connection = new SqlConnection(ConStringnew))
                            {
                                _SQLCommand = string.Format("SELECT Income FROM BH_Poverty_Guidelines WHERE State='{0}' AND Product='{1}' AND Householdsize='{2}'", s, Product, objpapModel.HouseHoldNumber);
                                connection.Open();
                                var cmd = connection.CreateCommand();
                                cmd.CommandText = _SQLCommand;
                                cmd.CommandType = System.Data.CommandType.Text;

                                var reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    objpapModel.income = Convert.ToInt32(reader["Income"]);
                                    objpapModel.OOPThreshold = Convert.ToInt32(Convert.ToInt32(reader["Income"]) * 0.05);
                                }

                                connection.Close();
                                connection.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver - BauschHealthpap - IsEligible Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objpapModel.error = ex.Message;
            }

            return Json(objpapModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePDF()
        {
            PAPModel objModel = new PAPModel();
            string PDFName = string.Empty;
            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        Random rnd = new Random();
                        string myRandomNo = Convert.ToString(rnd.Next(10000000, 99999999));

                        string TemplatePdfName = "BauschHealthPAP.pdf";
                        string PdfName = myRandomNo + ".pdf";

                        string pdfFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "Bauschhealthpap", "Template");
                        string templateFilePath = System.IO.Path.Combine(pdfFolderPath, TemplatePdfName);
                        string generatedPath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Bauschhealthpap");
                        string FilePath = System.IO.Path.Combine(generatedPath, PdfName);

                        using (PdfDocument document = new PdfDocument(new PdfReader(templateFilePath), new PdfWriter(FilePath)))
                        {
                            var form = PdfAcroForm.GetAcroForm(document, false);

                            form.GetField("AppId1").SetValue("AppId1", myRandomNo);
                            form.GetField("AppId2").SetValue("AppId2", myRandomNo);
                            form.GetField("AppId3").SetValue("AppId3", myRandomNo);
                            form.GetField("AppId4").SetValue("AppId4", myRandomNo);
                            form.GetField("AppId5").SetValue("AppId5", myRandomNo);

                            form.FlattenFields();
                            document.Close();
                        }

                        HtmlHelpers.UploadPdfinCms(FilePath, "BHpap-Eligible-pdf");

                        System.IO.File.Delete(FilePath);

                        objModel.PDFName = myRandomNo;
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver - BauschHealthpap - CreatePDF Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    //IsSuccess = isSuccess,                    
                    PDFName = objModel.PDFName

                }),

                ContentType = "application/json",
            };
        }

    }

    internal class JsonRequestBehavior
    {
        public static object AllowGet { get; internal set; }
    }
}
