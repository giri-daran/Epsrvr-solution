using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Wistia;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Wistia
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                   ModelType = typeof(WistiaBlock),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]

    public class WistiaBlockBlockController : BlockComponent<WistiaBlock>
    {
        protected override IViewComponentResult InvokeComponent(WistiaBlock currentBlock)
        {
            return View("WistiaBlock", currentBlock);
        }        
    }

}
