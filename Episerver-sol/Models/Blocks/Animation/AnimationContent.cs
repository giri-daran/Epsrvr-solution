using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.PlugIn;
using EPiServer.Cms.Shell.Json.Internal;
using Newtonsoft.Json;

namespace EpiserverBH.Models.Blocks.Animation
{
    [ContentType(DisplayName = "Animation-Block", GUID = "D0052984-D742-44CD-B4EA-D36669DC9199", Description = "AnimationBlock", Order =143, GroupName = Globals.GroupNames.AnimationBlock)]
    public class AnimationContent : BlockData
    {
        [Display(Name = "BlockId", GroupName = "Information", Description = "Block Id", Order = 1)]
        public virtual string BlockId { get; set; }


        [Display(Name = "Title", GroupName = "Information", Description = "Animation Title", Order = 2)]
        [CultureSpecific]
        public virtual XhtmlString Title { get; set; }


        [Display(Name = "Animation Type", GroupName = "Information", Description = "Animation Type", Order = 3)]
        [SelectOne(SelectionFactoryType = typeof(IAnimationType))]
        public virtual string AnimationType { get; set; }

        [Display(Name = "Data Delay", GroupName = "Information", Description = "Data Delay", Order = 4)]
        public virtual string DataDelay { get; set; }

		[Display(Name = "Data Duration", GroupName = "Information", Description = "Data Duration", Order = 5)]
		public virtual string DataDuration { get; set; }



		[Display(Name = "Mobile Image", GroupName = "Information", Description = "Mobile Image", Order = 6)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MobileImage { get; set; }

		[Display(Name = "Mobile Image Position", GroupName = "Information", Description = "Mobile Image Position", Order = 7)]
		[SelectOne(SelectionFactoryType = typeof(TImagePosition))]
		[CultureSpecific]
		public virtual string MobileImagePosition { get; set; }


		[Display(Name = "Desktop Image", GroupName = "Information", Description = "Desktop Image", Order = 8)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference DesktopImage { get; set; }

		[Display(Name = "Desktop Image Position", GroupName = "Information", Description = "Desktop Image Position", Order = 9)]
		[SelectOne(SelectionFactoryType = typeof(TImagePosition))]
		[CultureSpecific]
		public virtual string DesktopImagePosition { get; set; }

		[Display(Name = "Background Color", GroupName = "Information", Description = "Background Color", Order = 10)]
        [CultureSpecific]
        public virtual string BackgroundColor { get; set; }

        [Display(Name = "Background Image", GroupName = "Information", Description = "Background Image", Order = 11)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference BackgroundImage { get; set; }


        [Display(Name = "Content", GroupName = "Information", Description = "Content", Order = 12)]
        [CultureSpecific]
        public virtual XhtmlString Content { get; set; }


        [Display(Name = "Link List", GroupName = "Information", Description = "Content", Order = 13)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<LinkList>))]
        public virtual IList<LinkList> LinkListBlock { get; set; }

    }


    public class IAnimationType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem() { Text = "Select Type", Value = "none" },
                new SelectItem() { Text = "Fade In", Value = "fadein" },
                new SelectItem() { Text = "Fade Up", Value = "fadeup" },
                new SelectItem() { Text = "Fade Out", Value = "fadeout" },
                new SelectItem() { Text = "Fade Top", Value = "fadetop" },
                new SelectItem() { Text = "Fade in Right", Value = "fadeinright" },
                new SelectItem() { Text = "Fade in Left", Value = "fadeinleft" },
                new SelectItem() { Text = "Zoom in", Value = "zoomin" },
                new SelectItem() { Text = "Graph Animate", Value = "graphanimate" },
                new SelectItem() { Text = "Number Rotate", Value = "numberrotate" },
                new SelectItem() { Text = "No Animation", Value = "noanimation" },
				new SelectItem() { Text = "typerotate1", Value = "typerotate1" },
				new SelectItem() { Text = "typerotate2", Value = "typerotate2" },
				new SelectItem() { Text = "typerotate3", Value = "typerotate3" },
				new SelectItem() { Text = "typerotate4", Value = "typerotate4" },
				new SelectItem() { Text = "typerotate5", Value = "typerotate5" },
			};
        }
    }

    public class TImagePosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                new SelectItem() { Text = "Select Position", Value = "none" },
                new SelectItem() { Text = "Top", Value = "top" },
                new SelectItem() { Text = "Left", Value = "left" },
                new SelectItem() { Text = "Right", Value = "right" },
                new SelectItem() { Text = "Bottom", Value = "bottom" },

            };
        }

    }

    public class LinkList
    {

        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        [Display(Name = "Link Url")]
        public virtual Url LinkPicker { get; set; }

        [Display(Name = "Link Text")]
        public XhtmlString LinkText { get; set; }

        [Display(Name = "Alt Tag")]
        public virtual string AltTag { get; set; }

        [Display(Name = "Link Position")]
        [SelectOne(SelectionFactoryType = typeof(TPosition))]
        [CultureSpecific]
        public virtual string LinkPosition { get; set; }

		[Display(Name = "Rotate Desktop Image")]
		[UIHint(UIHint.Image)]
		public virtual ContentReference RotateDesktopImage { get; set; }

		[Display(Name = "Rotate Mobile Image")]
		[UIHint(UIHint.Image)]
		public virtual ContentReference RotateMobileImage { get; set; }

		[Display(Name = "RotateX")]
		public virtual string rotateX { get; set; }

		[Display(Name = "DataMoveZ")]
		public virtual string datamovez { get; set; }

		[Display(Name = "MoveY")]
		public virtual string movey { get; set; }

	}

    public class TPosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                new SelectItem() { Text = "Select Position", Value = "none" },
                new SelectItem() { Text = "Top", Value = "top" },
                new SelectItem() { Text = "Left", Value = "left" },
                new SelectItem() { Text = "Right", Value = "right" },
                new SelectItem() { Text = "Bottom", Value = "bottom" },
                new SelectItem() { Text = "Top Center", Value = "topcenter" },
                new SelectItem() { Text = "Bottom Center", Value = "bottomcenter" },
            };
        }

    }

	



	[PropertyDefinitionTypePlugIn]
    public class AnimationContentProperty : PropertyList<LinkList> { }
}
