using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpiserverBH.Models.Blocks
{
    /// <summary>
    /// Used to present contact information with a call-to-action link
    /// </summary>
    /// <remarks>Actual contact details are retrieved from a contact page specified using the ContactPageLink property</remarks>
    [SiteContentType(GUID = "7E932EAF-6BC2-4753-902A-8670EDC5F373")]
    
    public class FormPrintAndEmailBlock : SiteBlockData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [CultureSpecific]
        public virtual bool ShowPrintButton { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [CultureSpecific]
        public virtual bool ShowEmailButton { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [CultureSpecific]
        public virtual XhtmlString Message { get; set; }


    }
}