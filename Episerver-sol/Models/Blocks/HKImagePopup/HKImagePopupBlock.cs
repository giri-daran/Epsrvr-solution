using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.HKImagePopup
{
    [ContentType(DisplayName = "HKPopupImage-Block", GUID = "7FD71175-5525-4BD4-B90E-438B1E9D3921", Description = "This Block is used Popup Image Blocks", GroupName = Globals.GroupNames.HK)]
    //[SiteImageUrl("/assets/Bauschsurgical/img/gfx/layout.png")]
    public class HKImagePopupBlock : BlockData
    {
        [Display(Name = "Popup Image List", GroupName = "Information", Description = "Heading", Order = 1)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<ImageList>))]
        public virtual IList<ImageList> ImageList { get; set; }

       

    }
}

