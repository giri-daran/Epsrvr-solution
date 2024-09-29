using EPiServer.Personalization;
using EPiServer.Web.Mvc;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using EpiserverBH.Models.ViewModels.ArestinProfessional;
using Newtonsoft.Json;
using EPiServer.Framework.Localization;
using EPiServer.Find.Cms.Statistics;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc;
using EPiServer.Cms.UI.AspNetIdentity;
using EpiserverBH.Infrastructure.Cms.Users;
using EPiServer.Find;
using System.Web;
using Verndale.RedirectManager.Data;
using EpiserverBH.Controllers.BauschHealth;
using System.Net.Mail;
using System.Net;
using EpiserverBH.Helpers;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinForgotPasswordBlockController : BlockComponent<ArestinForgotPasswordBlock>
    {
        private static ArestinForgotPasswordBlock _block;
        private readonly LocalizationService _localizationService;

        private IConfiguration Configuration;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private static IServiceProvider serviceProvider;
        private readonly ApplicationUserManager<SiteUser> _userManager;

        public ArestinForgotPasswordBlockController(LocalizationService localizationService, IHttpContextAccessor _iHttpContextAccessor, IServiceProvider _iserviceProvider, ApplicationUserManager<SiteUser> userManager, IConfiguration _configuration)
        {
            _localizationService = localizationService;
            _IHttpContextAccessor = _iHttpContextAccessor;
            serviceProvider = _iserviceProvider;
            _userManager = userManager;
            Configuration = _configuration;
        }

        protected override IViewComponentResult InvokeComponent(ArestinForgotPasswordBlock currentBlock)
        {

            var model = new ArestinForgotPasswordViewModel
            {
                ReturnUrl = Convert.ToString(currentBlock.ReturnURL),
                ResetButtonText = currentBlock.ResetButtonText,
                CancelButtonText = currentBlock.CancelButtonText,
                UsernameText = currentBlock.UsernameText,

                CurrentBlockID = Convert.ToString(currentBlock.ReturnURL),
            };



            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ArestinForgotPasswordViewModel forpasswordblockmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = "";
                    SiteUser user = null;
                    try
                    {
                        user = await _userManager.FindByEmailAsync(forpasswordblockmodel.Email);
                        //username = user.Username;
                    }
                    catch
                    {
                        ModelState.AddModelError("ForgotPasswordBlockModel.Email", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "User doesn't exist in our system, please contact site Administrator"));
                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                success = true,
                                errors = ModelState.Keys
                                    .SelectMany(k => ModelState[k].Errors)
                                    .Select(m => m.ErrorMessage).ToArray()
                            }),
                            ContentType = "application/json",
                        };
                    }

                    if (username != null)
                    {
                        string To = username, UserID, Password, SMTPPort, Host;
                        if(user.IsLockedOut)
                        {
                            user.IsLockedOut = false;
                            var status = await _userManager.UpdateAsync(user);
                        }

                        //var Submitresult = objUC.GeneratePasswordResetToken(token, username);
                        var Submitresult = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var hostname = _IHttpContextAccessor.HttpContext.Request.Host; //Request.Url.AbsoluteUri.Replace(Request.Url.LocalPath, "");

                        if (Submitresult != null)
                        {
                            string protocol = "http://";
                            if (_IHttpContextAccessor.HttpContext.Request.IsHttps)
                                protocol = "https://";

                            var ResetLink = protocol + hostname + "/resetpassword?ctl=PasswordReset&token=" + HttpUtility.UrlEncode(Submitresult);

                            //HTML Template for Send email

                            string FirstName = Convert.ToString(user.FirstName);
                            string LastName = Convert.ToString(user.LastName);

                            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

                            var contentLink = forpasswordblockmodel.CurrentBlockID;
                            ContentReference currentblockRef;
                            ContentReference.TryParse(contentLink, out currentblockRef);
                            BlockData data = repository.Get<BlockData>(currentblockRef);

                            string subject = Convert.ToString(data["EmailSubject"]);
                            string body = Convert.ToString(data["EmailBody"]).Replace("{{Username}}", username).Replace("{{FirstName}}", FirstName).Replace("{{LastName}}", LastName).Replace("{{Email}}", forpasswordblockmodel.Email).Replace("{{Token}}", HttpUtility.UrlEncode(Submitresult)).Replace("{{ResetLink}}", ResetLink);

                            // MailService.AppSettings(out UserID, out Password, out SMTPPort, out Host);
                            // MailService.SendEmail("no-reply@valeantonline.com", subject, body, forpasswordblockmodel.Email, UserID, Password, SMTPPort, Host);
                            bool result = sendmail(body, subject, forpasswordblockmodel.Email);
                            return new ContentResult
                            {
                                Content = JsonConvert.SerializeObject(new
                                {
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
                            ModelState.AddModelError("ForgotPasswordBlockModel.Email", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "User doesn't exist in our system"));
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
                        ModelState.AddModelError("ForgotPasswordBlockModel.Email", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "User doesn't exist in our system, please contact site Administrator"));
                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                success = true,
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
                    ModelState.AddModelError("ForgotPasswordBlockModel.Email", _localizationService.GetString("/Login/Form/Error/WrongPasswordOrEmail", "User doesn't exist in our system"));
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

            return (IActionResult)View();
        }
        public bool sendmail(string body, string subject, string toAddress )
        {
            try
            {
                string hosts = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Host");
                int port = Configuration.GetValue<int>("EPiServer:CMS:Smtp:Network:Port");
                //string fromAddress = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:NotificationEmailAddress");
                string userName = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:UserName");
                string password = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Password");

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = hosts;
                smtpClient.Port = Convert.ToInt16(port);
                smtpClient.Credentials = new NetworkCredential(userName, password);
                smtpClient.EnableSsl = true;


                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("no-reply@valeantonline.com");
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(toAddress);
                    smtpClient.Send(mailMessage);
                }
                return true;
            }
            catch(Exception ex)
            {
                //MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver CMS12 - ArestinProfessional ForgotPassword SendMail", ex.StackTrace, "websitealerts@bauschhealth.com");
                return false;
            }
        }

    }

}