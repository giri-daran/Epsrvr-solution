using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.PlugIn;
using EPiServer.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Wistia
{
	public class WistiaList
    {
		
		[Display(Name = "SlideImage", GroupName = "Information", Description = "Slide Image", Order = 1)]
		[UIHint(UIHint.Image)]
		public virtual ContentReference SlideImage { get; set; }

		[Display(Name = "SlideVideoID", GroupName = "Information", Description = "Slide VideoID", Order = 2)]
		public virtual string SlideVideoID { get; set; }

		[Display(Name = "SlideText", GroupName = "Information", Description = "Slide Text", Order = 3)]
		public virtual XhtmlString SlideText { get; set; }

		[Display(Name = "WistiaGridID", GroupName = "Information", Description = "Wistia GridID", Order = 4)]
		public virtual string WistiaGridID { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(UrlConverter))]
		[Display(Name = "SliderLink", GroupName = "Information", Description = "Slider Link", Order = 5)]
		public virtual Url SliderLink { get; set; }

	}



	[PropertyDefinitionTypePlugIn]
    public class WistiaSliderProperty : PropertyList<WistiaList> { }


}