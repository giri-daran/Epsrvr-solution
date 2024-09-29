using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(GUID = "4A17EC20-D24B-4B5B-949A-0A7C0FBD64DC", DisplayName = "Aplenzin : Sad Quiz Responses", Description = "Aplenzin : Sad Quiz Responses Element Block", Order = 105)]
    public class SADQuizModuleElementBlock : SiteBlockData
    {
        [Display(Name = "PI Template URL (With Pdf name)", Order = 1, Description = "PI Template URL Path (Full URL path with filename.pdf)", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string PITemplatePath { get; set; }
    }
}