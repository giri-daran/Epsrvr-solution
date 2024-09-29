using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Pages;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EpiserverBH.Controllers
{
    public class HomeController : PageController<Models.Pages.Home>
    {
        private static IHttpContextAccessor _IHttpContextAccessor { get; }
       
        public IActionResult Index(Home currentPage)
        {
           
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
                // return View(string.Format("~/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<Home>, Home>();
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
                    return View(string.Format("~/Views/{0}/Index.cshtml", data["SiteTemplate"]), model);
                }
                else
                {
                    return View(string.Format("~/Views/{0}/Index.cshtml", ParentPagedata["SiteTemplate"]), model);
                }
               
            }
            else
            {
                return View(string.Format("~/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }

        
        }
    }
}