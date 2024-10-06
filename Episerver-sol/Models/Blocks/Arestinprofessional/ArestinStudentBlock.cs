using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Arestinprofessional
{
    [ContentType(DisplayName = "ArestinStudentBlock", GUID = "9F2F65BD-C2C8-4B5C-A79C-D090C24B22E3", Description = "ArestinStudentBlock")]
    public class ArestinStudentBlock : BlockData
    {

        //[Display(Name = "Title", Description = "Title of the Login Form", Order = 1)]

        // public virtual string Title { get; set; }

        [Display(Name = "Login Template Name", Description = "Enter the Login Template Name", Order = 1, ShortName = "Enter the Template Name")]
        //[Required]
        public virtual string TemplateName { get; set; }

        [Display(Name = "Enter Label Name for Username", Description = "Enter Label Name for Username", Order = 2, ShortName = "Enter Label Name for Username")]
        [Required]
        public virtual string UsernameText { get; set; }
        [Display(Name = "Enter Label Name for Password", Description = "Enter Label Name for Password", Order = 3, ShortName = "Enter Label Name for Password")]
        [Required]
        public virtual string PasswordText { get; set; }

        [Display(Name = "Enter Label Name for Login Button Text", Description = "Enter Label Name for Login Button Text", Order = 4, ShortName = "Enter Label Name for Login Button Text")]
        [Required]
        public virtual string LoginButtonText { get; set; }

        [Display(Name = "Enter Label Name for Cancel Button Text", Description = "Enter Label Name for Cancel Button Text", Order = 4, ShortName = "Enter Label Name for Cancel Button Text")]
        [Required]
        public virtual string CancelButtonText { get; set; }


        [Display(Name = "Enter Label Name for Forgot Password", Description = "Enter Label Name for Forgot Password", Order = 5, ShortName = "Enter Label Name for Forgot Password")]
        [Required]
        public virtual string ForgotPasswordText { get; set; }

        [Display(Name = "Login Return URL", Description = "Login Return URL", Order = 6, ShortName = "Select Login Return URL")]
        [Required]
        public virtual ContentReference ReturnURL { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            TemplateName = "ArestinProfessional";
        }

    }

}