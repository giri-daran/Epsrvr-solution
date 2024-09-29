using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using Microsoft.AspNetCore.Mvc;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinProfileBlockController : BlockComponent<ArestinProfileBlock>
    {
        private static ArestinProfileBlock _block;

        protected override IViewComponentResult InvokeComponent(ArestinProfileBlock currentContent)
        {
            _block = currentContent;

            return View(currentContent);
        }

    }

}