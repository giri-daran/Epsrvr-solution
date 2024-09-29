using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks
{ 
    [ContentType(GUID = "{49ACDB2B-9752-4169-A729-29B134A1F115}",
        DisplayName = "ActionForm Custom TextBox Element",
        Description = "ActionForm Custom TextBox Element",
        Order = 1000,
        GroupName = "Form Elements")]
    [SiteImageUrl("/assets/Global/img/textboxelementblock.png")]
    public class AFCustomTextboxElementBlock : TextboxElementBlock
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        [Display(Name = "InputCSS", Order = 7, Description = "InputCSS", GroupName = SystemTabNames.Content)]
        public virtual String InputCSS { get; set; }
        [Display(Name = "Input Div CSS", Order = 8, Description = "Input Div CSS", GroupName = SystemTabNames.Content)]
        public virtual String InputDivCSS { get; set; }

        [Display(Name = "LabelCSS", Order = 5, Description = "LabelCSS", GroupName = SystemTabNames.Content)]
        public virtual String LabelCSS { get; set; }
        [Display(Name = "Label Div CSS", Order = 6, Description = "Label Div CSS", GroupName = SystemTabNames.Content)]
        public virtual String LabelDivCSS { get; set; }

        [Display(Name = "Control DivCSS", Order = 4, Description = "Control DivCSS", GroupName = SystemTabNames.Content)]
        public virtual String DivCSS { get; set; }

    }
}