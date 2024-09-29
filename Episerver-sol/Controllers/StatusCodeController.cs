using EPiServer.Filters;
using Microsoft.AspNetCore.Mvc;
using EPiServer.Core;
using EpiserverBH.Models.Pages;
using EpiserverBH.Models.Pages.Development;
using EpiserverBH.Models.ViewModels;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace EpiserverBH.Controllers
{
    public class StatusCodeController : Controller
    {
        private readonly IContentLoader _contentLoader;
        private static IHttpContextAccessor _IHttpContextAccessor;
        public StatusCodeController(IHttpContextAccessor IHttpContextAccessor, IContentLoader ContentLoader)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
            _contentLoader = ContentLoader;
        }


        [Route("/statuscode/{statusCode}")]
        public IActionResult Index(string statusCode)
        {
            string segmentfor404 = string.Empty;
            string type = String.Empty;

            if (statusCode == "404")
            {
                try
                {
                    var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    string url = feature?.OriginalPath;
                    var currentContentLink = _IHttpContextAccessor.HttpContext.GetContentLink();
                    try
                    {
                        var content = _contentLoader.Get<IContent>(currentContentLink);
                        type = content.Property["PageTypeName"].ToString();
                    }
                    catch (Exception ex)
                    {
                        return View("/Views/StatusCode/PageNotFound.cshtml");
                    }
                    segmentfor404 = "Page-Not-Found";
                    var statusCodePage = _contentLoader.GetBySegment(ContentReference.StartPage, segmentfor404, new LoaderOptions());
                    if (url.ToLower().StartsWith("/hcp"))
                    {
                        type = "HCP";
                        var hcpPage = _contentLoader.GetBySegment(ContentReference.StartPage, "hcp", new LoaderOptions());
                        currentContentLink = hcpPage.ContentLink;
                        statusCodePage = _contentLoader.GetBySegment(hcpPage.ContentLink, segmentfor404, new LoaderOptions());
                    }
                    if (url.ToLower().StartsWith("/he/"))
                    {
                        type = "HCP";
                        var hcpPage = _contentLoader.GetBySegment(ContentReference.StartPage, "he", new LoaderOptions());
                        currentContentLink = hcpPage.ContentLink;
                        statusCodePage = _contentLoader.GetBySegment(hcpPage.ContentLink, segmentfor404, new LoaderOptions());
                    }
                    if (url.ToLower().StartsWith("/ossix"))
                    {
                        type = "HCP";
                        var hcpPage = _contentLoader.GetBySegment(ContentReference.StartPage, "ossix", new LoaderOptions());
                        currentContentLink = hcpPage.ContentLink;
                        statusCodePage = _contentLoader.GetBySegment(hcpPage.ContentLink, segmentfor404, new LoaderOptions());
                    }
                    var pageRef = ContentReference.StartPage;
                    if (pageRef.ID > 0)
                    {
                        string SiteTemplate = string.Empty;
                        var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

                        if (type == "HCP")
                        {
                            PageData data = repository.Get<PageData>(currentContentLink);
                            SiteTemplate = Convert.ToString(data["SiteTemplate"]);
                        }
                        else
                        {
                            PageData data = repository.Get<PageData>(pageRef);
                            SiteTemplate = Convert.ToString(data["SiteTemplate"]);
                        }
                        if (statusCodePage is null)
                        {

                            return View("/Views/StatusCode/PageNotFound.cshtml");
                        }
                        if (statusCodePage.GetOriginalType().Name == "GenericError")
                        {
                            var currentPage = _contentLoader.Get<GenericError>(statusCodePage.ContentLink);
                            return new FilterContentForVisitor().ShouldFilter(statusCodePage)
                                ? View(statusCode)
                                : View(string.Format("/Views/{0}/PageNotFound.cshtml", SiteTemplate), PageViewModel.Create(currentPage));
                        }
                        else
                        {
                            var currentPage = _contentLoader.Get<Error>(statusCodePage.ContentLink);
                            return new FilterContentForVisitor().ShouldFilter(statusCodePage)
                                ? View(statusCode)
                                : View(string.Format("/Views/{0}/PageNotFound.cshtml", SiteTemplate), PageViewModel.Create(currentPage));
                        }

                    }
                    else
                    {
                        return View("/Views/StatusCode/PageNotFound.cshtml");
                    }
                }
                catch (Exception ex) { return View("/Views/StatusCode/PageNotFound.cshtml"); }
            }
            else
            {
                segmentfor404 = "404";
            }

            return View("/Views/StatusCode/PageNotFound.cshtml");
        }
    }
}
