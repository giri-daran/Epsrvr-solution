﻿using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Trulance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using static EpiserverBH.Controllers.Trulance.SavingsCardHelper;
using static EpiserverBH.Controllers.Trulance.SavingsInfo;

namespace EpiserverBH.Controllers.Trulance
{
    public class TrulanceEmailCardElementBlockController : BlockComponent<TrulanceEmailCardElementBlock>
    {
        private static TrulanceEmailCardElementBlock _block;
        private SavingsCardHelper sch;
        private rtnResult result;
        private static IHttpContextAccessor _IHttpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IMemoryCache _IMemoryCache;
        private readonly IConfiguration _configuration;

        protected override IViewComponentResult InvokeComponent(TrulanceEmailCardElementBlock currentContent)
        {
            _block = currentContent;

            return View(currentContent);
        }

        public TrulanceEmailCardElementBlockController(IHttpContextAccessor _iHttpContextAccessor, IWebHostEnvironment hostingEnvironment, IMemoryCache memoryCache, IConfiguration _iConfiguration)
        {
            _IHttpContextAccessor = _iHttpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _configuration = _iConfiguration;
            _IMemoryCache = memoryCache;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmailCard(TrulanceCardInfo cardInfo)
        {
            bool isSuccess = false;
            string LeadID = string.Empty;
            string ErrorMsg = string.Empty;
            TrulanceInfo info = new TrulanceInfo();
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    try
                    {
                        info.PortalID = 615;
                        if (!string.IsNullOrWhiteSpace(info.SiteType))
                        {
                            if (info.SiteType.Trim().ToUpper() == "DTC")
                            {
                                //DTC
                                info.FormID = 22;
                                info.FormName = "Email";
                            }
                            else
                            {
                                //HCP
                                info.FormID = 23;
                                info.FormName = "Email";
                            }
                        }
                        else
                        {
                            info.SiteType = "DTC";
                            info.FormID = 22;
                            info.FormName = "Email";
                            info.SavingCardAction = "download";
                            info.hdnProgramId = "45";
                            info.hdnSurvey = "False";
                            info.EmailAddress = cardInfo.EmailAddress;
                        }

                        sch = new SavingsCardHelper(_configuration, _hostingEnvironment, _IHttpContextAccessor, _IMemoryCache);
                        result = sch.Execute(info);

                        if (result.status == "True" || result.status == "true")
                        {
                            isSuccess = true;
                        }

                        LeadID = info.LeadID;
                        ErrorMsg = result.errorMessage;
                    }
                    catch (Exception ex)
                    {
                        //MailService.SendAlert("no-reply@valeantonline.com", "Episerver - Trulance" + info.FormName + " Form Exception", ex.Message, "websitealerts@bauschhealth.com");
                    }

                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(new
                        {
                            IsSuccess = isSuccess,
                            LeadID = LeadID,
                            ErrorMsg = ErrorMsg
                        }),

                        ContentType = "application/json",
                    };
                }
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    LeadID = LeadID,
                    ErrorMsg = ErrorMsg
                }),

                ContentType = "application/json",
            };
        }
    }


}