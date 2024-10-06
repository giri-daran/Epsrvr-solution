using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.BHITSurvey

{
    [ContentType(GUID = "DA5C0D9B-2BD5-4D00-80B7-CF7FCE2817E6", DisplayName = "BHIT Survey Module Block", Description = "BHIT Survey Module Block", Order = 102)]
    public class BHITSurveyModuleBlock : SiteBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Order = 1, Description = "Question", Name = "Question")]
        [Required]
        public virtual string Question { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 2, Description = "Options", Name = "Options  (Please Enter Options seperated by '|' symbol)")]
        [Required]
        public virtual string Options { get; set; }

   
        [Display(GroupName = SystemTabNames.Content, Order = 3, Description = "Module ID", Name = "ModuleID")]
        [Required]
        public virtual int ModuleID { get; set; }


        [Ignore]
        public int SurveyID { get; set; }

    }
}