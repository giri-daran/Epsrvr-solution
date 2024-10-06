using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Pages.Patents
{
    [ContentType(DisplayName = "PatentHome", GUID = "894f1adf-5762-4303-812f-f60b042a7168", Description = "Patents Home Page", GroupName = "Development")]
    public class PatentHome : SitePageData
    {
        [Display(GroupName = SystemTabNames.Settings, Order = 1, Description = "Site Template", Name = "Site Template")]
        public virtual String SiteTemplate { get; set; }

        [Display(GroupName = SystemTabNames.PageHeader, Order = 2, Description = "Page CSS", Name = "Page CSS")]
        // [BackingType(typeof(PropertyStringList))]
        public virtual string[] SiteCSS { get; set; }


        [Display(GroupName = SystemTabNames.PageHeader, Order = 3, Description = "Page JS", Name = "Page JS")]
        //[BackingType(typeof(PropertyStringList))]
        public virtual string[] SiteJs { get; set; }

        [Display(GroupName = Globals.GroupNames.MetaData, Order = 310, Description = "PageSchema", Name = "PageSchema")]
        public virtual ContentArea PageSchema { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 6, Description = "Site Schema", Name = "Site Schema")]
        public virtual ContentArea SiteSchema { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 7, Description = "Google Tag Manager", Name = "Google Tag Manager")]
        public virtual String GoogleTagManager { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 8, Description = "Site Logo", Name ="Site Logo")]
        public virtual Blocks.SiteLogotypeBlock SiteLogotype { get; set; }

        [Display(Name = "Banner", Description = "Banner Section",
        GroupName = SystemTabNames.Content,
        Order = 311)]
        [CultureSpecific]
        public virtual ContentArea Banner { get; set; }

        [Display(Name = "MainBody", Description = "MainBody Section",
           GroupName = SystemTabNames.Content,
           Order = 313)]
        [CultureSpecific]
        public virtual ContentArea MainBody { get; set; }
        [Display(Name = "ContentAreaFullwidth", Description = "ContentAreaFullwidth Section",
                   GroupName = SystemTabNames.Content,
                   Order = 317)]
        public virtual ContentArea ContentAreaFullwidth { get; set; }


        [Display(Name = "Footer", Description = "Footer Section",
         GroupName = SystemTabNames.Content,
         Order = 319)]
        [CultureSpecific]
        public virtual ContentArea Footer { get; set; }
    }
}