using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EpiserverBH.Models.Blocks.BadgeGrid;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(DisplayName = "PopupBlock-Block", GUID = "7B33FB2D-2CAF-4281-8215-839A49978A5C", Description = "PopupBlock", GroupName = EpiserverBH.Globals.GroupNames.PopupBlock, Order =21)]
    //[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
    public class PopupBlock : BlockData
    {


		[Display(Name = "modelId", Order = 1)]
		public virtual string modelId { get; set; }

		[CultureSpecific]
        [Display(
                   Name = "Title",
                   Description = "Title",
                   GroupName = SystemTabNames.Content,
                   Order = 2)]
        public virtual XhtmlString Title { get; set; }


        [CultureSpecific]
        [Display(
                    Name = "Descriptions",
                    Description = "Descriptions",
                    GroupName = SystemTabNames.Content,
                    Order = 3)]
        public virtual XhtmlString Descriptions { get; set; }


        [Display(Name = "hasCloseBtn", Order = 4)]
        [CultureSpecific]
        public virtual bool hasCloseBtn { get; set; }


        [Display(Name = "Link Details")]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<LinkList>))]
        public virtual IList<LinkList> LinkListBlock { get; set; }

        [Display(Name = "Bootstrap Version")]
        [SelectOne(SelectionFactoryType = typeof(BootstrapClass))]
        public virtual string BootstrapVersion { get; set; }




    }


    public class LinkList
    {

		[Display(Name = "LinkClass")]
		[SelectOne(SelectionFactoryType = typeof(ILinkClass))]
		public virtual string LinkClass { get; set; }


		[Display(Name = "Link Text")]
        public virtual string LinkText { get; set; }

        [Display(Name = "Link Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference LinkImage { get; set; }


    }
	public class ILinkClass : ISelectionFactory
	{
		public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			return new ISelectItem[]
			{
				  new SelectItem() { Text = "continue", Value = "continue" },
				   new SelectItem() { Text = "cancel", Value = "cancel" }
				  
			};
		}
	}


	[PropertyDefinitionTypePlugIn]
    public class PopupListProperty : PropertyList<LinkList> { }

}