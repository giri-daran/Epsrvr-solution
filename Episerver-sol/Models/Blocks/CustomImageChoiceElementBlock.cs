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
    [ContentType(GUID = "{9DDD1DC0-D409-4CC1-A6C4-20CF60991B15}",
        DisplayName = "Custom Image Choice Element",
        Description = "Custom Image Choise Element",
        Order = 1005,
        GroupName = "Form Elements")]
    public class CustomImageChoiceElementBlock : ImageChoiceElementBlock
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