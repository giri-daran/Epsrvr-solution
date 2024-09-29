using EpiserverBH.Helpers;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace EpiserverBH.Controllers.BauschHealth
{
    public class BauschHealthController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public BauschHealthController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProductList()
        {
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {

                        string ConStringnew = GetConnectionString();

                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        _SQLCommand = "SELECT 'Please select' AS ProductName, 0 AS Id UNION SELECT Replace(Replace(ProductName,'&#8482;',''),'&#174;','®') AS ProductName, Id FROM R2I_Valeant_ProductTable WHERE ISNULL(ProductName,'') <> '' ORDER BY 1";
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objBHModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GetProductList Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { objBHModel }),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        public ActionResult GetCategory()
        {
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];
                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        _SQLCommand = "SELECT 'Please select' AS Category, 'PleaseSelect' AS Category UNION SELECT DISTINCT Category, Replace(Category,' ','-') AS Category FROM R2I_Valeant_ProductTable WHERE ISNULL(ProductName,'') <> '' ORDER BY 1";
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objBHModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                 MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GetCategory Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { objBHModel }),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        public ActionResult GetClassification()
        {
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        _SQLCommand = "SELECT 'Please select' AS Classification, 'PleaseSelect' AS Classification UNION SELECT DISTINCT Classification AS Classification, Classification AS Classification FROM R2I_Valeant_ProductTable WHERE ISNULL(Classification,'') <> '' ORDER BY 1";
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objBHModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GetClassification Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objBHModel),
                ContentType = "application/json",
            };

        }

        [HttpPost]
        public ActionResult FilterGrid(int Id, string Category, string Classification, int PageSize, int PageNumber)
        {
            BHModel bH = new BHModel();
            bH.Id = Id;
            bH.Category = Category;
            bH.Classification = Classification;
            bH.PageSize = PageSize;
            bH.PageNumber = PageNumber;
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);
                        string lcCondition1 = "", lcCondition2 = "", lcCondition3 = "";
                        //string lcTable = "R2I_Valeant_ProductTable ", lcSelect = "SELECT * ", lcOrderby = " ORDER BY ProductName asc";
                        //string lcFrom = "FROM ", lcWhere = "WHERE ";

                        if (!hasSpecialChar(bH.Category) && !hasSpecialChar(bH.Classification))
                        {
                            if (bH.Id != 0)
                            {
                                lcCondition1 = "Id = " + bH.Id + " AND ";
                            }

                            if (!string.IsNullOrEmpty(bH.Category) && bH.Category != "All")
                            {
                                lcCondition2 = "Category = '" + bH.Category + "' AND ";
                            }

                            if (!string.IsNullOrEmpty(bH.Classification) && bH.Classification != "All")
                            {
                                lcCondition3 = " Classification = '" + bH.Classification + "'";
                            }

                            string lcCond = lcCondition1 + lcCondition2 + lcCondition3;
                            lcCond = lcCond.EndsWith(" AND ") ? lcCond.Substring(0, lcCond.Length - 5) : lcCond;

                            lcCond = (lcCond != "" ? ("WHERE " + lcCond) : lcCond);
                            ///_SQLCommand = lcSelect + lcFrom + lcTable + lcCond + lcOrderby;
                            _SQLCommand = "SELECT * FROM R2I_Valeant_ProductTable " + lcCond + " ORDER BY ProductName asc";

                            SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dataTable);
                            conn.Close();
                            da.Dispose();

                            List<DataRow> listData = dataTable.AsEnumerable().ToList();
                            if (listData.Count > 0)
                            {
                                DataTable dt = dataTable.AsEnumerable().ToList().Skip(bH.PageSize * (bH.PageNumber - 1)).Take(bH.PageSize).CopyToDataTable();
                                objBHModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dt);
                                objBHModel.TotalPages = Convert.ToInt32(Math.Ceiling((double)listData.Count() / bH.PageSize));
                                objBHModel.CurrentPage = bH.PageNumber;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver - BauschHealth - FilterGrid Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objBHModel),
                ContentType = "application/json",
            };
        }

        public string DataTableToJSONWithStringBuilderForGrid(DataTable table)
        {
            var JSONString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\"");
                        }
                    }

                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }

                JSONString.Append("]");
            }

            return JSONString.ToString();
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }

                    if (i == table.Rows.Count - 1)
                    {
                        //  JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append(",");
                    }
                }

                JSONString.Append("]");
            }

            return JSONString.ToString();
        }

        [HttpPost]
        public ActionResult GetMedicalBU()
        {
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        _SQLCommand = "SELECT DISTINCT BusinessUnit FROM MIRF_Products where  BusinessUnit != 'Bausch + Lomb'";
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objBHModel.JsonResults = DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GetMedicalBU Function Exception", ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objBHModel),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        public ActionResult GetInquireProduct(string BU)
        {
            BHModel objBHModel = new BHModel();

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest" && !hasSpecialChar(BU))
                    {
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        _SQLCommand = string.Format("SELECT DISTINCT ProductName FROM MIRF_Products WHERE BusinessUnit = '{0}'", BU);
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objBHModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GetInquireProduct Function Exception" + BU, ex.Message, "websitealerts@bauschhealth.com");
                objBHModel.error = ex.Message;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objBHModel),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        public ActionResult GeneratePDF(MedicalForminfo datainfo)
        {
            bool isSuccess = false;
            string pdfFilePath = string.Empty;
            string pdfFileName = string.Empty;
            string pdfTemplatePath = string.Empty;
            string lcRandNo;
            DataTable dataTable = new DataTable();
            string recepient = "";
            if (datainfo.ConfNameandYear is null)
                datainfo.ConfNameandYear = "";
            if (datainfo.txtAddressLine2 is null)
                datainfo.txtAddressLine2 = "";
            if (datainfo.txtFaxnumber is null)
                datainfo.txtFaxnumber = "";

            try
            {
                string method = HttpContext.Request.Method;
                string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();

                        try
                        {
                            using (SqlConnection connection = new SqlConnection(ConStringnew))
                            {
                                connection.Open();

                                string commandText1 = "SELECT 100000 + (CONVERT(INT, CRYPT_GEN_RANDOM(2)) % 100000), 100000 + (CONVERT(INT, RAND()*1000000) % 100000), 100000 + (ABS(CHECKSUM(NEWID())) % 100000) as RanNo";
                                using (SqlCommand command1 = new SqlCommand(commandText1, connection))
                                {
                                    lcRandNo = Convert.ToString(command1.ExecuteScalar());
                                    datainfo.FieldValue = lcRandNo;
                                }

                                string commandText2 = "SELECT Contact FROM MIRF_Products WHERE ProductName = '" + datainfo.ddlProduct + "'";
                                using (SqlCommand command2 = new SqlCommand(commandText2, connection))
                                {
                                    recepient = Convert.ToString(command2.ExecuteScalar());
                                }

                                using (SqlCommand cmd = new SqlCommand("SP_Insert_BHMedicalInquiry", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@InquiryNo", Convert.ToInt32(lcRandNo));
                                    cmd.Parameters.AddWithValue("@Email", datainfo.txtEmail ?? "");
                                    cmd.Parameters.AddWithValue("@BusinessUnit", datainfo.ddlBU ?? "");
                                    cmd.Parameters.AddWithValue("@ProductName", datainfo.ddlProduct ?? "");
                                    cmd.Parameters.AddWithValue("@IsDirected", datainfo.IsDirected ?? "");
                                    cmd.Parameters.AddWithValue("@ConfNameandYear", Convert.ToString(datainfo.ConfNameandYear).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@MedicalInquiry", Convert.ToString(datainfo.MedicalInquiry).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@Designation", datainfo.ddlBest ?? "");
                                    cmd.Parameters.AddWithValue("@Salutation", datainfo.ddlSalutation ?? "");
                                    cmd.Parameters.AddWithValue("@FirstName", Convert.ToString(datainfo.txtFirstName).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@LastName", Convert.ToString(datainfo.txtLastName).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@Address", Convert.ToString(datainfo.txtAddress).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@AddressLine2", Convert.ToString(datainfo.txtAddressLine2).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@City", Convert.ToString(datainfo.txtCity).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@State", Convert.ToString(datainfo.txtState).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@ZIPCode", Convert.ToInt32(datainfo.txtZIPCode));
                                    cmd.Parameters.AddWithValue("@Phone", Convert.ToString(datainfo.txtPhone).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@Faxnumber", Convert.ToString(datainfo.txtFaxnumber).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");
                                    cmd.Parameters.AddWithValue("@ContactSource", datainfo.ddlContactSource ?? "");
                                    cmd.Parameters.AddWithValue("@ElectronicSignature", Convert.ToString(datainfo.txtElectronicSignature).Replace("\n", "").Replace("\r", "").Replace("|", "") ?? "");

                                    cmd.ExecuteNonQuery();

                                    isSuccess = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GeneratePDF SQL Exception", ex.StackTrace, "websitealerts@bauschhealth.com");
                            isSuccess = false;
                        }

                        string TemplatePdfName = "MIRF.pdf";   
                        string pdfFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Assets", "Bauschhealth", "Template");
                        pdfTemplatePath = System.IO.Path.Combine(pdfFolderPath, TemplatePdfName);
                        string currentDateTime = DateTime.Now.ToString("MMddyyy-HHmmssff");
                        pdfFileName = "BauschHealth-MedicalInquiry-" + currentDateTime + ".pdf";
                        string generatedPath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "BauschHealth");                        
                        pdfFilePath = System.IO.Path.Combine(generatedPath, pdfFileName);


                        using (PdfDocument document = new PdfDocument(new PdfReader(pdfTemplatePath), new PdfWriter(pdfFilePath)))
                        {
                            var form = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                            form.GetField("txtBU").SetValue("txtBU", datainfo.ddlBU);
                            form.GetField("txtBest").SetValue("txtBest", datainfo.ddlBest);
                            form.GetField("txtProduct").SetValue("txtProduct", datainfo.ddlProduct);
                            form.GetField("txtIsDirected").SetValue("txtIsDirected", datainfo.IsDirected);
                            form.GetField("txtConfNameandYear").SetValue("txtConfNameandYear", Convert.ToString(datainfo.ConfNameandYear));
                            form.GetField("txtProduct").SetValue("txtProduct", datainfo.ddlProduct);
                            form.GetField("Question").SetValue("Question", datainfo.MedicalInquiry);
                            form.GetField("txtResponse").SetValue("txtResponse", datainfo.ddlContactSource);
                            form.GetField("txtSalutation").SetValue("txtSalutation", datainfo.ddlSalutation);
                            form.GetField("txtFirstName").SetValue("txtFirstName", datainfo.txtFirstName);
                            form.GetField("txtLastName").SetValue("txtLastName", datainfo.txtLastName);
                            form.GetField("txtAddress").SetValue("txtAddress", datainfo.txtAddress);
                            form.GetField("txtAddress2").SetValue("txtAddress2", datainfo.txtAddressLine2);
                            form.GetField("txtState").SetValue("txtState", datainfo.txtState);
                            form.GetField("txtCity").SetValue("txtCity", datainfo.txtCity);
                            form.GetField("txtZipCode").SetValue("txtZipCode", datainfo.txtZIPCode);
                            form.GetField("txtEmail").SetValue("txtEmail", datainfo.txtEmail);
                            form.GetField("txtPhone").SetValue("txtPhone", datainfo.txtPhone);
                            form.GetField("txtFaxNumber").SetValue("txtFaxNumber", Convert.ToString(datainfo.txtFaxnumber));
                            form.GetField("txtHCPSignature").SetValue("txtHCPSignature", datainfo.txtElectronicSignature);
                            form.GetField("txtApplicationId").SetValue("txtApplicationId", datainfo.FieldValue);

                            form.FlattenFields();
                            document.Close();
                        }

                        datainfo.FilePath = pdfFilePath;
                        HtmlHelpers.UploadPdfinCms(pdfFilePath, "BHMedicalInquiry");


                        isSuccess = true;

                        sendmail(datainfo, recepient);
                        System.IO.File.Delete(pdfFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", "Episerver CMS12 - BauschHealth - GeneratePDF Function Exception", ex.StackTrace+ " pdfFilePath: "+pdfFilePath+" pdfTemplatePath:"+pdfTemplatePath, "websitealerts@bauschhealth.com");
                isSuccess = false;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { IsSuccess = isSuccess, PdfFileName = pdfFileName }),
                ContentType = "application/json",
            };

        }

           public void sendmail(MedicalForminfo datainfo, string recepient)
        {
            try
            {
                string hosts = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Host");
                int port = Configuration.GetValue<int>("EPiServer:CMS:Smtp:Network:Port");
                string fromAddress = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:NotificationEmailAddress");
                string userName = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:UserName");
                string password = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Password");

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = hosts;
                smtpClient.Port = Convert.ToInt16(port);
                smtpClient.Credentials = new NetworkCredential(userName, password);
                smtpClient.EnableSsl = true;


                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("no-reply@valeantonline.com");
                    mailMessage.Subject = datainfo.FieldValue + " - Thank you for submitting your recent inquiry regarding " + datainfo.ddlProduct;
                    mailMessage.Body = "<p><b>The Bausch Health Medical Information Department has received your specific unsolicited medical information request.</b></p><p>If you have requested a response by phone, we will attempt to contact you three times.</p><p>If you have any further questions or comments, please contact the Bausch Health Medical Information Department at 877-361-2719.</p>";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(datainfo.txtEmail);
                    smtpClient.Send(mailMessage);
                }

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("no-reply@valeantonline.com");
                    mailMessage.Subject = "Website Medical Inquiry Form Submission – " + datainfo.ddlProduct + " - " + datainfo.FieldValue;
                    mailMessage.Body = "<p>Medical Inquiry Information:</p><p>Select the Division/Business Unit*:" + datainfo.ddlBU + "</p><p>Select the product for your Inquiry*:" + datainfo.ddlProduct + "</p><p>Please indicate if this medical inquiry was related to your attendance at a professional congress/conference*:" + datainfo.IsDirected + "</p><p>Congress/Conference Name and Year*:" + Convert.ToString(datainfo.ConfNameandYear) + "</p><p>Medical Inquiry*:" + datainfo.MedicalInquiry + "</p>" +
                            "<p>Contact Information:</p><p>Professional Designation*:" + datainfo.ddlBest + "</p><p>Title*:" + datainfo.ddlSalutation + "</p><p>First Name*:" + datainfo.txtFirstName + "</p><p>Last Name*:" + datainfo.txtLastName + "</p><p>Office/Institution Address*:" + datainfo.txtAddress + "</p><p>Office/Institution Address 2:" + Convert.ToString(datainfo.txtAddressLine2) + "</p><p>City*:" + datainfo.txtCity + "</p><p>State*:" + datainfo.txtState + "</p><p>Zip Code*:" + datainfo.txtZIPCode + "</p><p>Email*:" + datainfo.txtEmail + "</p><p>Phone*:" + datainfo.txtPhone + "</p><p>Fax number:" + Convert.ToString(datainfo.txtFaxnumber) + "</p><p>Please indicate how you would like your response to your question(s) delivered*:" + datainfo.ddlContactSource + "</p><p>HCP Electronic Signature*:" + datainfo.txtElectronicSignature + "</p>";
                    mailMessage.IsBodyHtml = true;

                    string[] strRecipient = recepient.Split(';');
                    strRecipient = strRecipient.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    foreach (string lcRecepient in strRecipient)
                    {
                        mailMessage.To.Add(new MailAddress(lcRecepient));
                    }

                    // string fileName = System.IO.Path.GetFileName(datainfo.FilePath);
                    //var attachment = new Attachment(datainfo.FilePath);
                    // mailMessage.Attachments.Add(attachment);

                    string scrCSV = "InquiryNo|DateSubmitted|Email|BusinessUnit|ProductName|IsDirected|ConfNameandYear|MedicalInquiry|Designation|Salutation|FirstName|LastName|Address|City|State|ZIPCode|Phone|Faxnumber|ContactSource|ElectronicSignature";
                    scrCSV = scrCSV + Environment.NewLine +
                       datainfo.FieldValue + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + datainfo.txtEmail + "|" + datainfo.ddlBU + "|" + datainfo.ddlProduct + "|" + datainfo.IsDirected + "|" + Convert.ToString(datainfo.ConfNameandYear).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.MedicalInquiry).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + datainfo.ddlBest + "|" + datainfo.ddlSalutation + "|" + Convert.ToString(datainfo.txtFirstName).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.txtLastName).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.txtAddress + " " + datainfo.txtAddressLine2).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.txtCity).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + datainfo.txtState + "|" +
                       Convert.ToString(datainfo.txtZIPCode).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.txtPhone).Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + Convert.ToString(datainfo.txtFaxnumber) + "|" + datainfo.ddlContactSource.Replace("\n", "").Replace("\r", "").Replace("|", "") + "|" + datainfo.txtElectronicSignature.Replace("\n", "").Replace("\r", "").Replace("|", "");

                    using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(scrCSV)))
                    {
                        Attachment attachment2 = new Attachment(stream, new System.Net.Mime.ContentType("text/csv"));
                        attachment2.Name = "MIRF-Inquiry-Report.csv";
                        mailMessage.Attachments.Add(attachment2);
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //MailService.SendAlert(Configuration,"no-reply@email.bauschhealth.com", "Episerver - BauschHealth - sendmail Function Exception", ex.StackTrace, "websitealerts@bauschhealth.com");

            }

        }


        public bool hasSpecialChar(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string specialChar = @"\|!#$%&/()=?»«@£§€{};'<>_,";
                foreach (var item in specialChar)
                {
                    if (input.Contains(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class BHModel
    {
        public string error { get; set; }
        public string JsonResults { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int Id { get; set; }
        public string Category { get; set; }
        public string Classification { get; set; }
    }

    public class MedicalForminfo
    {
        public string FieldValue { get; set; }
        public string ddlBU { get; set; }
        public string ddlBest { get; set; }
        public string ddlProduct { get; set; }
        public string IsDirected { get; set; }
        public string ConfNameandYear { get; set; }
        public string MedicalInquiry { get; set; }
        public string ddlContactSource { get; set; }
        public string ddlSalutation { get; set; }
        public string txtFirstName { get; set; }
        public string txtLastName { get; set; }
        public string txtAddress { get; set; }
        public string txtAddressLine2 { get; set; }
        public string txtCity { get; set; }
        public string txtState { get; set; }
        public string txtZIPCode { get; set; }
        public string txtEmail { get; set; }
        public string txtPhone { get; set; }
        public string txtFaxnumber { get; set; }
        public string txtElectronicSignature { get; set; }
        public string AdminEmail { get; set; }
        public string FilePath { get; set; }

    }

}