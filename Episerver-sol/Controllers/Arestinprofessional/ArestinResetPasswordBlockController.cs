using Advanced.CMS.ApprovalReviews;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework.Localization;
using EPiServer.Shell.Security;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EpiserverBH.Infrastructure.Cms.Users;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace EpiserverBH.Controllers.Arestinprofessional
{

    public class ArestinResetPasswordBlockController : BlockComponent<ArestinResetPasswordBlock>
    {
        private readonly LocalizationService _localizationService;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private static IServiceProvider serviceProvider;
        private readonly ApplicationUserManager<SiteUser> _userManager;
        private readonly ApplicationSignInManager<SiteUser> _signInManager;
        private readonly ApplicationRoleProvider<SiteUser> _roleManager;
        private readonly UIRoleProvider _roleProvider;

        public ArestinResetPasswordBlockController(UIRoleProvider roleProvider, LocalizationService localizationService, ApplicationRoleProvider<SiteUser> roleManager, ApplicationSignInManager<SiteUser> signInManager, IHttpContextAccessor _iHttpContextAccessor, IServiceProvider _iserviceProvider, ApplicationUserManager<SiteUser> userManager)
        {
            _localizationService = localizationService;
            _IHttpContextAccessor = _iHttpContextAccessor;
            serviceProvider = _iserviceProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleProvider = roleProvider;
        }


        protected override IViewComponentResult InvokeComponent(ArestinResetPasswordBlock currentBlock)
        {
            string token = _IHttpContextAccessor.HttpContext.Request.Query["token"];// Request.QueryString["token"];
            var model = new ArestinResetPasswordViewModel
            {
                ReturnUrl = Convert.ToString(currentBlock.ReturnURL),
                ChngPassButtonText = currentBlock.ChngPassButtonText,
                CancelButtonText = currentBlock.CancelButtonText,
                NewPasswordText = currentBlock.NewPasswordText,
                ConfirmPasswordText = currentBlock.ConfirmPasswordText,
                Token = token
            };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ArestinResetPasswordViewModel resetpasswordblockmodel)
        public async Task<ActionResult> ResetPassword(ArestinResetPasswordViewModel resetpasswordblockmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = "";
                    SiteUser user = null;
                    try
                    {
                        user = await _userManager.FindByEmailAsync(resetpasswordblockmodel.Username);
                        username = user.Username;
                    }
                    catch
                    {

                        ModelState.AddModelError("ResetPasswordBlockModel.Password", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "User Email invalid, please contact site Administrator to reset Password manually."));

                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                success = false,
                                errors = ModelState.Keys
                             .SelectMany(k => ModelState[k].Errors)
                             .Select(m => m.ErrorMessage).ToArray()
                            }),
                            ContentType = "application/json",
                        };
                    }

                    var returnUrl = "/educational-resources/arestin-student-certification-program";

                    var result = await _userManager.ResetPasswordAsync(user, resetpasswordblockmodel.Token, resetpasswordblockmodel.NewPassword);
                    //Autologin Code on successfull validation
                    bool loginresult = false;


                    if (result.Succeeded)
                    {
                        bool facultyuser = await _userManager.IsInRoleAsync(user, "Faculty User");
                        bool studentuser = await _userManager.IsInRoleAsync(user, "Student User");

                        if (facultyuser)
                        {
                            returnUrl = "Program-for-faculty";
                            _IHttpContextAccessor.HttpContext.Session.SetString("ProfileType", "Faculty");
                        }
                        else
                        {
                            returnUrl = "program-for-students";
                            _IHttpContextAccessor.HttpContext.Session.SetString("ProfileType", "Student");
                        }
                        loginresult = await _signInManager.SignInAsync(user.UserName, resetpasswordblockmodel.NewPassword, returnUrl);
                    }
                    if (loginresult)
                    {
                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                returnURL = returnUrl,
                                success = true,
                                errors = ModelState.Keys
                                   .SelectMany(k => ModelState[k].Errors)
                                   .Select(m => m.ErrorMessage).ToArray()
                            }),
                            ContentType = "application/json",
                        };
                    }
                    else
                    {
                        ModelState.AddModelError("ResetPasswordBlockModel.Password", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "Reset token Invalid, please regenerate teh Token."));

                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                success = false,
                                errors = ModelState.Keys
                             .SelectMany(k => ModelState[k].Errors)
                             .Select(m => m.ErrorMessage).ToArray()
                            }),
                            ContentType = "application/json",
                        };
                    }



                }
                else
                {
                    ModelState.AddModelError("ResetPasswordBlockModel.Password", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "Password reset failed. Please reenter your values and try again."));
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(new
                        {
                            success = false,
                            errors = ModelState.Keys
                             .SelectMany(k => ModelState[k].Errors)
                             .Select(m => m.ErrorMessage).ToArray()
                        }),
                        ContentType = "application/json",
                    };
                }

            }

            catch (Exception ex)
            {

                ModelState.AddModelError("ForgotPasswordBlockModel.Email", ex.Message);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new
                    {
                        success = false,
                        errors = ModelState.Keys
                             .SelectMany(k => ModelState[k].Errors)
                             .Select(m => m.ErrorMessage).ToArray()
                    }),
                    ContentType = "application/json",
                };
            }

        }

    }
}
