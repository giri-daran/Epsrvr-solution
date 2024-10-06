using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.ProgressiveSlider
{
	[ContentType(DisplayName = "Progressive Slider Block", GUID = "BFB3370B-22B2-490A-80D4-72AF476FAA92", Description = "ProgressiveSliderBlock", Order = 145, GroupName = Globals.GroupNames.ProgressiveSliderBlock)]
	public class ProgressiveSliderBlock : BlockData
	{
		
		[Display(Name = "BlockID", GroupName = SystemTabNames.Content, Description = "Block ID", Order = 1)]
		public virtual string BlockID { get; set; }

		[CultureSpecific]
		[Display(Name = "Title", GroupName = SystemTabNames.Content, Description = "Title", Order = 2)]
		public virtual XhtmlString Title { get; set; }

		[CultureSpecific]
		[Display(Name = "Description", GroupName = SystemTabNames.Content, Description = "Description", Order = 3)]
		public virtual XhtmlString Description { get; set; }

		[Display(Name = "Thumb List", GroupName = "Information", Description = "Thumb List", Order = 4)]
		[EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<ThumbList>))]
		public virtual IList<ThumbList> ThumbList { get; set; }

	}



	public class ThumbList
	{

        [Display(Name = "Gender", GroupName = "Information", Description = "Gender", Order = 1)]
        [SelectOne(SelectionFactoryType = typeof(TGenderType))]
        [CultureSpecific]
        public virtual string Gender { get; set; }

        [CultureSpecific]
        [Display(Name = "Slider Title", GroupName = SystemTabNames.Content, Description = "Slider Title", Order = 2)]
        public virtual XhtmlString SliderTitle { get; set; }

        [CultureSpecific]
        [Display(Name = "Thumb Description", GroupName = SystemTabNames.Content, Description = "Thumb Description", Order = 3)]
        public virtual XhtmlString ThumbDescription { get; set; }

        [Display(Name = "Image Stage", GroupName = "Information", Description = "Image Stage", Order = 4)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference ImageStage { get; set; }

        [Display(Name = "Image Stage Alt Description", GroupName = SystemTabNames.Content, Description = "Image Stage Alt Description", Order = 5)]
        public virtual string ImageStageAltDescription { get; set; }

        [Display(Name = "ImageClass", GroupName = SystemTabNames.Content, Description = "Image Class", Order = 6)]
		public virtual string ImageClass { get; set; }

        [Display(Name = "Image List", GroupName = "Information", Description = "Image List", Order = 7)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<ImageList>))]
        public virtual IList<ImageList> ImageList { get; set; }

    }

	public class ImageList
	{
        [Display(Name = "Image Stage", GroupName = "Information", Description = "Image Stage", Order = 1)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference ImageStage { get; set; }

        [Display(Name = "Image Stage Description", GroupName = SystemTabNames.Content, Description = "Image Stage Description", Order = 2)]
        public virtual string ImageStageDescription { get; set; }

        [Display(Name = "Image Hover Text", GroupName = SystemTabNames.Content, Description = "Image Hover Text", Order = 3)]
        public virtual string ImageHoverText { get; set; }

        [Display(Name = "Image Alt Description", GroupName = SystemTabNames.Content, Description = "Image Alt Description", Order = 4)]
        public virtual string ImageAltDescription { get; set; }
    }


    public class TGenderType : ISelectionFactory
	{
		public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			return new ISelectItem[]
			{
				new SelectItem() { Text = "Select Type", Value = "none" },
				new SelectItem() { Text = "Male", Value = "Male" },
				new SelectItem() { Text = "Female", Value = "Female" },
			};
		}

	}


    [PropertyDefinitionTypePlugIn]
    public class ProgressiveSliderThumbProperty : PropertyList<ThumbList> { }

    [PropertyDefinitionTypePlugIn]
	public class ProgressiveSliderImageProperty : PropertyList<ImageList> { }
}
