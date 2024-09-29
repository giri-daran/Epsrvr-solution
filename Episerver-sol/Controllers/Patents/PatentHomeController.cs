using System;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages;
using EPiServer.Web;
using EPiServer.Core;
using EPiServer;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Patents
{
    public class PatentHomeController : PageController<Models.Pages.Patents.PatentHome>
    {
        // GET: PatentHome
        public IActionResult Index(Models.Pages.Patents.PatentHome currentPage)
        {
            var pageRef = ContentReference.StartPage;
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

            PageData data = repository.Get<PageData>(pageRef);

            var model = PageViewModel.Create(currentPage);

            return View(string.Format("~/Views/{0}/Index.cshtml", data["SiteTemplate"]), model);
        }
    }
}