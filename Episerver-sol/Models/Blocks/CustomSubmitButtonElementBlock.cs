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

    [ContentType(GUID = "{5012200E-1BF6-4532-A30E-3C40CCF0FED8}",
        DisplayName = "Custom SubmitButton Element",
        Description = "Custom SubmitButton Element",
        Order = 1005,
        GroupName = "Form Elements")]
    public class CustomSubmitButtonElementBlock : SubmitButtonElementBlock 
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        [Display(Name = "InputCSS", Order = 5, Description = "InputCSS", GroupName = SystemTabNames.Content)]
        public virtual String InputCSS { get; set; }


        [Display(Name = "DivCSS", Order = 4, Description = "DivCSS", GroupName = SystemTabNames.Content)]
        public virtual String DivCSS { get; set; }
       
    }
}