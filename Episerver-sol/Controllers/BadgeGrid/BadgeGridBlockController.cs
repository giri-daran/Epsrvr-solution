using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.BadgeGrid;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.BadgeGrid
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                   ModelType = typeof(BadgeGridBlock),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]

    public class BadgeGridBlockController : BlockComponent<BadgeGridBlock>
    {
        protected override IViewComponentResult InvokeComponent(BadgeGridBlock currentBlock)
        {
            return View("BadgeGridBlockControl", currentBlock);
        }        
    }

}
