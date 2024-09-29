using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.SearchBlock;
using EpiserverBH.Models.Blocks.SearchResultBlock;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;



namespace EpiserverBH.Controllers
{
    public class SearchResultBlockController : BlockComponent<SearchResult>
    {
        private static SearchResult _block;

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public SearchResultBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }

        protected override IViewComponentResult InvokeComponent(SearchResult currentContent)
        {
            _block = currentContent;

            if (currentContent != null)
            {
                var assetFolder = string.Format("Assets/SearchResultBlock/Templates/{0}/", currentContent.TemplateName);
                var assetsPath = Path.Combine(_hostingEnvironment.WebRootPath,assetFolder);

                var viewFilePath = System.IO.Path.Combine(assetsPath, "Result.htm");

                if (System.IO.File.Exists(viewFilePath))
                {
                    ViewBag.HtmlContent = System.IO.File.ReadAllText(viewFilePath);
                }
                else
                {
                    ViewBag.HtmlContent = "";
                }
            }

            return View("/Views/Shared/Blocks/SearchResult.cshtml", _block);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SearchResult(string ProductDispVal, string TA, string keyword)
        {
            string lcResult = string.Empty;
            bool lbSuccess = false;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string ConStringnew = GetConnectionString();
                        string _TableName = _block.TableName.ToString();
                       // string ConStringnew = string.Empty;
                        

                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        if (string.IsNullOrEmpty(keyword))
                        {
                            _SQLCommand = string.Format("SELECT Type, Title, Author, Link, CongressInfo FROM {0} where ProductDose like '%{1}%' and TA like '%{2}%' order by Date desc, Author asc, Title asc", _TableName, ProductDispVal.Replace("®", "&#174;"), TA);
                        }
                        else
                        {
                            keyword = keyword.Contains("'") ? keyword.Replace("'", "''") : keyword;
                            _SQLCommand = string.Format("SELECT Type, Title, Author, Link, CongressInfo FROM {0} where ProductDose like '%{1}%' and TA like '%{2}%' and Title like '%{3}%' order by Date desc, Author asc, Title asc", _TableName, ProductDispVal.Replace("®", "&#174;"), TA, keyword);
                        }

                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        lcResult = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                        lbSuccess = !string.IsNullOrEmpty(lcResult);
                    }
                }
            }
            catch (Exception ex)
            {
                string site = "SalixMedical.com";
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", site + " Search Result Block Exception", ex.Message, "websitealerts@bauschhealth.com");
                lbSuccess = false;
            }

            // return Json(new { IsSuccess = lbSuccess, JsonResults = lcResult }, JsonRequestBehavior.AllowGet);
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = lbSuccess,
                    JsonResults = lcResult

                }),

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


    }
}