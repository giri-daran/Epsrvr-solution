using EPiServer.Core;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.ViewModels.ArestinProfessional
{
    public class ArestinResetPasswordViewModel
    {
        
        public string Token { get; set; }
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        // [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The New Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string NewPasswordText { get; set; }
        public string ConfirmPasswordText { get; set; }        
        public string ChngPassButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string ReturnUrl { get; set; }


    }
}