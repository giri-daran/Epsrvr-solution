using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EpiserverBH.Controllers
{
    
    [TemplateDescriptor(AvailableWithoutTag = true,
                  ModelType = typeof(GenericAccordionBlock),
                TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]
    public class GenericAccordionBlockController : BlockComponent<GenericAccordionBlock>
    {
        protected override IViewComponentResult InvokeComponent(GenericAccordionBlock currentBlock)
        {

            return View("GenericAccordionBlockControl", currentBlock);
        }
    }
}