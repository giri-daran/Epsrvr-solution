using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Arestinprofessional
{
    [ContentType(DisplayName = "Arestin Reset Password Block", GUID = "A8CE9151-38B6-436C-9FCC-C0C4A64A9EBD", Description = "Arestin Reset Password Block")]
    public class ArestinResetPasswordBlock : BlockData
    {

        [Display(Name = "Enter the Reset Password Template Name", Description = "Enter the Reset Password Template Name", Order = 1, ShortName = "Enter the Reset Password Template Name")]
        //[Required]
        public virtual string TemplateName { get; set; }

        [Display(Name = "Reset Password Return URL", Description = "Reset Password Return URL", Order = 2, ShortName = "Reset Password Return URL")]
       // [Required]
        public virtual ContentReference ReturnURL { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            TemplateName = "ArestinProfessional"; NewPasswordText = "New Password"; ConfirmPasswordText = "Confirm Password"; ChngPassButtonText = "Change Paasword"; CancelButtonText = "Cancel";
        }


        [Display(Name = "Enter Label Name for New Password", Description = "Enter Label Name for New Password", Order = 1, ShortName = "Enter Label Name for New Password")]
        [Required]
        public virtual string NewPasswordText { get; set; }

        [Display(Name = "Enter Label Name for Confirm Password", Description = "Enter Label Name for Confirm Password", Order = 1, ShortName = "Enter Label Name for Confirm Password")]
        [Required]
        public virtual string ConfirmPasswordText { get; set; }

        [Display(Name = "Enter Label Name for Change Password Button Text", Description = "Enter Label Name for Button Text", Order = 1, ShortName = "Enter Label Name for Change Password Button Text")]
        [Required]
        public virtual string ChngPassButtonText { get; set; }

        [Display(Name = "Enter Label Name for Cancel Button Text", Description = "Enter Label Name for Cancel Button Text", Order = 1, ShortName = "Enter Label Name for Cancel Button Text")]
        [Required]
        public virtual string CancelButtonText { get; set; }




    }

}