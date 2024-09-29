using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers
{
    [TemplateDescriptor(AvailableWithoutTag = true,
                   ModelType = typeof(PopupBlock),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]

    public class PopupBlockBlockController : BlockComponent<PopupBlock>
    {
        protected override IViewComponentResult InvokeComponent(PopupBlock currentBlock)
        {
            return View("PopupBlock", currentBlock);
        }        
    }

}
