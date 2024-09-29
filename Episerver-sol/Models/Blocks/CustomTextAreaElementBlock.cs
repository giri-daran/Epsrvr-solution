using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(GUID = "{1DC0828B-4E8B-47FF-8031-FF6468BB6EF9}",
       DisplayName = "Custom TextArea Element",
       Description = "Custom Text Area Element",
       Order = 1001,
       GroupName = "Form Elements")]
    public class CustomTextAreaElementBlock : TextareaElementBlock
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        [Display(Name = "InputCSS", Order = 6, Description = "TextArea Class Name", GroupName = SystemTabNames.Content)]
        public virtual String InputCSS { get; set; }

        [Display(Name = "LabelCSS", Order = 5, Description = "Label Class Name", GroupName = SystemTabNames.Content)]
        public virtual String LabelCSS { get; set; }

        [Display(Name = "DivCSS", Order = 4, Description = "Div Class Name", GroupName = SystemTabNames.Content)]
        public virtual String DivCSS { get; set; }

    }
}