using EPiServer.Core;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using EpiserverBH.Models.Pages.Arestinprofessional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.ViewModels.ArestinProfessional
{
    public class ArestinForgotPasswordViewModel
    {


        [Required(ErrorMessage = "Email is Required. It cannot be empty")] 
        public  string Email { get; set; }

        public string ReturnUrl { get; set; }
        public string ResetButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string UsernameText { get; set; }
        public string CurrentBlockID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }


    }
}
