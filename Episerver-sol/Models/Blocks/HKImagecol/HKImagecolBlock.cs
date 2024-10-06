using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.HKImageContent
{
    [ContentType(DisplayName = "HKImagecol-Block", GUID = "F98BFE66-79F5-43CF-9D1E-D8F804A12821", Description = "This Block is used multiple column image Blocks", GroupName = EpiserverBH.Globals.GroupNames.HK, Order =20)]
    //[SiteImageUrl(EpiserverBH.Global.AssetsGraphicsFolderPath + "Logo.jpg")]
    public class HKImagecolBlock : BlockData
    {
        //[Display(Name = "Number of Columns", GroupName = SystemTabNames.Content, Description = "No of Columns",
        //    Order = 1)]
        //[SelectOne(SelectionFactoryType = typeof(NumberOfColumns))]
        //public virtual string NumberOfColumns { get; set; }

        [CultureSpecific]
        [Display(
                  Name = "Heading",
                  Description = "Heading",
                  GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual XhtmlString Title { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Subheading",
                    Description = "Sub Heading",
                    GroupName = SystemTabNames.Content, 
            Order = 3)]
        public virtual XhtmlString Subheading { get; set; }

        [CultureSpecific]
        [Display(
           Name = "Description",
           Description = "body description",
           GroupName = SystemTabNames.Content, 
            Order = 4)]
        public virtual XhtmlString Description { get; set; }     

        [CultureSpecific]
        [Display(
                    Name = "Desktop Image",
                    Description = "Image description",
                    GroupName = SystemTabNames.Content,
            Order = 5)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Mobile Image",
                    Description = "description",
                    GroupName = SystemTabNames.Content, Order = 6)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MobileImage { get; set; }

        [CultureSpecific]
        [Display(Name = "Image Position", GroupName = SystemTabNames.Content, Description = "Image Position", Order = 7)]
        [SelectOne(SelectionFactoryType = typeof(ImagePosition))]
        public virtual string ImagePosition { get; set; }

        [CultureSpecific]
        [Display(Name = "Text Alignment", GroupName = SystemTabNames.Content, Description = "Text Alignment",
            Order = 8)]
        [SelectOne(SelectionFactoryType = typeof(TextAlignment))]
        public virtual string TextAlignment { get; set; }

        [CultureSpecific]
        [Display(Name = "Background Color", GroupName = SystemTabNames.Content, Description = "Bg Colour",
            Order = 9)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColor))]
        public virtual string BackgroundColor { get; set; }



    }
}