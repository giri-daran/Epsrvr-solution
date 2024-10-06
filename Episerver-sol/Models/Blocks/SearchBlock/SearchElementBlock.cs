using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.SearchBlock
{
    [ContentType(GUID = "528B045E-51EC-43C0-8385-5D56C876996D", DisplayName = "Search Element Block", Description = "Search Element Block", Order = 105)]
    public class SearchElementBlock : SiteBlockData
    {
        [Display(Name = "Template Name", Order = 1, Description = "Template Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TemplateName { get; set; }
        
        [Display(Name = "Table Name", Order = 3, Description = "Table Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TableName { get; set; }

    }
}