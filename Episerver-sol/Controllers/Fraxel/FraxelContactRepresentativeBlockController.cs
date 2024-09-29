using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks.Fraxel;
using Microsoft.AspNetCore.Mvc;


namespace EpiserverBH.Controllers.Fraxel
{
    public class FraxelContactRepresentativeBlockController : BlockController<FraxelContactRepresentativeBlock>
    {
        private static FraxelContactRepresentativeBlock _block;

        /* public override ActionResult Index(FraxelContactRepresentativeBlock currentContent)
         {
             _block = currentContent;

             return base.Index(currentContent);
         }*/

        protected override IViewComponentResult InvokeComponent(FraxelContactRepresentativeBlock currentBlock)
        {
            _block = currentBlock;
            return base.InvokeComponent(currentBlock);
        }

    }
}
