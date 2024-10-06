using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;

namespace EpiserverBH.Models.Blocks.Accordion
{
    [ContentType(DisplayName = "AccordionSliderBlock", GUID = "8bb670ad-c75d-4cbe-a718-96dd912dc421", Description = "Block used to display image", GroupName = Globals.GroupNames.CustomBlocks)]
     [SiteImageUrl("/gfx/page-type-thumbnail.png")]
    public class AccordionSliderBlock : BlockData
    {
        [Display(Name = "Accordion Images List", GroupName = "Information", Description = "Heading", Order = 12)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<AccordionList>))]
        public virtual IList<AccordionList> AccordionSliderListBlock { get; set; }


        [Display(Name = "Accordion Title", GroupName = "Information", Description = "AccordionTitle", Order = 1)]
        [CultureSpecific]     
        public virtual XhtmlString AccordionTitle { get; set; }

        [Display(Name = "TitlePosition", GroupName = "Information", Description = "TitlePosition", Order = 2)]
        [SelectOne(SelectionFactoryType = typeof(TPosition))]
        [CultureSpecific]
        public virtual string TitlePosition { get; set; }

        [Display(Name = "Margin-Bottom", GroupName = "Information", Description = "MarginBottom", Order = 3)]
        [CultureSpecific]
        public virtual string MarginBottom { get; set; }

        [Display(Name = "Margin-Top", GroupName = "Information", Description = "MarginTop", Order = 4)]
        [CultureSpecific]
        public virtual string MarginTop { get; set; }

        [Display(Name = "ContainerLayout", GroupName = "Information", Description = "ContainerLayout ", Order = 5)]
        [SelectOne(SelectionFactoryType = typeof(IContainerLayout))]
        public virtual string ContainerLayout { get; set; }

        [Display(Name = "Container Size", GroupName = "Information", Description = "ContainerSize", Order = 6)]
        [SelectOne(SelectionFactoryType = typeof(IContainerLayoutSize))]
        public virtual string ContainerSize { get; set; }

        [Display(Name = "Max Number of Image", GroupName = "Information", Description = "MaxNumberofImage", Order = 7)]
        [SelectOne(SelectionFactoryType = typeof(MaxNumberofImage))]
        public virtual string MaxNumberofImage { get; set; }

      

        //[Display(Name = "Description Color", GroupName = "Information", Description = "Description Color", Order = 9)]
        //[CultureSpecific]
        //public virtual string DescriptionColor { get; set; }

        [Display(Name = "Link Color", GroupName = "Information", Description = "Link Color", Order = 8)]
        [CultureSpecific]
        public virtual string LinkColor { get; set; }

        [Display(Name = "Gradient", GroupName = "Information", Description = "Gradient ", Order = 9)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string Gradient { get; set; }


        [Display(Name = "Image Title Position", GroupName = "Information", Description = "ImgTitleDecPosition ", Order = 10)]
        [SelectOne(SelectionFactoryType = typeof(imgTitleDecPosition))]
        public virtual string ImgTitleDecPosition { get; set; }

        [Display(Name = "Close image Title Position", GroupName = "Information", Description = "CloseimgTitleDecPosition ", Order = 11)]
        [SelectOne(SelectionFactoryType = typeof(closeimgTitleDecPosition))]
        public virtual string CloseimgTitleDecPosition { get; set; }

        //[Display(Name = "Slide List", GroupName = "Information", Description = "Slide List ", Order = 11)]
        //[CultureSpecific]
        //public virtual string SlideList { get; set; }

    }

}