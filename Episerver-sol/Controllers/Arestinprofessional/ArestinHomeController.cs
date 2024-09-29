using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages.Arestinprofessional;
using EPiServer.Web;
using System.Linq;
using EPiServer.Web.Routing;
using EPiServer.Framework.Localization;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using EPiServer.Core.Routing.Pipeline;
using EpiserverBH.Models;
using EPiServer.ServiceLocation;
using Episerver.Marketing.Connector;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Identity;
using EPiServer.Cms.UI.AspNetIdentity;
using EpiserverBH.Infrastructure.Cms.Users;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinHomeController : PageController<Models.Pages.Arestinprofessional.ArestinHome>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public readonly ApplicationSignInManager<SiteUser> _SignInManager;



        public ArestinHomeController(ApplicationSignInManager<SiteUser> SignInManager, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            _SignInManager = SignInManager;
        }
        private string GetUrl(string segment, string pageName)
        {
            string url = "";
            var index = _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().IndexOf(segment);

            if (index > 0)
            {
                var baseUrl = _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().Substring(0, index);

                if (!baseUrl.EndsWith("/"))
                {
                    baseUrl = string.Format("{0}/", baseUrl);
                }
                url = string.Format("{0}{1}", baseUrl, pageName);
            }

            return url;
        }
        private string GetLoginUrl(string segment, string redirectUrl)
        {
            string url = "educational-resources/arestin-student-certification-program";
            var index = _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().IndexOf(segment);

            if (index > 0)
            {
                var baseUrl = _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().Substring(0, index);
                //var baseUrl = url;

                if (!baseUrl.EndsWith("/"))
                {
                    baseUrl = string.Format("{0}/", baseUrl);
                }
                url = string.Format("{0}{1}", baseUrl, string.Format(url));
            }

            return url;
        }
        private bool IsFacultyUser()
        {
            bool isFacultyUser = false;

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var FacultyRoles = "Faculty User";

                isFacultyUser = IsUserInRole(FacultyRoles);
            }

            return isFacultyUser;
        }

        private bool IsStudentUser()
        {
            bool isStudentUser = false;

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var StudentRoles = "Student User";

                isStudentUser = IsUserInRole(StudentRoles);
            }

            return isStudentUser;
        }
        private bool IsAPUser()
        {
            bool isAPUser = false;

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var ArestinRoles = "ArestinProfessional_Users";

                isAPUser = IsUserInRole(ArestinRoles);
            }

            return isAPUser;
        }

        private bool IsUserInRole(string roleCommaSeparated)
        {
            bool isUserInRole = false;

            if (!string.IsNullOrEmpty(roleCommaSeparated))
            {
                var roleArray = roleCommaSeparated.Split(new[] { ',' });

                foreach (var role in roleArray)
                {
                    isUserInRole = User.IsInRole(role);
                    if (isUserInRole)
                    {
                        break;
                    }
                }
            }

            return isUserInRole;
        }

        public IActionResult Index(ArestinHome currentPage)
        {
            var index = _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().IndexOf("/enroll-your-school");

            if (index>0)
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    if (!IsFacultyUser())
                    {
                        return Redirect(GetUrl("/enroll-your-school", "Unauthorized"));
                    }
                }
                else
                {
                    var returnUrl = GetLoginUrl("/enroll-your-school", _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl());
                    return Redirect(returnUrl);
                }
            }
            else if (_IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().ToLower().StartsWith("/program-for-faculty"))
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    if (!IsFacultyUser())
                    {
                        if (!IsAPUser())
                        {
                            return Redirect(GetUrl("/Program-for-faculty", "Unauthorized"));
                        }

                    }
                }
                else
                {
                    var returnUrl = GetLoginUrl("/Program-for-faculty", _IHttpContextAccessor.HttpContext.Request.GetDisplayUrl());
                    return Redirect(returnUrl);
                }
            }
            else if (_IHttpContextAccessor.HttpContext.Request.GetDisplayUrl().ToLower().StartsWith("/manage-profile"))
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {

                    if (IsAPUser())
                    {
                        if (!IsFacultyUser())
                        {
                            if (!IsStudentUser())
                            {
                                return Redirect(GetUrl("/manage-profile", "Unauthorized"));
                            }

                        }
                        
                    }
                }
            }


            var model = PageViewModel.Create(currentPage);
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();
            var loader = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentLoader>();

            var pageRef = ContentReference.StartPage;

            PageData data = repository.Get<PageData>(pageRef);
            
            var startpage = SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(data.ContentLink);
            // var startPage = _contentLoader.Get<Home>(startPageContentLink);
            string templatename = string.Empty;

            if (Convert.ToString(currentPage.SiteTemplate) == null)
            {
                PageData ParentPagedata = repository.Get<PageData>(currentPage.ParentLink);
                if (Convert.ToString(ParentPagedata["SiteTemplate"]) == null)
                {
                    templatename = Convert.ToString(data["SiteTemplate"]);                   
                }
                else
                {
                    templatename = Convert.ToString(ParentPagedata["SiteTemplate"]);                   
                }
            }
            else
            {
                templatename = currentPage.SiteTemplate;
            }

            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<ArestinHome>, ArestinHome>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                //editHints.AddConnection(m => m.Layout.SiteCSS, p => p.SiteCSS);
                // editHints.AddConnection(m => m.Layout.SiteJs, p => p.SiteJs);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);

                editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
            }
            // var sPage = ContentReference.StartPage;

            if (currentPage.SiteTemplate == null)
            {
                PageData ParentPagedata = repository.Get<PageData>(currentPage.ParentLink);
                if (Convert.ToString(ParentPagedata["SiteTemplate"]) == "")
                {
                    return View(string.Format("/Views/{0}/Index.cshtml", data["SiteTemplate"]), model);
                }
                else
                {
                    return View(string.Format("/Views/{0}/Index.cshtml", ParentPagedata["SiteTemplate"]), model);
                }               
            }
            else
            {
                return View(string.Format("/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }        
        }

        [HttpPost]
        public ActionResult SignOutUser()
        {
            _SignInManager.SignOutAsync();

            _IHttpContextAccessor.HttpContext.Session.Remove("ProfileType");

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    success = false,
                    returnURL = "/educational-resources/arestin-student-certification-program"
                }),
                ContentType = "application/json",
            };

        }


    }
}