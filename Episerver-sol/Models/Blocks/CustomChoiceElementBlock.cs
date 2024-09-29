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
    [ContentType(GUID = "{1040A7B9-DACF-45FF-86EC-51ECEA12A6D6}",
        DisplayName = "Custom Choice Element",
        Description = "Custom Choise Element",
        Order = 1006,
        GroupName = "Form Elements")]
    public class CustomChoiceElementBlock : ChoiceElementBlock
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