using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Cms.Shell.Json.Internal;
using Newtonsoft.Json;
using EPiServer.PlugIn;
using EPiServer.Web;

namespace EpiserverBH.Models.Blocks.HKBAImage
{

    public class BAImageList
    {

        [Display(Name = "Title1")]
        public virtual XhtmlString Title { get; set; }

        [Display(Name = "Title2")]
        public virtual XhtmlString Title2 { get; set; }

        [Display(Name = "Before Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference BeforeImage { get; set; }

        [Display(Name = "After Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference AfterImage { get; set; }

        [Display(Name = "Before Image Mobile")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference BeforeImageMobile { get; set; }

        [Display(Name = "After Image Mobile")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference AfterImageMobile { get; set; }


        [Display(Name = "Image Width")]
        public virtual string ImageWidth { get; set; }

    }



    [PropertyDefinitionTypePlugIn]
    public class ImageListContent : PropertyList<BAImageList> { }

}