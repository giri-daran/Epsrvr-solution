using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EpiserverBH.Models.Blocks.MarketoForm;
using Microsoft.AspNetCore.Mvc;


namespace EpiserverBH.Controllers.MarketoForm
{
    [TemplateDescriptor(AvailableWithoutTag = true,

                 ModelType = typeof(MarketoFormContainer),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent)]

   /* [TemplateDescriptor(AvailableWithoutTag = true,
                 Default = true,
                 ModelType = typeof(MarketoFormContainer),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]*/

    public class MarketoFormContainerController : FormContainerBlockController
    {
        protected override IViewComponentResult InvokeComponent(FormContainerBlock currentBlock)
        {

            /*var viewResult = base.Index(currentBlock) as PartialViewResult;
            viewResult.ViewName = "MarketoFormContainer";
            return viewResult;*/

            return base.InvokeComponent(currentBlock);
        }
    }
}