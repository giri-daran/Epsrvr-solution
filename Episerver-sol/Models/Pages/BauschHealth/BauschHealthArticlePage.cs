using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Pages.BauschHealth
{
    [ContentType(DisplayName = "BauschHealthArticlePage", GUID = "b4c792d5-d817-48fc-bb5a-16c3a9bcd5b5", Description = "Article page", GroupName = EpiserverBH.Globals.GroupNames.EpiserverBH, Order = 13)]
    [SiteImageUrl(Globals.AssetsGraphicsFolderPath + "Logo.jpg")]
    public class BauschHealthArticlePage : SitePageData
    {
        [Display(GroupName = SystemTabNames.Settings, Order = 1, Description = "Site Template", Name = "Site Template")]
        public virtual String SiteTemplate { get; set; }

        [Display(GroupName = SystemTabNames.PageHeader, Order = 2, Description = "Page CSS", Name = "Page CSS")]
        // [BackingType(typeof(PropertyStringList))]
        public virtual string[] SiteCSS { get; set; }


        [Display(GroupName = SystemTabNames.PageHeader, Order = 3, Description = "Page JS", Name = "Page JS")]
        //[BackingType(typeof(PropertyStringList))]
        public virtual string[] SiteJs { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 4, Description = "Google Tag Manager", Name = "Google Tag Manager")]
        public virtual String GoogleTagManager { get; set; }

        [Display(
      GroupName = SystemTabNames.Content,
      Order = 310)]
        [CultureSpecific]
        public virtual ContentArea HeaderLinksMobile { get; set; }

        [Display(
       GroupName = SystemTabNames.Content,
       Order = 311)]
        [CultureSpecific]
        public virtual ContentArea HeaderLinksAboveMenu { get; set; }

        [Display(
           GroupName = SystemTabNames.Content,
           Order = 314)]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 315)]
        public virtual ContentArea RawHTMLContent { get; set; }

        [Display(
      GroupName = SystemTabNames.Content,
      Order = 312)]
        [CultureSpecific]
        public virtual ContentArea Banner { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 317)]
        public virtual ContentArea ContentArea_2 { get; set; }

        [CultureSpecific]
        [Required(AllowEmptyStrings = false)]
        [Display(
       Name = "Article Title",
       Description = "Title",
       GroupName = SystemTabNames.Content,
       Order = 14)]
        public virtual String ArticleTitle { get; set; }

        [CultureSpecific]
        [Required(AllowEmptyStrings = false)]
        [Display(
        Name = "Published Date",
        Description = "Date",
        GroupName = SystemTabNames.Content,
        Order = 12)]
        public virtual DateTime Publisheddate { get; set; }

        [CultureSpecific]
        [Display(
       Name = "Header Article",
       Description = "Header Area",
       GroupName = SystemTabNames.Content,
       Order = 1)]
        public virtual ContentArea HeadingArticle { get; set; }

        

        [CultureSpecific]
        [Display(
       Name = "Full Article",
       Description = "Article Area",
       GroupName = SystemTabNames.Content,
       Order = 10)]
        public virtual XhtmlString FullArticle { get; set; }

        [CultureSpecific]
        [Required(AllowEmptyStrings = false)]
        [UIHint(UIHint.Image)]
        [Display(Name = "BH Image",
            Description = "BHHImage",
            GroupName = SystemTabNames.Content,
            Order = 13)]
        public virtual ContentReference BHImage { get; set; }

        [CultureSpecific]
        [Display(
       Name = "Footer Article",
       Description = "Footer Area",
       GroupName = SystemTabNames.Content,
       Order = 1000)]
        public virtual ContentArea Footerarticlearea { get; set; }

        [CultureSpecific]
        //[Required(AllowEmptyStrings = false)]
        [Display(
       Name = "Short Description",
       Description = "Description",
       GroupName = SystemTabNames.Content,
       Order = 6)]
        public virtual string ShortDesc { get; set; }

        [CultureSpecific]
        [Display(Name = "Article Type",
            Description = "Type",
            GroupName = SystemTabNames.Content,
            Order = 8)]
        [SelectOne(SelectionFactoryType = typeof(ArticleType))]
        public virtual string ArticleType { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 319)]
        public virtual ContentArea ContentArea_4 { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea ContentArea_5 { get; set; }

        [Display(
            Name = "ContentArea_6",
       Description = "ContentArea_6",
          GroupName = SystemTabNames.Content,
          Order = 321)]
        public virtual ContentArea ContentArea_6 { get; set; }

        [Display(
         GroupName = SystemTabNames.Content,
         Order = 323)]
        public virtual ContentArea ContentArea_7 { get; set; }


        [Display(
        GroupName = SystemTabNames.Content,
        Order = 316)]
        [CultureSpecific]
        public virtual ContentArea BodyISI { get; set; }

        [Display(
         GroupName = SystemTabNames.Content,
         Order = 317)]
        [CultureSpecific]
        public virtual ContentArea Footer { get; set; }

        [Display(
        GroupName = SystemTabNames.Content,
        Order = 318)]
        [CultureSpecific]
        public virtual ContentArea FixedISI { get; set; }

        [CultureSpecific]
        [Required(AllowEmptyStrings = false)]
        [Display(
        Name = "Description",
        Description = "Description",
        GroupName = SystemTabNames.Content,
        Order = 7)]
        public virtual string Description { get; set; }

        public virtual bool IsPublish { get; set; }
    }

    public class ArticleType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Newsroom", Value = "Newsroom" }, new SelectItem() { Text = "Internal Announcement", Value = "Internal" }
        };
        }

    }
}