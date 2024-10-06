using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace EpiserverBH.Models.Pages.Xifaxan
{
    [ContentType(DisplayName = "IBSD-HCP", GUID = "B6A92C77-9B58-4951-A9AA-69094DC6B756", Description = "Xifaxan IBSD HCP Page Type", GroupName = "Xifaxan")]
    public class IBSDHCP : Home
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