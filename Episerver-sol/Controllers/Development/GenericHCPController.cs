using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Data;
using EpiserverBH.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Development.GenericHCP
{
    public class GenericHCPController : PageController<Models.Pages.Development.GenericHCP>
    {


        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public GenericHCPController(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }
        public IActionResult Index(Models.Pages.Development.GenericHCP currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            var startPageContentLink = SiteDefinition.Current.StartPage;
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {

                var editHints = ViewData.GetEditHints<PageViewModel<Models.Pages.Development.GenericHCP>, Models.Pages.Development.GenericHCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);

            }


            if (currentPage.SiteTemplate == "" || currentPage.SiteTemplate == null)
            {
                var pages = _contentLoader.GetChildren<SitePageData>(startPageContentLink);
                var HCPPage = pages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                //PageReference listRoot = HCPPage.PageLink;
                //var HCPPages = _contentLoader.GetChildren<Models.Pages.Development.GenericHCP> (listRoot).OfType<Models.Pages.Development.GenericHCP>();
                var GenericHCPHome = (EpiserverBH.Models.Pages.Development.GenericHCP)HCPPage;//HCPPages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();
                string templatename = string.Empty;               
                templatename = GenericHCPHome.SiteTemplate;
                if (templatename == null)
                {                    
                    var GenericHCPPage = _contentLoader.Get<Models.Pages.Development.GenericHCP>(GenericHCPHome.ContentLink);
                    //model.CurrentPage.SiteTemplate = templatename;
                    var editHints = ViewData.GetEditHints<PageViewModel<Models.Pages.Development.GenericHCP>, Models.Pages.Development.GenericHCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => GenericHCPPage.SiteTemplate);
                    return View(string.Format("/Views/{0}/Index.cshtml", GenericHCPPage.SiteTemplate), model);
                }
                else
                {
                    var editHints = ViewData.GetEditHints<PageViewModel<Models.Pages.Development.GenericHCP>, Models.Pages.Development.GenericHCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
                    return View(string.Format("/Views/{0}/Index.cshtml", templatename), model);
                }

            }
            else
            {
                var editHints = ViewData.GetEditHints<PageViewModel<Models.Pages.Development.GenericHCP>, Models.Pages.Development.GenericHCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);
                return View(string.Format("/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }

        }
    }
}