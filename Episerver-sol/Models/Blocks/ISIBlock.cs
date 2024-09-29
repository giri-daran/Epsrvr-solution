using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using System.ComponentModel.DataAnnotations;
namespace EpiserverBH.Models.Blocks
{
	[ContentType(DisplayName = "ISIBlock", GUID = "85C1AB52-3978-4899-9F67-7CD618E9CDF2", Description = "ISIBlock", GroupName = EpiserverBH.Globals.GroupNames.ISIBlock, Order = 21)]
	//[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
	public class ISIBlock : BlockData
	{


		[Display(Name = "ModelId", Order = 1)]
		public virtual string ModelId { get; set; }

		[CultureSpecific]
		[Display(Name = "Title1", Description = "Title", GroupName = SystemTabNames.Content, Order = 2)]
		public virtual XhtmlString Title1 { get; set; }


		[CultureSpecific]
		[Display(Name = "Descriptions1", Description = "Descriptions", GroupName = SystemTabNames.Content, Order = 3)]
		public virtual XhtmlString Descriptions1 { get; set; }

		[CultureSpecific]
		[Display(Name = "Title2", Description = "Title", GroupName = SystemTabNames.Content, Order = 4)]
		public virtual XhtmlString Title2 { get; set; }


		[CultureSpecific]
		[Display(Name = "Descriptions2", Description = "Descriptions", GroupName = SystemTabNames.Content, Order = 5)]
		public virtual XhtmlString Descriptions2 { get; set; }

		[CultureSpecific]
        [Display(Name = "StaticTitle1", Description = "StaticTitle", GroupName = SystemTabNames.Content, Order = 6)]
        public virtual XhtmlString StaticTitle1 { get; set; }

        [CultureSpecific]
        [Display(Name = "StaticDescriptions1", Description = "StaticDescriptions", GroupName = SystemTabNames.Content, Order = 7)]
        public virtual XhtmlString StaticDescriptions1 { get; set; }

		[CultureSpecific]
		[Display(Name = "StaticTitle2", Description = "StaticTitle", GroupName = SystemTabNames.Content, Order = 8)]
		public virtual XhtmlString StaticTitle2 { get; set; }

		[CultureSpecific]
		[Display(Name = "StaticDescriptions2", Description = "StaticDescriptions", GroupName = SystemTabNames.Content, Order = 9)]
		public virtual XhtmlString StaticDescriptions2 { get; set; }

		[Display(Name = "PositionClass")]
		[SelectOne(SelectionFactoryType = typeof(IPositionClass))]
		public virtual string PositionClass { get; set; }

	}


	public class IPositionClass : ISelectionFactory
	{
		public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			return new ISelectItem[]
			{
				  new SelectItem() { Text="topPosition", Value = "topPosition" },
				   new SelectItem() { Text="bottomPosition", Value = "bottomPosition" },
				   new SelectItem() { Text="leftPosition", Value = "leftPosition" },
				   new SelectItem() { Text="rightPosition", Value = "rightPosition" }

			};
		}
	}


}