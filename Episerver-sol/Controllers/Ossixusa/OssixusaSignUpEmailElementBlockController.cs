using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Ossixusa;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Ossixusa
{
    public class OssixusaSignUpEmailElementBlockController : BlockComponent<OssixusaSignUpEmailElementBlock>
    {
        private static OssixusaSignUpEmailElementBlock _block;

        protected override IViewComponentResult InvokeComponent(OssixusaSignUpEmailElementBlock currentBlock)
        {
            _block = currentBlock;
            //return base.InvokeComponent(currentBlock);

            return View("/Views/Shared/Blocks/OssixusaSignUpEmailElementBlock.cshtml", _block);
        }
    }
}
