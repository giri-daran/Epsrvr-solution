using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Ossixusa;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Ossixusa
{
    public class OssixusaResourcesElementBlockController : BlockComponent<OssixusaResourcesElementBlock>
    {
        private static OssixusaResourcesElementBlock _block;
        protected override IViewComponentResult InvokeComponent(OssixusaResourcesElementBlock currentBlock)
        {
            _block = currentBlock;
            //return base.InvokeComponent(currentBlock);

            return View("/Views/Shared/Blocks/OssixusaResourcesElementBlock.cshtml", _block);
        }
    }
}