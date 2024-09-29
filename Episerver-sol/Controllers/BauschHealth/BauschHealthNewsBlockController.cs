using System;
using System.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.BauschHealth;
using EPiServer.Web;
using EPiServer.Core;
using EPiServer;
using System.Collections.Generic;
using EPiServer.Filters;
using EpiserverBH.Business;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.BauschHealth
{
    public class BauschHealthNewsBlockController : BlockController<BauschHealthNewsBlock>
    {
        private ContentLocator contentLocator;
        private IContentLoader contentLoader;

        public BauschHealthNewsBlockController(ContentLocator contentLocator, IContentLoader contentLoader)
        {
            this.contentLocator = contentLocator;
            this.contentLoader = contentLoader;
        }

        protected override IViewComponentResult InvokeComponent(BauschHealthNewsBlock currentBlock)
        {
            var pages = FindPages(currentBlock);

            pages = Sort(pages, currentBlock.SortOrder);

            if (currentBlock.Count > 0)
            {
                pages = pages.Take(currentBlock.Count);
            }

            var model = new BauschHealthListModel(currentBlock)
            {
                Pages = pages
            };

            ViewData.GetEditHints<PageListModel, PageListBlock>()
                .AddConnection(x => x.Heading, x => x.Heading);
            
            return View("/Views/Shared/Blocks/BauschHealthNewsBlock.cshtml", model);
            //return PartialView(model);
        }        

        private IEnumerable<PageData> FindPages(BauschHealthNewsBlock currentBlock)
        {
            IEnumerable<PageData> pages;
            var listRoot = currentBlock.Root;
            if (currentBlock.Recursive)
            {
                if (currentBlock.PageTypeFilter != null)
                {
                    pages = contentLocator.FindPagesByPageType(listRoot, true, currentBlock.PageTypeFilter.ID);
                }
                else
                {
                    pages = contentLocator.GetAll<PageData>(listRoot);
                }
            }
            else
            {
                if (currentBlock.PageTypeFilter != null)
                {
                    pages = contentLoader.GetChildren<PageData>(listRoot)
                        .Where(p => p.ContentTypeID == currentBlock.PageTypeFilter.ID);
                }
                else
                {
                    pages = contentLoader.GetChildren<PageData>(listRoot);
                }
            }

            if (currentBlock.CategoryFilter != null && currentBlock.CategoryFilter.Any())
            {
                pages = pages.Where(x => x.Category.Intersect(currentBlock.CategoryFilter).Any());
            }
            return pages;
        }

        private IEnumerable<PageData> Sort(IEnumerable<PageData> pages, FilterSortOrder sortOrder)
        {
            var asCollection = new PageDataCollection(pages);
            var sortFilter = new FilterSort(sortOrder);
            sortFilter.Sort(asCollection);
            return asCollection;
        }

    }
}