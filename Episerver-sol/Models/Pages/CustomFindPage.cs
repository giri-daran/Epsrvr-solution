using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EpiserverBH.Models.Blocks;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Pages
{
    [SiteContentType(DisplayName = "CustomFindPage", GroupName = EpiserverBH.Globals.GroupNames.Specialized, GUID = "ba13c216-5883-492b-a970-8531be82a39a", Description = "Generic Find Page")]
    [SiteImageUrl]
    public class CustomFindPage : SitePageData, IHasRelatedContent, ISearchPage
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            PageSize = 20;
            ExcerptLength = 200;
            HitImagesHeight = 30;
            base.SetDefaultValues(contentType);
        }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(IContentData) }, new[] { typeof(JumbotronBlock) })]
        public virtual ContentArea RelatedContentArea { get; set; }

        /// <summary>
        /// Allow editors to control how many hits should be displayed
        /// on each search result listing when paging.
        /// </summary>
        [Range(1, 100)]
        [DefaultValue(20)]
        [Required]
        public virtual int PageSize { get; set; }

        /// <summary>
        /// Allow editors to control wether matching keywords in 
        /// search hit titles should be highlighted.
        /// </summary>
        public virtual bool HighlightTitles { get; set; }

        /// <summary>
        /// Allow editors to control wether matching keywords in 
        /// excerpt texts for search hits should be highlighted.
        /// If false the beginning of the search text will be retrieved.
        /// </summary>
        public virtual bool HighlightExcerpts { get; set; }

        /// <summary>
        /// Allow editors to specify the hight of hit images.
        /// </summary>
        [Range(0, 300)]
        [DefaultValue(30)]
        [Required]
        public virtual int HitImagesHeight { get; set; }

        /// <summary>
        /// Allow editors to specify length of excerpt text to 
        /// retrieve and show for each search hit.
        /// </summary>
        [Range(0, 1000)]
        [DefaultValue(200)]
        [Required]
        public virtual int ExcerptLength { get; set; }

        /// <summary>
        /// Allow search query to combine multiple search terms with AND
        /// </summary>
        public virtual bool UseAndForMultipleSearchTerms { get; set; }
        public virtual bool ASPageType { get; set; }
        public virtual bool ASDocumentType { get; set; }
        public virtual bool ASImageType { get; set; }
        public virtual bool ASExactSearch { get; set; }



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
      Order = 312)]
        [CultureSpecific]
        public virtual ContentArea Banner { get; set; }

        [Display(
           GroupName = SystemTabNames.Content,
           Order = 313)]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

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
            Order = 316)]
        public virtual ContentArea ContentArea_1 { get; set; }


        [Display(
            GroupName = SystemTabNames.Content,
            Order = 317)]
        public virtual ContentArea ContentArea_2 { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 318)]
        public virtual ContentArea ContentArea_3 { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 319)]
        public virtual ContentArea ContentArea_4 { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea ContentArea_5 { get; set; }

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


        //[Display(GroupName = SystemTabNames.Settings, Order = 1)]
        //public virtual string SiteLayout { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual Blocks.SiteLogotypeBlock SiteLogotype { get; set; }
    }
}