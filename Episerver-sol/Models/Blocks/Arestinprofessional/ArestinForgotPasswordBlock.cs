using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Arestinprofessional
{
    [ContentType(DisplayName = "Arestin Forgot Password", GUID = "417BD5F3-4A63-4F52-AFC8-3D049183019B", Description = "Arestin Forgot Password")]
    public class ArestinForgotPasswordBlock : BlockData
    {
        [Display(Name = "Enter the Forgot Password Template Name", Description = "Enter the Forgot Password Template Name", Order = 1, ShortName = "Enter the Forgot Password Template Name")]

        public virtual string TemplateName { get; set; }

        [Display(Name = "Forgot Password Return URL", Description = "Forgot Password Return URL", Order = 1, ShortName = "Forgot Password Return URL")]

        public virtual ContentReference ReturnURL { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            TemplateName = "ArestinProfessional";
        }


        [Display(Name = "Enter Label Name for Username", Description = "Enter Label Name for Username", Order = 1, ShortName = "Enter Label Name for Username")]
        [Required]
        public virtual string UsernameText { get; set; }


        [Display(Name = "Enter Label Name for Reset Button Text", Description = "Enter Label Name for Reset Button Text", Order = 1, ShortName = "Enter Label Name for Reset Button Text")]
        [Required]
        public virtual string ResetButtonText { get; set; }

        [Display(Name = "Enter Label Name for Cancel Button Text", Description = "Enter Label Name for Cancel Button Text", Order = 1, ShortName = "Enter Label Name for Cancel Button Text")]
        [Required]
        public virtual string CancelButtonText { get; set; }

        [Display(Name = "Enter the Email FROM Address", Description = "Enter the Email FROM Address", Order = 1, ShortName = "Enter the Email FROM Address", GroupName = "Email Configuration")]
        [Required]
        public virtual string EmailFromAddress { get; set; }

        [Display(Name = "Enter the Email Subject", Description = "Enter the Email Subject", Order = 1, ShortName = "Enter the Email Subject", GroupName = "Email Configuration")]
        [Required]
        public virtual string EmailSubject { get; set; }

        [Display(Name = "Enter the Body Content", Description = "Enter the Body Content. Ex Use {{Username}}, {{Token}},{{FirstName}},{{LastName}}", Order = 1, ShortName = "Enter the Body Content", GroupName = "Email Configuration")]
        [Required]
        public virtual XhtmlString EmailBody { get; set; }


    }

}

