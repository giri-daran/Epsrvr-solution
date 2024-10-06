using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.HKLocationBlock
{
    [ContentType(GUID = "804202BF-3850-4581-A937-2FC9F3117C9C", DisplayName = "HK Location Block", Description = "HK Location Block", Order = 105)]
    public class HKLocation : SiteBlockData
    {
        [Display(Name = "Template Name", Order = 1, Description = "Template Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TemplateName { get; set; }
        
        [Display(Name = "Table Name", Order = 3, Description = "Table Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TableName { get; set; }

    }
}