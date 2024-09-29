using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages.Development;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Data;
using EpiserverBH.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Xifaxan
{
    public class GenericHEHCPController : PageController<GenericHEHCP>
    {


        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public GenericHEHCPController(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }
        public IActionResult Index(GenericHEHCP currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            var startPageContentLink = SiteDefinition.Current.StartPage;
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {

                var editHints = ViewData.GetEditHints<PageViewModel<GenericHEHCP>, GenericHEHCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);

            }


            if (currentPage.SiteTemplate == "" || currentPage.SiteTemplate == null)
            {
                var pages = _contentLoader.GetChildren<SitePageData>(startPageContentLink);
                var HCPPage = pages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                PageReference listRoot = HCPPage.PageLink;
                var HCPPages = _contentLoader.GetChildren<GenericHEHCP>(listRoot).OfType<GenericHEHCP>();
                var HEHCPHome = HCPPages.Where(i => i.PageTypeName == "GenericHEHCP").FirstOrDefault();
                string templatename = string.Empty;
                templatename = HEHCPHome.SiteTemplate;


                if (templatename == null)
                {
                    var HEHCPPage = _contentLoader.Get<GenericHEHCP>(startPageContentLink);
                    //model.CurrentPage.SiteTemplate = templatename;
                    var editHints = ViewData.GetEditHints<PageViewModel<GenericHEHCP>, GenericHEHCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => HEHCPPage.SiteTemplate);
                    return View(string.Format("/Views/{0}/Index.cshtml", HEHCPPage.SiteTemplate), model);
                }
                else
                {
                    var editHints = ViewData.GetEditHints<PageViewModel<GenericHEHCP>, GenericHEHCP>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
                    return View(string.Format("/Views/{0}/Index.cshtml", templatename), model);
                }

            }
            else
            {
                var editHints = ViewData.GetEditHints<PageViewModel<GenericHEHCP>, GenericHEHCP>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);
                return View(string.Format("/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }

        }
    }
}