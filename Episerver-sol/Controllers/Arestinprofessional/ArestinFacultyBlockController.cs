using EPiServer;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Profile;
using EPiServer.Shell.Security;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EpiserverBH.Business;
using EpiserverBH.Business.Identity;
using EpiserverBH.Helpers;
using EpiserverBH.Infrastructure.Cms.Users;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Web;


namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinFacultyBlockController : BlockComponent<ArestinFacultyBlock>                  //BaseLoginBlockController<ArestinFacultyBlock>
    {
        private const string SUCCESS_KEY = "FormPosted";
        private const string FAILURE_KEY = "Loginfailed";
        private readonly LocalizationService _localizationService;
        private static IHttpContextAccessor _IHttpContextAccessor;
        private readonly ApplicationSignInManager<SiteUser> _signInManager;
        private readonly ApplicationUserManager<SiteUser> _userManager;
        private readonly IProfileRepository _profileRepository;
        private readonly IConfiguration Configuration;

        public ArestinFacultyBlockController(LocalizationService localizationService, IHttpContextAccessor _iHttpContextAccessor, ApplicationSignInManager<SiteUser> signInManager, ApplicationUserManager<SiteUser> userManager, IProfileRepository profileRepo, IConfiguration _configuration)
        {
            _localizationService = localizationService;
            _IHttpContextAccessor = _iHttpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _profileRepository = profileRepo;
            Configuration = _configuration;
        }
        protected override IViewComponentResult InvokeComponent(ArestinFacultyBlock currentBlock)
        {
            var model = new ArestinFacultyViewModel
            {
                ReturnUrl = Convert.ToString(currentBlock.ReturnURL),
                LoginButtonText = currentBlock.LoginButtonText,
                CancelButtonText = currentBlock.CancelButtonText,
                ForgotPasswordText = currentBlock.ForgotPasswordText,
                PasswordText = currentBlock.PasswordText,
                UsernameText = currentBlock.UsernameText,
            };

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Submit(ArestinFacultyViewModel loginmodel, ArestinFacultyBlock loginBlock, PageData pageData)
        {
            var returnurl = UrlResolver.Current.GetUrl(loginmodel.ReturnUrl);
            var isValid = false;
            //var isValid = Membership.Provider.ValidateUser(loginmodel.Username, loginmodel.Password);
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = HttpContext.Request.Headers["X-Requested-With"];
            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    if (isValid)
                    {
                        //FormsAuthentication.SetAuthCookie(loginmodel.Username, true);
                        return new RedirectResult(returnurl); //Important to redirect after login to be sure cookies etc are set.
                    }
                    else
                    {

                        ModelState.AddModelError("", "Wrong credentials, try again");
                        ContentReference postedBlock;

                        return new RedirectResult(returnurl); //Important to redirect after login to be sure cookies etc are set.
                    }

                }
            }

            return new RedirectResult(returnurl);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InternalLogin(ArestinFacultyViewModel viewModel)
        {
            //var returnUrl = GetSafeReturnUrl(Request.GetTypedHeaders().Referer);

            var returnUrl = "/program-for-faculty";

            if (!ModelState.IsValid)
            {
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

            var profile = _profileRepository.GetOrCreateProfile(viewModel.Username);
            var nam = await _userManager.FindByNameAsync(viewModel.Username);
            try
            {
                if (profile != null && profile.Settings.Count <= 1)
                {
                    Dictionary<string, string> profiledata = GetProfileDetails(nam.Id);

                    foreach (var p in profiledata)
                    {
                        profile.Settings[p.Key] = p.Value;
                        _profileRepository.Save(profile);
                    }
                }
            }catch(Exception ex)
            { }

            //var password = await _userManager.CheckPasswordAsync(nam, viewModel.Password);

            var isValid = await _signInManager.SignInAsync(viewModel.Username, viewModel.Password, returnUrl);

            var ErrorMsg = "You have entered wrong username or password";
            var getrole = await _userManager.GetRolesAsync(nam);

            if (!getrole.Contains("Faculty User"))
            {
                if (getrole.Contains("ArestinProfessional_Users"))
                {
                    if (getrole.Contains("Student User"))
                    {
                        ErrorMsg = "Please Check the User Role";
                        isValid = false;
                    }
                    else
                    {
                        _IHttpContextAccessor.HttpContext.Session.SetString("ProfileType", "Faculty");
                    }
                }
                else
                {
                    ErrorMsg = "Please Check the User Role";
                    isValid = false;
                }
            }
            else
            {
                _IHttpContextAccessor.HttpContext.Session.SetString("ProfileType", "Faculty");
            }

            if (isValid)
            {
                var EpiUser = _profileRepository.GetOrCreateProfile(nam.UserName);
                if (EpiUser.Settings.Count > 1)
                {
                    try
                    {
                        _IHttpContextAccessor.HttpContext.Session.SetString("Email", nam.UserName);

                        if (EpiUser.Settings.ContainsKey("Profession"))
                        {
                            string lcProf = Convert.ToString(EpiUser.Settings["Profession"]);
                            _IHttpContextAccessor.HttpContext.Session.SetString("Profession", lcProf);
                        }

                        if (EpiUser.Settings.ContainsKey("SchoolName"))
                        {
                            string lcSchName = Convert.ToString(EpiUser.Settings["SchoolName"]);
                            _IHttpContextAccessor.HttpContext.Session.SetString("SchoolName", lcSchName);
                        }

                        if (EpiUser.Settings.ContainsKey("SecondaryEmail"))
                        {
                            string lcSEmail = Convert.ToString(EpiUser.Settings["SecondaryEmail"]);
                            _IHttpContextAccessor.HttpContext.Session.SetString("SEmail", lcSEmail);
                        }

                        if (EpiUser.Settings.ContainsKey("FirstName"))
                        {
                            string lcFName = Convert.ToString(EpiUser.Settings["FirstName"]);
                            _IHttpContextAccessor.HttpContext.Session.SetString("FirstName", lcFName);
                        }

                        if (EpiUser.Settings.ContainsKey("LastName"))
                        {
                            string lcLName = Convert.ToString(EpiUser.Settings["LastName"]);
                            _IHttpContextAccessor.HttpContext.Session.SetString("LastName", lcLName);
                        }

                        //_IHttpContextAccessor.HttpContext.Session.SetString("FirstName", nam.FirstName);
                        //_IHttpContextAccessor.HttpContext.Session.SetString("LastName", nam.LastName);

                        //FormsAuthentication.SetAuthCookie(viewModel.Username, viewModel.RememberMe);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    //_IHttpContextAccessor.HttpContext.Session.SetString("FirstName", nam.FirstName);
                    //_IHttpContextAccessor.HttpContext.Session.SetString("LastName", nam.LastName);
                    _IHttpContextAccessor.HttpContext.Session.SetString("Email", nam.UserName);

                }
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new
                    {
                        success = true,
                        returnURL = returnUrl,
                        errors = ModelState.Keys
                       .SelectMany(k => ModelState[k].Errors)
                       .Select(m => m.ErrorMessage).ToArray()
                    }),
                    ContentType = "application/json",
                };

            }

            ModelState.AddModelError("LoginViewModel.Password", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", ErrorMsg));

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

        /// <summary>
        /// Get the Return Url form Querystring URl
        /// </summary>
        /// <param name="referrer"></param>
        /// <returns> returns redirect url </returns>
        private string GetSafeReturnUrl(Uri referrer)
        {
            //Make sure we only return to relative url.
            var returnUrl = HttpUtility.ParseQueryString(referrer.Query)["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
            {
                return "/";
            }

            if (Uri.TryCreate(returnUrl, UriKind.Absolute, out var uri))
            {
                return uri.PathAndQuery;
            }
            return returnUrl;
        }

        /// <summary>
        /// Getting Faculty Profile Details for Existing Users from EPiServerDB
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns> Dictionary<string, string> profiledata </returns>
        private dynamic GetProfileDetails(string UserId)
        {
            Dictionary<string, string> profiledata = new Dictionary<string, string>();

            try
            {
                string ConStringnew = ConfigurationExtensions.GetConnectionString(Configuration, "EPiServerDB");

                DataTable dataTable = new DataTable();
                SqlConnection conn = new SqlConnection(ConStringnew);

                string _SQLCommand = string.Format("select PropertyNames, PropertyValueStrings from Profiles where UserId = '{0}'", UserId);
                SqlCommand cmd = new SqlCommand(_SQLCommand, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();

                string lcPropNames = Convert.ToString(dataTable.Rows[0][0]);
                string lcPropVal = Convert.ToString(dataTable.Rows[0][1]);

                string[] values = lcPropNames.Split(':');

                for (int i = 0; i < values.Length - 2; i++)
                {
                    string str1 = values[i].ToString();
                    string str2 = lcPropVal.Substring(Convert.ToInt32(values[i + 1]), Convert.ToInt32(values[i + 2]));

                    profiledata.Add(str1, str2);
                    i += 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return profiledata;
        }
    }
}
