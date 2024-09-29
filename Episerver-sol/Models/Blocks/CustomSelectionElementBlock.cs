using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(GUID = "{3118C678-C0E0-4DB1-99FF-5416ED84BE30}",
        DisplayName = "Custom Selection Element",
        Description = "Custom Selection Element",
        Order = 1002,
        GroupName = "Form Elements")]
    public class CustomSelectionElementBlock : SelectionElementBlock
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        [Display(Name = "InputCSS", Order = 6, Description = "InputCSS", GroupName = SystemTabNames.Content)]
        public virtual String InputCSS { get; set; }

        [Display(Name = "LabelCSS", Order = 5, Description = "LabelCSS", GroupName = SystemTabNames.Content)]
        public virtual String LabelCSS { get; set; }

        [Display(Name = "DivCSS", Order = 4, Description = "DivCSS", GroupName = SystemTabNames.Content)]
        public virtual String DivCSS { get; set; }

    }
}