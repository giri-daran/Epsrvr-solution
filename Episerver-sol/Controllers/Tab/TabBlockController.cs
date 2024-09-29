using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Tab;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Tab
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                   ModelType = typeof(TabBlock),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]

    public class TabBlockController : BlockComponent<TabBlock>
    {
        protected override IViewComponentResult InvokeComponent(TabBlock currentBlock)
        {
            return View("TabBlock", currentBlock);
        }        
    }

}
