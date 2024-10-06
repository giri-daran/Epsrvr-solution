using EPiServer.PlugIn;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Tab
{
	public class TabList
    {
		
		[Display(Name = "TabID", GroupName = "Information", Description = "Tab ID", Order = 1)]
		public virtual string TabID { get; set; }

		[Display(Name = "TabText", GroupName = "Information", Description = "Tab Text", Order = 2)]
		[CultureSpecific]
		public virtual string TabText { get; set; }

		[Display(Name = "Content", GroupName = "Information", Description = "Tab Content", Order = 3)]
		[CultureSpecific]
		public virtual XhtmlString Content { get; set; }

	}



	[PropertyDefinitionTypePlugIn]
    public class TabSliderProperty : PropertyList<TabList> { }


}