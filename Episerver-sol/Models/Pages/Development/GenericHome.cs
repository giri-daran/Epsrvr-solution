using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Pages.Development
{
    [ContentType(DisplayName = "GenericHome", GUID = "c3430223-ba1f-4e58-b324-c0898f1a34df", Description = "Development Home Template", GroupName = "Development")]
    public class GenericHome : SitePageData
    {
        [Display(GroupName = SystemTabNames.Settings, Order = 1, Description = "Site Template", Name = "Site Template")]
        public virtual String SiteTemplate { get; set; }

        [Display(GroupName = SystemTabNames.PageHeader, Order = 2, Description = "Page CSS", Name = "Page CSS")]
        public virtual string[] SiteCSS { get; set; }


        [Display(GroupName = SystemTabNames.PageHeader, Order = 3, Description = "Page JS", Name = "Page JS")]
        public virtual string[] SiteJs { get; set; }


        [Display(GroupName = SystemTabNames.Settings, Order = 7, Description = "Google Tag Manager", Name = "Google Tag Manager")]
        public virtual String GoogleTagManager { get; set; }

        [Display(Name = "OG-Title", Description = "OG-Title",
      GroupName = Globals.GroupNames.MetaData,
      Order = 600)]
        [CultureSpecific]
        public virtual String OGTitle
        {
            get
            {
                var OGTitle = this.GetPropertyValue(p => p.OGTitle);

                // Use explicitly set meta title, otherwise fall back to page name
                return !string.IsNullOrWhiteSpace(OGTitle)
                        ? OGTitle
                        : PageName;
            }
            set { this.SetPropertyValue(p => p.OGTitle, value); }
        }

        [Display(Name = "OG-Description", Description = "OG-Description",
      GroupName = Globals.GroupNames.MetaData,
      Order = 601)]
        [UIHint(UIHint.Textarea)]
        public virtual String OGDescription
        {
            get
            {
                var OGDescription = this.GetPropertyValue(p => p.OGDescription);

                // Use explicitly set OG- Description, otherwise fall back to MetaDescription
                return !string.IsNullOrWhiteSpace(OGDescription)
                        ? OGDescription
                        : MetaDescription;
            }
            set { this.SetPropertyValue(p => p.OGDescription, value); }
        }

        [Display(Name = "OG-Image", Description = "OG-Image", GroupName = Globals.GroupNames.MetaData,
         Order = 602)]
        //public virtual Blocks.SiteLogotypeBlock OGImage { get; set; }

        [UIHint(UIHint.Image)]
        public virtual Url OGImage
        {
            get
            {
                var url = this.GetPropertyValue(b => b.OGImage);

                return url == null || url.IsEmpty()
                            ? new Url("/Static/gfx/logotype.png")
                            : url;
            }
            set
            {
                this.SetPropertyValue(b => b.OGImage, value);
            }
        }

        [Display(Name = "HeaderLinks", Description = "HeaderLinks Section",
         GroupName = Globals.GroupNames.Header,
         Order = 1)]
        [CultureSpecific]
        public virtual LinkItemCollection HeaderLinksSection { get; set; }

        [Display(Name = "Header", Description = "Header Section",
       GroupName = Globals.GroupNames.Header,
       Order = 2)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea HeaderLinks { get; set; }

        [Display(Name = "Banner", Description = "Banner Section",
        GroupName = SystemTabNames.Content,
        Order = 3)]
       
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML),
                               typeof(EpiserverBH.Models.Blocks.EditorialBlock),
                               typeof(EpiserverBH.Models.Blocks.HKBAImage.HKImageBlock),
                               typeof(EpiserverBH.Models.Blocks.HKImageContent.HKBlock),
                               typeof(EpiserverBH.Models.Blocks.HKAccordion.HKAccordionBlock),
                               typeof(EpiserverBH.Models.Blocks.HKImagePopup.HKImagePopupBlock),
                               typeof(EpiserverBH.Models.Blocks.HKLocationBlock.HKLocation),
                               typeof(EpiserverBH.Models.Blocks.BadgeGrid.BadgeGridBlock),
                               typeof(EpiserverBH.Models.Blocks.PopupBlock),
                               typeof(EpiserverBH.Models.Blocks.Wistia.WistiaBlock),
                               typeof(EpiserverBH.Models.Blocks.ISIBlock)
        })]
        public virtual ContentArea BannerSlider { get; set; }

        [Display(Name = "BannerContent", Description = "BannerContent Section",
        GroupName = SystemTabNames.Content,
        Order = 4)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML), 
                               typeof(EpiserverBH.Models.Blocks.EditorialBlock), 
                               typeof(EpiserverBH.Models.Blocks.HKBAImage.HKImageBlock), 
                               typeof(EpiserverBH.Models.Blocks.HKImageContent.HKBlock),
                               typeof(EpiserverBH.Models.Blocks.HKAccordion.HKAccordionBlock),
                               typeof(EpiserverBH.Models.Blocks.HKImagePopup.HKImagePopupBlock),
                               typeof(EpiserverBH.Models.Blocks.HKLocationBlock.HKLocation),
                               typeof(EpiserverBH.Models.Blocks.BadgeGrid.BadgeGridBlock),
                               typeof(EpiserverBH.Models.Blocks.PopupBlock),
                               typeof(EpiserverBH.Models.Blocks.Wistia.WistiaBlock),
                               typeof(EpiserverBH.Models.Blocks.ISIBlock)
        })]
        [CultureSpecific]
        public virtual ContentArea BannerContent { get; set; }


        [Display(Name = "ContentArea", Description = "ContentArea Section",
           GroupName = SystemTabNames.Content,
           Order = 5)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML),
                               typeof(EpiserverBH.Models.Blocks.EditorialBlock),
                               typeof(EpiserverBH.Models.Blocks.HKBAImage.HKImageBlock),
                               typeof(EpiserverBH.Models.Blocks.HKImageContent.HKBlock),
                               typeof(EpiserverBH.Models.Blocks.HKAccordion.HKAccordionBlock),
                               typeof(EpiserverBH.Models.Blocks.HKImagePopup.HKImagePopupBlock),
                               typeof(EpiserverBH.Models.Blocks.HKLocationBlock.HKLocation),
                               typeof(EpiserverBH.Models.Blocks.BadgeGrid.BadgeGridBlock),
                               typeof(EpiserverBH.Models.Blocks.PopupBlock),
                               typeof(EpiserverBH.Models.Blocks.Wistia.WistiaBlock),
                               typeof(EpiserverBH.Models.Blocks.ISIBlock)
        })]
        public virtual ContentArea ContentArea { get; set; }

        [Display(Name = "FormArea", Description = "FormArea Section",
          GroupName = Globals.GroupNames.Forms,
          Order = 6)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML), typeof(EPiServer.Forms.Implementation.Elements.FormContainerBlock) })]
        public virtual ContentArea FormArea { get; set; }

        [Display(Name = "Footer", Description = "Footer Section",
         GroupName = Globals.GroupNames.Footer,
         Order = 7)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea Footer { get; set; }

        [Display(Name = "FooterSocial", Description = "FooterSocial Link Section",
        GroupName = Globals.GroupNames.Footer,
        Order = 8)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea FooterSocial { get; set; }

        [Display(Name = "FooterLinks", Description = "FooterLinks Section",
         GroupName = Globals.GroupNames.Footer,
         Order = 9)]
        [CultureSpecific]
        public virtual LinkItemCollection FooterLinks { get; set; }

        [Display(Name = "FooterLegal", Description = "FooterLegal Section",
         GroupName = Globals.GroupNames.Footer,
         Order = 10)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea FooterLegal { get; set; }
        [Display(GroupName = Globals.GroupNames.Footer, Order = 7, Description = "PRC Code", Name = "Page PRC Code")]
        public virtual String PRCCode { get; set; }
        [Display(Name = "Footer AFter PRC", Description = "Footer After PRC Section",
    GroupName = Globals.GroupNames.Footer,
    Order = 10)]
        [AllowedTypes(
   AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea FooterAfterPRC { get; set; }

        [Display(Name = "PopUp-Static ISI", Description = "PopUp Section or Static ISI",
         GroupName = Globals.GroupNames.ISI,
         Order = 11)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea PopUp { get; set; }

        [Display(Name = "ISISection", Description = "ISI Section",
         GroupName = Globals.GroupNames.ISI,
         Order = 12)]
        [AllowedTypes(
        AllowedTypes = new[] { typeof(EpiserverBH.Models.Blocks.RawHTML) })]

        public virtual ContentArea ISISection { get; set; }

        [Display(Name = "SiteLogotype", Description = "SiteLogotype", GroupName = Globals.GroupNames.Header,
         Order = 13)]
        public virtual Blocks.SiteLogotypeBlock SiteLogotype { get; set; }

    }
}