using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.HKImageContent
{
    [ContentType(DisplayName = "HKImageContent-Block", GUID = "30F63D8B-0B20-42F1-8480-F983ED0D874E", Description = "HKImageContentBlock", GroupName = EpiserverBH.Globals.GroupNames.HK, Order =20)]
    //[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
    public class HKBlock : BlockData
    {
        //[Display(Name = "Number of Columns", GroupName = SystemTabNames.Content, Description = "No of Columns",
        //    Order = 11)]
        //[SelectOne(SelectionFactoryType = typeof(NumberOfColumns))]
        //public virtual string NumberOfColumns { get; set; }

        [CultureSpecific]
        [Display(Name = "BlockID", GroupName = SystemTabNames.Content, Description = "Block ID", Order = 1)]
        public virtual string BlockID { get; set; }

        [CultureSpecific]
        [Display(Name = "Image Position", GroupName = SystemTabNames.Content, Description = "Image Position", Order = 5)]
        [SelectOne(SelectionFactoryType = typeof(ImagePosition))]
        public virtual string ImagePosition { get; set; }

        [CultureSpecific]
        [Display(Name = "Curve Position", GroupName = SystemTabNames.Content, Description = "Curve Position", Order = 7)]
        [SelectOne(SelectionFactoryType = typeof(CurvePosition))]
        public virtual string CurvePosition { get; set; }

        [CultureSpecific]
        [Display(Name = "Text Alignment", GroupName = SystemTabNames.Content, Description = "Text Alignment",
            Order = 6)]
        [SelectOne(SelectionFactoryType = typeof(TextAlignment))]
        public virtual string TextAlignment { get; set; }

        [CultureSpecific]
        [Display(Name = "Background Color", GroupName = SystemTabNames.Content, Description = "Bg Colour",
            Order = 8)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColor))]
        public virtual string BackgroundColor { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Desktop Image",
                    Description = "description",
                    GroupName = SystemTabNames.Content,
                    Order = 3)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Mobile Image",
                    Description = "description",
                    GroupName = SystemTabNames.Content,
                    Order = 4)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MobileImage { get; set; }

        [CultureSpecific]
        [Display(
                   Name = "Title",
                   Description = "Title",
                   GroupName = SystemTabNames.Content,
                   Order = 1)]
        public virtual XhtmlString Title { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Subheading",
                    Description = "Sub Heading",
                    GroupName = SystemTabNames.Content,
                    Order = 2)]
        public virtual XhtmlString Subheading { get; set; }

        //[CultureSpecific]
        //[Display(
        //   Name = "Description",
        //   Description = "body description",
        //   GroupName = SystemTabNames.Content,
        //   Order = 9)]
        //public virtual XhtmlString ExtraDescription { get; set; }

        [CultureSpecific]
        [Display(Name = "Image Section", GroupName = SystemTabNames.Content, Description = "Image Section", Order = 15)]
        public virtual string ImageSection { get; set; }

    }
}