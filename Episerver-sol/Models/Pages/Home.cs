using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace EpiserverBH.Models.Pages
{
    //[TemplateDescriptor(Default = true)]
    [ContentType(DisplayName = "Home", GUID = "3a2a9f69-4aca-4dcc-9a97-49403590e4d0", Description = "Bausch Health Home Content Type", GroupName = "Transformation")]
    public class Home : SitePageData
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


        //[Display(GroupName = SystemTabNames.Settings, Order = 1)]
        //public virtual string SiteLayout { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual Blocks.SiteLogotypeBlock SiteLogotype { get; set; }

        // public IEnumerable<EpiserverBH.Models.EasyDNNNew> News { get; set; }
    }
}