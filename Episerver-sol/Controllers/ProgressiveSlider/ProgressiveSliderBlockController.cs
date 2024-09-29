using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.ProgressiveSlider;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.ProgressiveSlider
{
	[TemplateDescriptor(AvailableWithoutTag = true,
				   ModelType = typeof(ProgressiveSliderBlock),
				 TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent)]

	public class ProgressiveSliderBlockController : BlockComponent<ProgressiveSliderBlock>
	{

		protected override IViewComponentResult InvokeComponent(ProgressiveSliderBlock currentBlock)
		{
			return View("ProgressiveSliderBlock", currentBlock);
		}

		
	}
}
