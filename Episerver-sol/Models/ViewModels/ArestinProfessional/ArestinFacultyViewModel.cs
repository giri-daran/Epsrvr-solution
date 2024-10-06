using EPiServer.Core;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using EpiserverBH.Models.Pages.Arestinprofessional;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.ViewModels.ArestinProfessional
{
    public class ArestinFacultyViewModel
    {

        [Required(ErrorMessage = "Username is Required. It cannot be empty")]
        public string Username { get; set; }

        public ContentReference ResetPasswordPage { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }


        public bool RememberMe { get; set; }


        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string ForgotPasswordText { get; set; }
        public string LoginButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string RememberMeText { get; set; }
        public string UserType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public string EmailAddress { get; set; }
        public string SchoolName { get; set; }

    }
}