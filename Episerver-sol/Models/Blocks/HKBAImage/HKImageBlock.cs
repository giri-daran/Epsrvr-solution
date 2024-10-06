using EPiServer.Shell.ObjectEditing;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using System.ComponentModel.DataAnnotations;
namespace EpiserverBH.Models.Blocks.HKBAImage
{

    [ContentType(DisplayName = "HK-BA-Image-Block", GUID = "83F8F0AA-4EF1-446F-90A2-15DDDDA368D5", Description = "This Block is used Before After image Blocks", GroupName = Globals.GroupNames.HK)]
    //[SiteImageUrl("/assets/Bauschsurgical/img/gfx/layout.png")]
    public class HKImageBlock : BlockData
    {
        [Display(Name = "BA Image List", GroupName = "Information", Description = "Heading", Order = 1)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<BAImageList>))]
        public virtual IList<BAImageList> ImageList { get; set; }
    
    }
}