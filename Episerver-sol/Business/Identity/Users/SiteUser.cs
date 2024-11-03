using EPiServer.Cms.UI.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace EpiserverBH.Infrastructure.Cms.Users
{
    public class SiteUser : ApplicationUser
    {
        [NotMapped] public string FirstName { get; set; }

        [NotMapped] public string LastName { get; set; }
        [NotMapped] public string Password { get; set; }


        [NotMapped] public string SchoolName { get; set; }
        [NotMapped] public string GraduationMonth { get; set; }
        [NotMapped] public string GraduationYear { get; set; }
        [NotMapped] public string EmailAddress { get; set; }
        [NotMapped] public string SecondaryEmailAddress { get; set; }


        [NotMapped] public string Profession { get; set; }

        [NotMapped] public string RetypePassword { get; set; }
        [NotMapped] public string HowArestinSamples { get; set; }
        [NotMapped] public string OnAverageArestinProgram { get; set; }
        [NotMapped] public string HowSeniorInYourClass { get; set; }
        [NotMapped] public string AreYouFullTimeFaculty { get; set; }
        [NotMapped] public string AreYouOutsideInstitution { get; set; }


        [NotMapped] public string HowEducationalContent { get; set; }
        [NotMapped] public string WhereEducationalContent { get; set; }
        [NotMapped] public string AfterPractice { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(IUserClaimsPrincipalFactory<SiteUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateAsync(this);
            var claimsIdentity = ((ClaimsIdentity)userIdentity.Identity);
            // Add custom user claims here
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, Email));

            if (!string.IsNullOrEmpty(FirstName))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, FirstName));
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, LastName));
            }

            return claimsIdentity;
        }
    }
}