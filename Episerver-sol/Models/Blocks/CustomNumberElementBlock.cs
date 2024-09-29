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
    [ContentType(GUID = "{AD5BCDC3-0223-46E2-8F33-0A0F2C00A52E}",
        DisplayName = "Custom Number Element",
        Description = "Custom Number Element",
        Order = 1004,
        GroupName = "Form Elements")]
    public class CustomNumberElementBlock : NumberElementBlock
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