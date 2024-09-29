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
                   ModelType = typeof(ISIBlock),
                 TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]

    public class ISIBlockBlockController : BlockComponent<ISIBlock>
    {
        protected override IViewComponentResult InvokeComponent(ISIBlock currentBlock)
        {
            return View("ISIBlock", currentBlock);
        }        
    }

}
