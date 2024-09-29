using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Animation;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Animation
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                   ModelType = typeof(AnimationContent),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent)]

    public class AnimationBlockController : BlockComponent<AnimationContent>
    {
        protected override IViewComponentResult InvokeComponent(AnimationContent currentBlock)
        {
            return View("AnimationBlock", currentBlock);
        }        
    }

}
