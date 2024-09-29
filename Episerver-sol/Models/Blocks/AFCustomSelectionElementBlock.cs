using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(GUID = "CAC6F269-C1D5-4340-BDB0-2B3259A9B440", DisplayName = "ActionForm Custom Selection Element",
        Description = "ActionForm Custom Selection Element",
        Order = 1000,
        GroupName = "Form Elements")]
    [SiteImageUrl("/assets/Global/img/selectionelementblock.png")]
    public class AFCustomSelectionElementBlock : SelectionElementBlock
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