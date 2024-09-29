using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Development
{
    public class GenericInnerController : PageController<Models.Pages.Development.GenericInner>
    {
        // GET: GenericInner
        public IActionResult Index(Models.Pages.Development.GenericInner currentPage)
        {
            var pageRef = ContentReference.StartPage;
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

            PageData data = repository.Get<PageData>(pageRef);

            var model = PageViewModel.Create(currentPage);

            return View(string.Format("~/Views/{0}/Inner.cshtml", data["SiteTemplate"]), model);
        }
    }
}