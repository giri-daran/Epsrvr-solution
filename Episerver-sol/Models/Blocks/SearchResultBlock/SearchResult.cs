using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.SearchResultBlock
{
    [ContentType(GUID = "ED4BD544-CB22-41F3-8FA9-0D4B6692E5FF", DisplayName = "Search Result Block", Description = "Search Result Block", Order = 105)]
    public class SearchResult : SiteBlockData
    {
        [Display(Name = "Template Name", Order = 1, Description = "Template Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TemplateName { get; set; }
        
        [Display(Name = "Table Name", Order = 3, Description = "Table Name", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string TableName { get; set; }

    }
}