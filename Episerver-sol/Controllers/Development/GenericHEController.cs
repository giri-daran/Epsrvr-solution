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
    public class GenericHEController : PageController<GenericHE>
    {


        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;

        public GenericHEController(IContentLoader contentLoader, UrlResolver urlResolver, IDatabaseMode databaseMode)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
        }
        public IActionResult Index(GenericHE currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            var startPageContentLink = SiteDefinition.Current.StartPage;
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {

                var editHints = ViewData.GetEditHints<PageViewModel<GenericHE>, GenericHE>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);

            }


            if (currentPage.SiteTemplate == "" || currentPage.SiteTemplate == null)
            {
                var pages = _contentLoader.GetChildren<SitePageData>(startPageContentLink);
                var HEPages = pages.Where(i => i.PageTypeName == "GenericHome").FirstOrDefault();

                PageReference listRoot = HEPages.PageLink;
                var HCPPages = _contentLoader.GetChildren<GenericHE>(listRoot).OfType<GenericHE>();
                var HEHCPHome = HCPPages.Where(i => i.PageTypeName == "GenericHE").FirstOrDefault();
                string templatename = string.Empty;
                templatename = HEHCPHome.SiteTemplate;


                if (templatename == null)
                {
                    var HEPage = _contentLoader.Get<GenericHCP>(startPageContentLink);
                    //model.CurrentPage.SiteTemplate = templatename;
                    var editHints = ViewData.GetEditHints<PageViewModel<GenericHE>, GenericHE>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => HEPage.SiteTemplate);
                    return View(string.Format("/Views/{0}/Index.cshtml", HEPage.SiteTemplate), model);
                }
                else
                {
                    var editHints = ViewData.GetEditHints<PageViewModel<GenericHE>, GenericHE>();
                    editHints.AddConnection(m => m.Layout.SiteTemplate, p => templatename);
                    return View(string.Format("/Views/{0}/Index.cshtml", templatename), model);
                }

            }
            else
            {
                var editHints = ViewData.GetEditHints<PageViewModel<GenericHE>, GenericHE>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.GoogleTagManager, p => p.GoogleTagManager);
                editHints.AddConnection(m => m.Layout.SiteTemplate, p => p.SiteTemplate);
                return View(string.Format("/Views/{0}/Index.cshtml", currentPage.SiteTemplate), model);
            }

        }
    }
}