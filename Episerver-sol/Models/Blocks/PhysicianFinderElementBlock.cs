using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(GUID = "8F10E2EA-1CC9-4CBD-9620-7E504D5383FD", DisplayName = "Physician Finder Element Block", Description = "Physician Finder Element Block", Order = 104)]
    public class PhysicianFinderElementBlock : SiteBlockData
    {
        [Display(Name = "Template Name", Order = 1, Description = "Template Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TemplateName { get; set; }

        [Display(Name = "Module ID", Order = 2, Description = "Module ID", GroupName = SystemTabNames.Content)]
        public virtual string ModuleID { get; set; }
        
        [Display(Name = "Table Name", Order = 3, Description = "Table Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TableName { get; set; }

        [Display(Name = "Goolge Map API Key", Order = 4, Description = "Goolge Map API Key", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string MapApiKey { get; set; }


        [Display(Name = "Goolge Geo Code Key", Order = 5, Description = "Goolge Geo Code Key", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string MapGeoCodeKey { get; set; }
    }
}