using EPiServer.Core;
using EPiServer.Core.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using EpiserverBH.Business.Rendering;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EpiserverBH.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class BauschActiveURLManagement : IInitializableModule
    {
        private bool initialized = false;
        private IContentLoader contentLoader;
        private IContentUrlGeneratorEvents contentRouteEvents;
        private IHttpContextAccessor httpContextAccessor;
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += (o, e) =>
            {
                context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            };
        }

        public void Initialize(InitializationEngine context)
        {
            if (!initialized)
            {
                contentLoader = context.Locate.Advanced.GetInstance<IContentLoader>();
                contentRouteEvents = context.Locate.Advanced.GetInstance<IContentUrlGeneratorEvents>();
                //contentRouteEvents.GeneratingUrl += ContentRouteEvents_GeneratedUrl;
                httpContextAccessor = context.Locate.Advanced.GetInstance<IHttpContextAccessor>();
                initialized = true;
            }
        }

        private void ContentRouteEvents_GeneratedUrl(object sender, UrlGeneratorEventArgs e)
        {
            if (e.Context.ContentLink != null)
            {
                var lang = e.Context.Language.Name;
                var langSelector = string.IsNullOrEmpty(lang) ? LanguageSelector.AutoDetect() : new LanguageSelector(lang);
                var page = contentLoader.Get<IContent>(e.Context.ContentLink, langSelector) as PageData;
                
                var pageUrl = Convert.ToString(httpContextAccessor.HttpContext.Request.GetDisplayUrl().ToString());
                if (!pageUrl.ToLower().Contains("externalcontentview") && !pageUrl.ToLower().Contains("episerver/cms"))
                {
                    if (page != null && !string.IsNullOrEmpty(page.ExternalURL))
                    {

                        String eUrl = page.ExternalURL;
                        eUrl = "/" + eUrl;
                        eUrl = eUrl.Replace("//", "/");
                        eUrl = eUrl.Replace("https:/", "https://");
                        eUrl = eUrl.Replace("http:/", "http://");
                        e.Context.Url = new UrlBuilder(eUrl);
                        e.State = RoutingState.Done;

                    }
                }
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}