using EPiServer.Personalization;
using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks.Arestinprofessional;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EpiserverBH.Controllers.Arestinprofessional
{
    public class ArestinEnrollSchoolBlockController : BlockComponent<ArestinEnrollSchoolBlock>
    {
        private static ArestinEnrollSchoolBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }
        public ArestinEnrollSchoolBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        protected override IViewComponentResult InvokeComponent(ArestinEnrollSchoolBlock currentContent)
        {
            _block = currentContent;

            return View(currentContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnrollMySchool(School request)
        {
            bool isSuccess = false;

            string DirectorName = request.ProgramDirectorFirstName + " " + request.ProgramDirectorLastName;
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];
            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    string ConStringnew = GetConnectionString();

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConStringnew))
                        {
                            using (SqlCommand cmd = new SqlCommand("SP_Insert_Arestin_StudentAccessProgram", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@FormTitle", "School Enrollment Form");
                                cmd.Parameters.AddWithValue("@ProgramFacultyContactName", request.ProgramFacultyContactName.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramFacultyContactEmail", request.ProgramFacultyContactEmail.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@PrimaryPhoneNumber", request.PrimaryPhoneNumber.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@NumberofTrialKits", request.NumberofTrialKits.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@Address", request.Address.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@Address2", request.Address2 == null ? "" : request.Address2.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@City", request.City.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@State", request.State.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ZipCode", request.ZipCode.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@FaxNumber", request.FaxNumber == null ? "" : request.FaxNumber.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@SchoolName", request.EnrollSchoolName.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramDirectorName", DirectorName.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramDirectorEmail", request.ProgramDirectorEmail.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramDentistName", request.ProgramDentistName.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramDentistEmail", request.ProgramDentistEmail.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@ProgramDentistNameShipping", request.ProgramDentistNameShipping == null ? "" : request.ProgramDentistNameShipping.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");
                                cmd.Parameters.AddWithValue("@NumberofAdditionalHandles", request.NumberofAdditionalHandles == null ? "" : request.NumberofAdditionalHandles.Replace("\n", "").Replace("\r", "").Replace("'", "").Replace("|", "") ?? "");

                                connection.Open();
                                cmd.ExecuteNonQuery();
                                connection.Close();

                                isSuccess = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MailService.SendAlert(Configuration, "no-reply@valeantonline.com", "Episerver CMS12 - Arestin Professional: Enroll your school Block", ex.StackTrace, "websitealerts@bauschhealth.com");                       
                        isSuccess = false;
                    }
                }
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = isSuccess
                }),
                ContentType = "application/json",
            };           
        }

        public class School
        {

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
            public string NumberofAdditionalHandles { get; set; }

        }
    }

}