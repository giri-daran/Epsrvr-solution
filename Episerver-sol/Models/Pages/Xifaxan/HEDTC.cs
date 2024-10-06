using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace EpiserverBH.Models.Pages.Xifaxan
{
    [ContentType(DisplayName = "HE-DTC", GUID = "E14625FC-E4F0-41CF-BB87-CC2B4FBCA6B2", Description = "Xifaxan HE DTC Page Type", GroupName = "Xifaxan")]
    public class HEDTC : Home
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