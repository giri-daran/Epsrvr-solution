using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.BadgeGrid;
using EpiserverBH.Models.Blocks.DescriptionList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EpiserverBH.Controllers.DescriptionList
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                  Default = true,
                  ModelType = typeof(DescriptionListBlock),
                TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]
    public class DescriptionListBlockController : BlockComponent<DescriptionListBlock>
    {
        protected override IViewComponentResult InvokeComponent(DescriptionListBlock currentBlock)
        {
            return View("DescriptionListBlockControl", currentBlock);
        }
    }
}

