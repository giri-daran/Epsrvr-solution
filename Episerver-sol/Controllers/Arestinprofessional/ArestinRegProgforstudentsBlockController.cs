using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Shell.Profile;
using EPiServer.Web.Mvc;
using EpiserverBH.Infrastructure.Cms.Users;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinRegProgforStudentsElementBlockController : BlockComponent<ArestinRegProgforStudentsElementBlock>
    {
        private static ArestinRegProgforStudentsElementBlock _block;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly ApplicationUserManager<SiteUser> _userManager;
        private readonly ApplicationSignInManager<SiteUser> _signInManager;

        private readonly IProfileRepository _profileRepository;
        public ArestinRegProgforStudentsElementBlockController(IProfileRepository profileRepository, ApplicationSignInManager<SiteUser> signInManager, IHttpContextAccessor _iHttpContextAccessor, ApplicationUserManager<SiteUser> userManager)
        {
            _IHttpContextAccessor = _iHttpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _profileRepository = profileRepository;
        }
        protected override IViewComponentResult InvokeComponent(ArestinRegProgforStudentsElementBlock currentContent)
        {
            _block = currentContent;
            return View(currentContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentRegister(SiteUser user)
        {
            bool isSuccess = false;
            string errMsg = "";
            bool addNewUser = false;
            string RoleType = "Student User";
            string RoleTypeRoot = "ArestinProfessional_Users";
            user.Email = user.EmailAddress;
            user.Username = user.EmailAddress;
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {

                    var userExist = await _userManager.FindByEmailAsync(user.Email);

                    if (userExist != null)
                    {
                        errMsg = "A User Already Exists For the Email Address Specified. Please Register Using A Different Email Address";
                        isSuccess = false;
                    }
                    else
                    {
                        addNewUser = true;
                        isSuccess = true;
                        user.IsApproved = true;
                    }


                    if (addNewUser)
                    {
                        // Create membership
                        var result = new IdentityResult();
                        result = await _userManager.CreateAsync(user, user.Password);

                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            var role1 = await _userManager.AddToRoleAsync(user, RoleType);
                            var role2 = await _userManager.AddToRoleAsync(user, RoleTypeRoot);


                            // Save profile data

                            Dictionary<string, string> profiledata = new Dictionary<string, string>();
                            profiledata.Add("Profession", user.Profession ?? "");
                            profiledata.Add("SecondaryEmail", user.SecondaryEmailAddress ?? "");
                            profiledata.Add("SchoolName", user.SchoolName ?? "");
                            profiledata.Add("FirstName", user.FirstName ?? "");
                            profiledata.Add("LastName", user.LastName ?? "");
                            profiledata.Add("Email", user.EmailAddress ?? "");

                            profiledata.Add("GraduationMonth", user.GraduationMonth ?? "");
                            profiledata.Add("GraduationYear", user.GraduationYear ?? "");
                            profiledata.Add("HowEducationalContent", user.HowEducationalContent ?? "");
                            profiledata.Add("WhereEducationalContent", user.WhereEducationalContent ?? "");
                            profiledata.Add("AfterPractice", user.AfterPractice ?? "");

                            foreach (var p in profiledata)
                            {
                                var profile = _profileRepository.GetOrCreateProfile(user.EmailAddress);
                                profile.Settings[p.Key] = p.Value;
                                _profileRepository.Save(profile);
                            }

                            //Login the user
                            _IHttpContextAccessor.HttpContext.Session.SetString("FirstName", user.FirstName ?? "");
                            _IHttpContextAccessor.HttpContext.Session.SetString("LastName", user.LastName ?? "");
                            _IHttpContextAccessor.HttpContext.Session.SetString("ProfileType", "Student");
                            _IHttpContextAccessor.HttpContext.Session.SetString("Profession", user.Profession ?? "");
                            _IHttpContextAccessor.HttpContext.Session.SetString("Email", user.EmailAddress);
                            _IHttpContextAccessor.HttpContext.Session.SetString("SEmail", user.SecondaryEmailAddress ?? "");
                            _IHttpContextAccessor.HttpContext.Session.SetString("SchoolName", user.SchoolName ?? "");
                            isSuccess = true;
                        }
                    }
                }
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    ErrorMsg = errMsg
                }),
                ContentType = "application/json",
            };
        }      
    }


}