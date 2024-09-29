using EPiServer.Web.Mvc;
using EpiserverBH.Models.ViewModels;
using EpiserverBH.Models.Pages.BauschHealth;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.BauschHealth
{
    public class BauschHealthArticlePageController : PageController<BauschHealthArticlePage>
    {
        public IActionResult Index(BauschHealthArticlePage currentPage)
        {
            var pageRef = ContentReference.StartPage;
            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();


            var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            var contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();
            // loading a block type
            var contentType = contentTypeRepository.Load<BauschHealthArticlePage>();
            // getting the list of block type usages, each usage object has properties ContentLink, LanguageBranch and Name
            var usages = contentModelUsage.ListContentOfContentType(contentType);

            PageData data = repository.Get<PageData>(pageRef);

            var model = PageViewModel.Create(currentPage);

            ViewData["ArticleTitle"] = model.CurrentPage.ArticleTitle;


            return View(string.Format("~/Views/BauschHealth/BauschHealthArticlePage.cshtml"), model);
        }
    }
}