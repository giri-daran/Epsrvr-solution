using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Data;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers
{
    public class HCPController : PageController<Models.Pages.HCP>
    {


        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public HCPController(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }
        public IActionResult Index(HCP currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            var startPageContentLink = SiteDefinition.Current.StartPage;
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {

                var editHints = ViewData.GetEditHints<PageViewModel<HCP>, HCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);

            }


            if (currentPage.SiteTemplate == "" || currentPage.SiteTemplate == null)
            {
                var PageTypeList = _contentLoader.GetChildren<Home>(startPageContentLink);
                var PageType = PageTypeList.Where(i => i.PageTypeName == "HCP" && i.SiteTemplate != string.Empty);
                string templatename = string.Empty;
                foreach (var i in PageType)
                {
                    templatename = i.SiteTemplate;
                }


                if (templatename == null)
                {
                    var HCPPage = _contentLoader.Get<HCP>(startPageContentLink);
                    //model.CurrentPage.SiteTemplate = templatename;
                    var editHints = ViewData.GetEditHints<PageViewModel<HCP>, HCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => HCPPage.SiteTemplate);
                    return View(string.Format("~/Views/{0}/Index.cshtml", HCPPage.SiteTemplate), model);
                }
                else
                {
                    var editHints = ViewData.GetEditHints<PageViewModel<HCP>, HCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
                    return View(string.Format("~/Views/{0}/Index.cshtml", templatename), model);
                }

            }
            else
            {
                var editHints = ViewData.GetEditHints<PageViewModel<HCP>, HCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);
                return View(string.Format("~/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }

        }
    }
}