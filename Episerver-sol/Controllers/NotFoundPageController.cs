using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Data;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers
{
    public class NotFoundPageController : PageController<Error>
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public NotFoundPageController(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }
        public IActionResult Index(Error currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            var startPageContentLink = SiteDefinition.Current.StartPage;
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {

                var editHints = ViewData.GetEditHints<PageViewModel<Error>, Error>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);

            }


            if (currentPage.SiteTemplate == "" || currentPage.SiteTemplate == null)
            {
                var PageTypeList = _contentLoader.GetChildren<Home>(startPageContentLink);
                var PageType = PageTypeList.Where(i => i.PageTypeName == "Error" && i.SiteTemplate != string.Empty);
                string templatename = string.Empty;
                foreach (var i in PageType)
                {
                    templatename = i.SiteTemplate;
                }


                if (templatename == null)
                {
                    var ErrorPage = _contentLoader.Get<Error>(startPageContentLink);
                    //model.CurrentPage.SiteTemplate = templatename;
                    var editHints = ViewData.GetEditHints<PageViewModel<Error>, Error>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => ErrorPage.SiteTemplate);
                    return View(string.Format("~/Views/{0}/PageNotFound.cshtml", ErrorPage.SiteTemplate), model);
                }
                else
                {
                    var editHints = ViewData.GetEditHints<PageViewModel<Error>, Error>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
                    return View(string.Format("~/Views/{0}/PageNotFound.cshtml", templatename), model);
                }

            }
            else
            {
                var editHints = ViewData.GetEditHints<PageViewModel<Error>, Error>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);
                return View(string.Format("~/Views/{0}/PageNotFound.cshtml", currentPage.SiteTemplate), model);
            }
        }
    }
}
