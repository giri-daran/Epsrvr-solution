using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace EpiserverBH.Models.Pages
{
    [ContentType(DisplayName = "HCP", GUID = "e0003e1d-9df4-4104-a58a-e69c27d6f24c", Description = "Bausch Health HCP Page Type", GroupName = "Transformation")]
    public class HCP : Home
    {
        [Display(GroupName = SystemTabNames.Settings, Order = 1, Description = "Site Template", Name = "Site Template")]
        public override String SiteTemplate { get; set; }



        [Display(GroupName = SystemTabNames.Settings, Order = 4, Description = "Google Tag Manager", Name = "Google Tag Manager")]
        public override String GoogleTagManager { get; set; }


        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public override Blocks.SiteLogotypeBlock SiteLogotype { get; set; }

        // public IEnumerable<EpiserverBH.Models.EasyDNNNew> News { get; set; }


    }
}