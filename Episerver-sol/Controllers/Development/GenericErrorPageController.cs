using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages;
using EPiServer.Web;
using EpiserverBH.Business;
using EpiserverBH.Business.Initialization;
using EPiServer.Framework;
using System;
using EPiServer.Web.Routing.Segments.Internal;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System.Reflection;
using Newtonsoft.Json;
using EpiserverBH.Models.Pages.Development;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Development
{
    //[CustomPageNotFoundAttribute]
    public class GenericErrorPageController : PageController<Models.Pages.Development.GenericError>
    {
        // GET: ErrorPage
        public IActionResult Index(GenericError currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            GetLocalisedOrDefaultErrorPage(currentPage);
            
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();
            var loader = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentLoader>();

            var pageRef = ContentReference.StartPage;

            PageData data = repository.Get<PageData>(pageRef);
            var startpage = SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(data.ContentLink);
            return View(string.Format("~/Views/{0}/PageNotFound.cshtml", data["SiteTemplate"]), model);
        }
     
        private GenericError GetLocalisedOrDefaultErrorPage(GenericError errorPage)
        {
            // Determine language of the NotFoundUrl
            if (!string.IsNullOrEmpty(ViewBag.NotFoundUrl))
            {
                var uri = new Uri(ViewBag.NotFoundUrl);

                // if URI segments of the NotFoundUrl are composed of: 1) "/", 2) "{lang}/" (i.e. "/pl/incorrecturl")
                // then load the correct language of the Error page
                if (uri.Segments.Length > 2 && uri.Segments[0] == "/" && uri.Segments[1].EndsWith("/"))
                {

                    string firstWordSegmentInUrl = uri.Segments[1].Substring(0, uri.Segments[1].Length - 1);
                    var url = "/"+firstWordSegmentInUrl+"/Page-Not-Found";
                    var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
                    IContent contentData = urlResolver.Route(new UrlBuilder(url));
                    if (contentData != null)
                    {
                        Response.Redirect(SiteDefinition.Current.SiteUrl.AbsoluteUri + url);
                    }

                }
            }

            return errorPage;
        }
    }
}