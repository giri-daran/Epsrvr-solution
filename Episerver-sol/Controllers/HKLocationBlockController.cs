using EPiServer.Cms.UI.Admin.Configurations.Internal.ViewModels;
using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.HKLocationBlock;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace EpiserverBH.Controllers
{
    public class HKLocationBlockController : BlockComponent<HKLocation>
    {
        private static HKLocation _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IHttpContextAccessor _IHttpContextAccessor;

        public HKLocationBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor _iHttpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = _iHttpContextAccessor;
        }

        private string GetConnectionString()
        {
            return SQLGetConnection.GetConnection().ConnectionString;
        }

        protected override IViewComponentResult InvokeComponent(HKLocation currentContent)
        {
            _block = currentContent;

            if (currentContent != null)
            {
                var assetFolder = string.Format("Assets/HKLocationBlock/Templates/{0}/", currentContent.TemplateName);
                var assetsPath = Path.Combine(_hostingEnvironment.WebRootPath, assetFolder);

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

            return View(currentContent);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLocationList(string CategoryVal)
        {
            string Resultdata = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string _TableName = _block.TableName.ToString();
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        if (CategoryVal != null)
                        {
                            _SQLCommand = string.Format("SELECT DISTINCT Location,LocationChinese, LocationGroup FROM {0} WHERE Category like '%{1}%' order by [RegionOrder] asc, [Locationorder] asc", _TableName, CategoryVal);
                        }
                        else
                        {
                            _SQLCommand = string.Format("SELECT DISTINCT Location,LocationChinese, LocationGroup FROM {0} order by [RegionOrder] asc, [Locationorder] asc", _TableName);
                        }

                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        Resultdata = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ContentResult result = new ContentResult();
            result.Content = Resultdata;
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLocationListCnB(string CategoryVal)
        {
            string Resultdata = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string _TableName = _block.TableName.ToString();
                        string ConStringnew = GetConnectionString();


                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        if (CategoryVal != null)
                        {
                            _SQLCommand = string.Format("SELECT DISTINCT Location,LocationChinese, LocationGroup FROM {0} WHERE Category like '%{1}%'", _TableName, CategoryVal);
                        }
                        else
                        {
                            _SQLCommand = string.Format("SELECT DISTINCT Location,LocationChinese, LocationGroup FROM {0}", _TableName);
                        }
                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        Resultdata = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ContentResult result = new ContentResult();
            result.Content = Resultdata;
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetClinicNameList(string LocationVal, string CategoryVal)
        {
            string Resultdata = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string _TableName = _block.TableName.ToString();
                        string ConStringnew = GetConnectionString();

                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);

                        if (LocationVal != null && CategoryVal != null)
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0} WHERE Location = '{1}' AND Category like '%{2}%' order by [RegionOrder] asc, [Locationorder] asc, [ClinicName] asc", _TableName, LocationVal, CategoryVal);
                        }
                        else if (CategoryVal != null)
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0} WHERE Category like '%{1}%'  order by [RegionOrder] asc, [Locationorder] asc, [ClinicName] asc", _TableName, CategoryVal);
                        }
                        else if (LocationVal != null)
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0} WHERE Location = '{1}'  order by [RegionOrder] asc, [Locationorder] asc, [ClinicName] asc", _TableName, LocationVal);
                        }
                        else
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0}  order by [RegionOrder] asc, [Locationorder] asc, [ClinicName] asc", _TableName);
                        }

                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        Resultdata = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ContentResult result = new ContentResult();
            result.Content = Resultdata;
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetClinicNameListCnB(string LocationVal)
        {
            string Resultdata = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string _TableName = _block.TableName.ToString();
                        string ConStringnew = GetConnectionString();

                        string _SQLCommand = string.Empty;
                        string s = string.Empty;
                        DataTable dataTable = new DataTable();
                        SqlConnection conn = new SqlConnection(ConStringnew);
                        if (string.IsNullOrWhiteSpace(LocationVal))
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0}", _TableName);
                        }
                        else
                        {
                            _SQLCommand = string.Format("SELECT * FROM {0} WHERE Location = '{1}'", _TableName, LocationVal);
                        }



                        SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        conn.Close();
                        da.Dispose();

                        Resultdata = this.DataTableToJSONWithStringBuilderForGrid(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ContentResult result = new ContentResult();
            result.Content = Resultdata;
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
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