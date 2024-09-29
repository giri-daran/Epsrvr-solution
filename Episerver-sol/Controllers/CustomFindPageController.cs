using DocumentFormat.OpenXml.Wordprocessing;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.Internal;
using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Find.Helpers.Text;
using EPiServer.Find.UI;
using EPiServer.Find.UnifiedSearch;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Pages;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EpiserverBH.Controllers
{
    public class CustomFindPageController : PageController<Models.Pages.CustomFindPage>
    {
        private readonly IClient searchClient;
        private readonly IRequiredClientResourceList requiredClientResourceList;
        private readonly IVirtualPathResolver virtualPathResolver;

        public ActionResult Index(CustomFindPage currentPage, string q, [FromQuery(Name = "search")] string search, [FromQuery(Name = "page")] string pagenumber, [FromQuery(Name = "resultcnt")] int searchcount)
        {
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();
            var loader = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentLoader>();

            var pageRef = ContentReference.StartPage;

            PageData data = repository.Get<PageData>(pageRef);


            if (String.IsNullOrEmpty(q))
            {
                q = search;
            }
            int iSearchcount = 0;
            if (searchcount != 0)
            {
                iSearchcount = searchcount;
            }
            var model = new CustomFindSearchContentModel(currentPage,q);
            //var model = new FindSearchPageViewModel(currentPage, q);
            if (String.IsNullOrEmpty(q))
            {
                return View(string.Format("~/Views/{0}/Search.cshtml", currentPage.SiteTemplate), model);
            }
            int page;

            if (String.IsNullOrEmpty(pagenumber) || pagenumber == "0")
            {
                page = 1;
            }
            else
            {
                page = Int32.Parse(pagenumber);

            }

            int pageSize = 10;
            model.PageNumber = page;
            model.PreviousPage = model.PageNumber - 1;
            model.NextPage = model.PageNumber + 1;
            if (iSearchcount != 0)
            {
                int totalpages = (int)Math.Ceiling((double)iSearchcount / pageSize);
                if (model.NextPage > totalpages)
                {
                    model.NextPage = totalpages;
                }
            }
            var unifiedSearch = SearchClient.Instance.UnifiedSearchFor(q)
                //.Filter(i=>i.MatchTypeHierarchy(typeof(Models.Pages.SitePageData)))
                .OrFilter(SiteFilterBuilder)
                .Filter(p => !p.MatchTypeHierarchy(typeof(ImageData)))
                .Filter(i => i.SearchPublishDate.Exists())
                .TermsFacetFor(x => x.SearchSection)
                .FilterFacet("AllSections", x => x.SearchSection.Exists())
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ApplyBestBets();
            var objresult = unifiedSearch.GetResult();

            model.Searchtype = CreateSearchtype();
            model.Hits = objresult;
            model.ResultCount = model.Hits.TotalMatching;
            if (currentPage.SiteTemplate == null)
            {
                PageData ParentPagedata = repository.Get<PageData>(currentPage.ParentLink);
                if (Convert.ToString(ParentPagedata["SiteTemplate"]) == "")
                {
                    return View(string.Format("~/Views/{0}/Search.cshtml", data["SiteTemplate"]), model);
                }
                else
                {
                    return View(string.Format("~/Views/{0}/Search.cshtml", ParentPagedata["SiteTemplate"]), model);
                }

            }
            else
            {
                return View(string.Format("~/Views/{0}/Search.cshtml", currentPage.SiteTemplate), model);
            }
        }     
        private static FilterBuilder<ISearchContent> SiteFilterBuilder
        {
            get
            {
                var filter = SearchClient.Instance.BuildFilter<ISearchContent>();
                filter = filter.Or(x => x.SearchHitUrl.Prefix(EPiServer.Web.SiteDefinition.Current.SiteUrl.AbsoluteUri));
                return filter;
            }
        }
        private IEnumerable<SelectListItem> CreateSearchtype()
        {
            //var items = new List<SelectListItem> { CreateSelectListItem("", "", selected) };
            //items.AddRange(searchClient.Settings.Languages.Select(x => CreateSelectListItem(x.Name, x.FieldSuffix, selected)));

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "All", Value = "All" });
            items.Add(new SelectListItem() { Text = "Pages", Value = "Pages" });
            items.Add(new SelectListItem() { Text = "PDF Files", Value = "PdfFile" });
            items.Add(new SelectListItem() { Text = "Images", Value = "ImageFile" });
            items.Add(new SelectListItem() { Text = "WebContent", Value = "WebContent" });
            return items;
        }
    }

}