using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Cms.Shell.Json.Internal;
using Newtonsoft.Json;
using EPiServer.PlugIn;
using EPiServer.Web;

namespace EpiserverBH.Models.Blocks.HKImagePopup
{

    public class ImageList
    {

        [Display(Name = "Desktop Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference DesktopImage { get; set; }

        [Display(Name = "Mobile Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MobileImage { get; set; }


        [Display(Name = "Title")]
        public virtual XhtmlString Title { get; set; }


        [Display(Name = "Description1")]
        public virtual XhtmlString Description { get; set; }

        [Display(Name = "Description2")]
        public virtual XhtmlString Description2 { get; set; }


        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link { get; set; }

        [Display(Name = "Link Title")]
        [CultureSpecific]
        public virtual string LinkTitle { get; set; }

        [Display(Name = "FootNote")]
        public virtual XhtmlString FootNote { get; set; }      

    }



    [PropertyDefinitionTypePlugIn]
    public class ImageListContent : PropertyList<ImageList> { }

}