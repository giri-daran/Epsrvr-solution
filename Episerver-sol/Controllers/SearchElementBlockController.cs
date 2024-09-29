using EPiServer.Web.Mvc;
using EpiserverBH.Controllers.Bauschhealthpap;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.HKLocationBlock;
using EpiserverBH.Models.Blocks.SearchBlock;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;



namespace EpiserverBH.Controllers
{
    public class SearchElementBlockController : BlockComponent<SearchElementBlock>
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public SearchElementBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }


        private static SearchElementBlock _block;
        protected override IViewComponentResult InvokeComponent(SearchElementBlock currentContent)
        {
            _block = currentContent;

            if (currentContent != null)
            {
                var assetFolder = string.Format("Assets/SearchBlock/Templates/{0}/", currentContent.TemplateName);
                var assetsPath = Path.Combine(_hostingEnvironment.WebRootPath, assetFolder);

                var viewFilePath = System.IO.Path.Combine(assetsPath, "view.htm");
               
                if (System.IO.File.Exists(viewFilePath))
                {
                    ViewBag.HtmlContent = System.IO.File.ReadAllText(viewFilePath);
                }
                else
                {
                    ViewBag.HtmlContent = "";
                }
            }

            return View("/Views/Shared/Blocks/SearchElementBlock.cshtml", _block);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProductList()
        {
            SearchModel objSearchModel = new SearchModel();

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

                        _SQLCommand = string.Format("SELECT DISTINCT ProductValue, ProductDisplayName FROM {0} ORDER BY ProductDisplayName desc", _TableName);
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objSearchModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                string site = "SalixMedical.com";
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", site + " Search Block Exception", ex.Message, "websitealerts@bauschhealth.com");
                objSearchModel.error = ex.Message;
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objSearchModel),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTA(string ProductDispName)
        {
            SearchModel objSearchModel = new SearchModel();
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

                        _SQLCommand = string.Format("SELECT DISTINCT TA, Replace(TADisplayName, '&#174;','®') AS TADisplayName FROM {0} WHERE ProductDisplayName = '{1}' AND TA IN (SELECT value FROM STRING_SPLIT (TA, ','))", _TableName, ProductDispName.Replace("®", "&#174;"));
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        objSearchModel.JsonResults = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                string site = "SalixMedical.com";
                MailService.SendAlert(Configuration,"no-reply@valeantonline.com", site + " Search Block Exception", ex.Message, "websitealerts@bauschhealth.com");
                objSearchModel.error = ex.Message;
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(objSearchModel),
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

    public class SearchModel
    {
        public string error { get; set; }
        public string JsonResults { get; set; }
    }

}