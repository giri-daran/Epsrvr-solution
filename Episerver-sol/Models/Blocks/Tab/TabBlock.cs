using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.Shell.ObjectEditing;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Tab
{
    [ContentType(DisplayName = "TabBlock-Block", GUID = "20911030-A5DE-4144-A6A0-B73AECDEDB5C", Description = "TabBlock", GroupName = EpiserverBH.Globals.GroupNames.TabBlock)]
	//[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
	public class TabBlock : BlockData
	{

        [Display(Name = "BlockID", GroupName = "Information", Description = "Block ID", Order = 1)]
        public virtual string BlockID { get; set; }

        [Display(Name = "Tab List", GroupName = "Information", Description = "Tab List", Order = 2)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<TabList>))]
        public virtual IList<TabList> TabList { get; set; }

    }

}