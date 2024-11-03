using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace EpiserverBH.Business
{
    public static class PageExtension
    {
        public static PageDataCollection GetChildren(this PageData parentPage)
        {
            return parentPage.GetChildren<PageData>().ToPageDataCollection();
        }
        public static IEnumerable<T> GetChildren<T>(this PageData parentPage)
                   where T : PageData
        {
            if (parentPage == null)
            {
                return Enumerable.Empty<T>();
            }
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            return contentLoader.GetChildren<T>(parentPage.ContentLink);
        }
        public static PageDataCollection ToPageDataCollection(this IEnumerable<PageData> pages)
        {
            return new PageDataCollection(pages);
        }
        public static PageData GetPage(this ContentReference contentReference)
        {
            return contentReference.GetPage<PageData>();
        }
        public static T GetPage<T>(this ContentReference contentReference) where T : PageData
        {
            if (contentReference.IsNullOrEmpty()) return null;

            var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
            return loader.Get<PageData>(contentReference) as T;
        }
        public static bool IsNullOrEmpty(this ContentReference contentReference)
        {
            return ContentReference.IsNullOrEmpty(contentReference);
        }

        public static VirtualPathData FriendlyUrl(this ContentReference contentReference)
        {
            return ServiceLocator.Current.GetInstance<UrlResolver>().GetVirtualPath(contentReference);
            // or use the singleton
            // return UrlResolver.Current.GetVirtualPath(contentReference); 
        }

        public static VirtualPathData FriendlyUrl(this PageData pageData)
        {
            var contentReference = pageData.ContentLink;
            return ServiceLocator.Current.GetInstance<UrlResolver>().GetVirtualPath(contentReference);
            // or use the singleton
            // return UrlResolver.Current.GetVirtualPath(contentReference); 
        }

        public static VirtualPathData FriendlyUrl(this IContent iContent)
        {
            var contentReference = iContent.ContentLink;
            return ServiceLocator.Current.GetInstance<UrlResolver>().GetVirtualPath(contentReference);
            // or use the singleton
            // return UrlResolver.Current.GetVirtualPath(contentReference); 
        }
    }
}
