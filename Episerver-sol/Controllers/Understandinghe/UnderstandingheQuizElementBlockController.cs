using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Understandinghe;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Understandinghe
{
    public class UnderstandingheQuizElementBlockController : BlockComponent<UnderstandingheQuizBlock>
    {
        private static UnderstandingheQuizBlock _block;

        protected override IViewComponentResult InvokeComponent(UnderstandingheQuizBlock currentContent)
        {
            _block = currentContent;

            return View("/Views/Shared/Blocks/UnderstandingheQuizBlock.cshtml", _block);
        }
    }    
}