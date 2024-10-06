using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;


namespace EpiserverBH.Models.Blocks.Wistia
{
	[ContentType(DisplayName = "WistiaBlock-Block", GUID = "C4356211-1851-4A52-9848-9343CCC7F86C", Description = "WistiaBlock", GroupName = EpiserverBH.Globals.GroupNames.WistiaBlock, Order = 22)]
	//[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
	public class WistiaBlock : BlockData
	{

		[Display(Name = "BlockID", GroupName = SystemTabNames.Content, Description = "Block ID", Order = 1)]
		public virtual string BlockID { get; set; }

		[Display(Name = "VideoID", GroupName = SystemTabNames.Content, Description = "Video ID", Order = 2)]
		public virtual string VideoID { get; set; }

		[CultureSpecific]
		[Display(Name = "Title", GroupName = SystemTabNames.Content, Description = "Title", Order = 3)]
		public virtual XhtmlString Title { get; set; }

		[CultureSpecific]
		[Display(Name = "Video Title", GroupName = SystemTabNames.Content, Description = "Video Title", Order = 4)]
		public virtual XhtmlString VideoTitle { get; set; }

		[CultureSpecific]
		[Display(Name = "Video Descriptions", GroupName = SystemTabNames.Content, Description = "Video Descriptions", Order = 5)]
		public virtual XhtmlString VideoDescriptions { get; set; }

		[CultureSpecific]
		[Display(Name = "Custom Desktop Image", GroupName = SystemTabNames.Content, Description = "Custom Desktop Image", Order = 6)]
		[UIHint(UIHint.Image)]
		public virtual ContentReference DescImage { get; set; }

		[CultureSpecific]
		[Display(Name = "Custom Mobile Image", GroupName = SystemTabNames.Content, Description = "Custom Mobile Image", Order = 7)]
		[UIHint(UIHint.Image)]
		public virtual ContentReference MobImage { get; set; }

		[Display(Name = "Wistia Type", GroupName = SystemTabNames.Content, Description = "Wistia Type", Order = 8)]
		[SelectOne(SelectionFactoryType = typeof(IWistiaType))]
		public virtual string WistiaType { get; set; }

		[Display(Name = "Slide Title", GroupName = SystemTabNames.Content, Description = "Slide Title", Order = 9)]
		public virtual XhtmlString SlideTitle { get; set; }

		[Display(Name = "Wistia List", GroupName = "Information", Description = "Wistia List", Order = 10)]
		[EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<WistiaList>))]
		public virtual IList<WistiaList> WistiaList { get; set; }


	}



	public class IWistiaType : ISelectionFactory
	{
		public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			return new ISelectItem[]
			{
				new SelectItem() { Text="Inline", Value = "Inline" },
				new SelectItem() { Text="Popover", Value = "Popover" },
				new SelectItem() { Text="Slider", Value = "Slider" },
                new SelectItem() { Text="Grid", Value = "Grid" },
				new SelectItem() { Text="CustomSlider", Value = "CustomSlider" },
				new SelectItem() { Text="CustomSlick", Value="CustomSlick" }

			};
		}
	}


	

}