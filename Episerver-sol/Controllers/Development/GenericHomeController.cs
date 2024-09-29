using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Development
{
    public class GenericHomeController : PageController<Models.Pages.Development.GenericHome>
    {
        // GET: GenericHome
        public IActionResult Index(Models.Pages.Development.GenericHome currentPage)
        {
            var pageRef = ContentReference.StartPage;
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

            PageData data = repository.Get<PageData>(pageRef);

            var model = PageViewModel.Create(currentPage);
           
            return View(string.Format("~/Views/{0}/Index.cshtml", data["SiteTemplate"]), model);
        }
        // GET: Home

    }
}
