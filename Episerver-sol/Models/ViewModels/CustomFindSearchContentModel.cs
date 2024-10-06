using System;
using System.Collections.Generic;
using System.Web;
using EpiserverBH.Models.Pages;
using EPiServer.Find.UnifiedSearch;
using EPiServer.ServiceLocation;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EpiserverBH.Models.ViewModels
{
    public class CustomFindSearchContentModel : PageViewModel<CustomFindPage>
    {
      
        public CustomFindSearchContentModel(CustomFindPage currentPage, string searchQuery)
          : base(currentPage)
        {
            SearchQuery = searchQuery;
        }
        public string SearchQuery { get; private set; }
        public UnifiedSearchResults Hits { get; set; }
        public IList<UnifiedSearchHit> ResultsList = new List<UnifiedSearchHit>();
        public int PageNumber { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int ResultCount { get; set; }
        /// <summary>
        /// Current active section filter
        /// </summary>
        public bool ASExactSearch
        {
            get { return CurrentPage.ASExactSearch; }
        }
        public IEnumerable<SelectListItem> Searchtype { get; set; }
        /// <summary>
        /// Retrieve the paging page from the query string parameter "p".
        /// If no parameter exists, default to the first page.
        /// </summary>
        public int NumberOfHits
        {
            get { return Hits.TotalMatching; }
        }
        public int TotalPagingPages
        {
            get
            {
                if (CurrentPage.PageSize > 0)
                {
                    return 1 + (Hits.TotalMatching - 1) / CurrentPage.PageSize;
                }

                return 0;
            }
        }
    }
}