using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.MarketoForm
{    [ContentType(GUID = "B4EC29E8-08F8-4229-8AC3-F1B5252E009E", DisplayName = "Marketo Form  Block", Description = " CustomMarketoForm : Marketo Form  Block", Order = 105)]
    public class MarketoFormModuleBlock : SiteBlockData
    {
        [Display(Name = "MktForm ID", Order = 1, Description = "MktForm ID", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string MktFormID { get; set; }

        [Display(Name = "Redirect Url", Order = 2, Description = "Redirect Url", GroupName = SystemTabNames.Content)]
        
        public virtual string RedirectUrl { get; set; }

        [Display(Name = "Thank you Message", Order = 3, Description = "Thank you Message", GroupName = SystemTabNames.Content)]
        public virtual string ThankyouMessage { get; set; }        
    }
}
