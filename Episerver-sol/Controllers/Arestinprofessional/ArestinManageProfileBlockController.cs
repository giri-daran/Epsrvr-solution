using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Shell.Profile;
using EPiServer.Web.Mvc;
using EpiserverBH.Business;
using EpiserverBH.Infrastructure.Cms.Users;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinManageProfileBlockController : BlockComponent<ArestinManageProfileBlock>
    {
        private static ArestinManageProfileBlock _block;
        private static IHttpContextAccessor _IHttpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager<SiteUser> _userManager;
        private readonly IProfileRepository _profileRepository;

        public ArestinManageProfileBlockController(IHttpContextAccessor _iHttpContextAccessor, IConfiguration _iConfiguration, ApplicationUserManager<SiteUser> userManager, IProfileRepository profileRepo)
        {
            _IHttpContextAccessor = _iHttpContextAccessor;
            _configuration = _iConfiguration;
            _userManager = userManager;
            _profileRepository = profileRepo;
        }

        protected override IViewComponentResult InvokeComponent(ArestinManageProfileBlock currentContent)
        {
            _block = currentContent;

            return View(currentContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProfileUpdate(Request request)
        {

            bool isSuccess = false;
            string errMsg = "";

            string RoleTypeStudent = _configuration.GetValue<string>("CustomConfiguration:ArestinStudent");
            string RoleTypeFaculty = _configuration.GetValue<string>("CustomConfiguration:ArestinFaculty");
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    Dictionary<string, string> profiledata = new Dictionary<string, string>();
                    profiledata.Add("FirstName", request.FirstName ?? "");
                    profiledata.Add("LastName", request.LastName ?? "");
                    profiledata.Add("Profession", request.Profession ?? "");
                    profiledata.Add("SchoolName", request.SchoolName ?? "");
                    profiledata.Add("SecondaryEmail", request.SecondaryEmailAddress ?? "");

                    foreach (var p in profiledata)
                    {
                        var EpiUser = _profileRepository.GetOrCreateProfile(request.EmailAddress);
                        EpiUser.Settings[p.Key] = p.Value;
                        _profileRepository.Save(EpiUser);
                    }

                    //var user = Membership.GetUser(request.EmailAddress, false);
                    //isSuccess = user.ChangePassword(user.ResetPassword(), request.Password);

                    var user = await _userManager.FindByEmailAsync(request.EmailAddress);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    //var token = _IHttpContextAccessor.HttpContext.Request.Query["token"];
                   
                    var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
                    isSuccess = result.Succeeded;

                    if (isSuccess)
                    {
                        errMsg = "Success";

                        _IHttpContextAccessor.HttpContext.Session.SetString("FirstName", request.FirstName);
                        _IHttpContextAccessor.HttpContext.Session.SetString("LastName", request.LastName);
                        _IHttpContextAccessor.HttpContext.Session.SetString("SchoolName", request.SchoolName);
                        _IHttpContextAccessor.HttpContext.Session.SetString("Profession", request.Profession);
                        _IHttpContextAccessor.HttpContext.Session.SetString("SEmail", request.SecondaryEmailAddress ?? "");
                    }
                    else
                    {
                        if (result.Errors.Any())
                        {
                            string errormsg = result.Errors.AsEnumerable().FirstOrDefault().Description;

                            return new ContentResult
                            {
                                Content = JsonConvert.SerializeObject(new
                                {
                                    IsSuccess = isSuccess,
                                    Message = errormsg,
                                }),

                                ContentType = "application/json",
                            };
                        }
                    }
                }
            }

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess,
                    Message = errMsg
                }),

                ContentType = "application/json",
            };
        }

    }


    //Need to move - ArestinRegProgforstudentsBlockController
    public class Request
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchoolName { get; set; }
        public string GraduationMonth { get; set; }
        public string GraduationYear { get; set; }
        public string EmailAddress { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string HowEducationalContent { get; set; }
        public string WhereEducationalContent { get; set; }
        public string AfterPractice { get; set; }

        //public MembershipCreateStatus Status { get; set; }
        //public MembershipUser user { get; set; }

        public string EnrollSchoolName { get; set; }
        public string ProgramDirectorFirstName { get; set; }
        public string ProgramDirectorLastName { get; set; }
        public string ProgramDirectorEmail { get; set; }
        public string NumberofTrialKits { get; set; }
        public string ProgramFacultyContactName { get; set; }
        public string ProgramFacultyContactEmail { get; set; }
        public string ProgramDentistName { get; set; }
        public string ProgramDentistEmail { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string ProgramDentistNameShipping { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FaxNumber { get; set; }
        public string Terms { get; set; }
        public string NumberofAdditionalHandles { get; set; }


        public string Profession { get; set; }
        public string HowArestinSamples { get; set; }
        public string OnAverageArestinProgram { get; set; }
        public string HowSeniorInYourClass { get; set; }
        public string AreYouFullTimeFaculty { get; set; }
        public string AreYouOutsideInstitution { get; set; }

    }

}