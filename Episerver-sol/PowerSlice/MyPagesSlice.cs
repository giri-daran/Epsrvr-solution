using EPiServer.Cms.Shell.UI.Rest.ContentQuery;
using EPiServer.Find;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using EpiserverBH.Models.Pages;
using PowerSlice;

namespace EpiserverBH.PowerSlice
{
    [ServiceConfiguration(typeof(IContentQuery)), ServiceConfiguration(typeof(IContentSlice))]
    public class MyPagesSlice : ContentSliceBase<SitePageData>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MyPagesSlice(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override string Name
        {
            get { return "My Pages"; }
        }
        protected override ITypeSearch<SitePageData> Filter(ITypeSearch<SitePageData> searchRequest, ContentQueryParameters parameters)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return searchRequest.Filter(x => x.MatchTypeHierarchy(typeof(IChangeTrackable)) & ((IChangeTrackable)x).CreatedBy.Match(userName));
        }
    }
}
