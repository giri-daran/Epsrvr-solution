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
using EpiserverBH.Models.Blocks.BHITSurvey;
using EPiServer.Web.Mvc;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;

namespace EpiserverBH.Controllers.BHITSurvey
{
  
    [TemplateDescriptor(AvailableWithoutTag = true,

                    ModelType = typeof(BHITSurveyModuleBlock),
                  TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent)]
    public class BHITSurveyModuleBlockController : BlockComponent<BHITSurveyModuleBlock>
    {
        private static IHttpContextAccessor _IHttpContextAccessor;
        public IWebHostEnvironment _hostingEnvironment;
        public IConfiguration _configuration;
        public BHITSurveyModuleBlockController(IConfiguration configuration, IHttpContextAccessor _iHttpContextAccessor, IWebHostEnvironment iWebHostEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = iWebHostEnvironment;
            _IHttpContextAccessor = _iHttpContextAccessor;
        }
        private static BHITSurveyModuleBlock _block;
        protected override IViewComponentResult InvokeComponent(BHITSurveyModuleBlock currentContent)
        {
            _block = currentContent;
            return View(currentContent);
            
        }

        [HttpGet]
        public ActionResult GetSurvey()
        {
            SurveyDAL dal = new SurveyDAL();
            bool isDisplayResponse = false;

            Tuple<SurveyResponse, int> response;
            
            if (_IHttpContextAccessor.HttpContext.Request.Cookies.ContainsKey(_block.SurveyID + "_Survey"))
            {
                isDisplayResponse = true;
                response = dal.GetSurveyOptions(_block.SurveyID, _block.ModuleID, _block.Question, _block.Options, true);
            }
            else
            {
                response = dal.GetSurveyOptions(_block.SurveyID, _block.ModuleID, _block.Question, _block.Options);
                //This statement is required to ensure that when new survey question is given its inserted and the new surveyid is updated at block level
                _block.SurveyID = response.Item2;
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { Response = response.Item1, IsDisplayResponse = isDisplayResponse }),
                ContentType = "application/json",
            };
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSurveyResult(SurveyResultInfo surveyResultInfo)
        {
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    SurveyDAL surveyDAL = new SurveyDAL();
                    surveyDAL.AddSurveyResult(surveyResultInfo);
                    CookieOptions objCookie = new CookieOptions();
                    //HttpCookie objCookie = new HttpCookie(_block.SurveyID + "_Survey");

                    objCookie.Expires = DateTime.Now.AddDays(30);

                    _IHttpContextAccessor.HttpContext.Response.Cookies.Append(_block.SurveyID + "_Survey", "True", objCookie);
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(new { IsDisplayResponse = true }),
                        ContentType = "application/json",
                    };
                }
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { IsDisplayResponse = false }),
                ContentType = "application/json",
            };

        }
    }
}


