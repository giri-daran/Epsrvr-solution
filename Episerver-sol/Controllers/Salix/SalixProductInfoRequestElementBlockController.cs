using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.Salix;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace EpiserverBH.Controllers.Salix
{
    public class SalixProductInfoRequestElementBlockController : Controller
    {
        private static SalixProductInfoRequestElementBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private IConfiguration Configuration;
        // GET: BauschHealth
        public SalixProductInfoRequestElementBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
            Configuration = _configuration;
        }
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm(SalixProductInfoRequestInfo info)
        {
            bool isSuccess = false;

            info.PortalID = 253;
            info.FormID = 16;
            info.FormName = "Salix_RequestSamplesandClinicalInformation";

            isSuccess = SaveSalixProductInfo(info);

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { IsSuccess = isSuccess }),
                ContentType = "application/json",
            };          
        }

        private bool SaveSalixProductInfo(SalixProductInfoRequestInfo info)
        {
            bool isSaved = false;
            
            string ConStringnew = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(ConStringnew))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "usp_SalixRequestSave";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PortalID", info.PortalID);
                cmd.Parameters.AddWithValue("@FormID", info.FormID);
                cmd.Parameters.AddWithValue("@FormName", info.FormName ?? "");
                cmd.Parameters.AddWithValue("@FirstName", info.FirstName ?? "");
                cmd.Parameters.AddWithValue("@LastName", info.LastName ?? "");
                cmd.Parameters.AddWithValue("@Address1", info.Address1 ?? "");
                cmd.Parameters.AddWithValue("@Address2", info.Address2);
                cmd.Parameters.AddWithValue("@City", info.City ?? "");
                cmd.Parameters.AddWithValue("@State", info.State);
                cmd.Parameters.AddWithValue("@Zip", info.Zip);
                cmd.Parameters.AddWithValue("@Email", info.Email ?? "");
                cmd.Parameters.AddWithValue("@Daytimephone", info.Daytimephone ?? "");
                cmd.Parameters.AddWithValue("@Iama", info.Iama ?? "");
                cmd.Parameters.AddWithValue("@ChkCertify", info.IsChkCertify);
                cmd.Parameters.AddWithValue("@ChkInterested", info.IsChkInterested);
                cmd.Parameters.AddWithValue("@ProfessionSpecialty", info.ProfessionSpecialty ?? "");
                cmd.Parameters.AddWithValue("@Credentials", info.Credentials ?? "");
                cmd.Parameters.AddWithValue("@InstitutionPractice", info.InstitutionPractice ?? "");

                connection.Open();

                cmd.ExecuteNonQuery();

                isSaved = true;
            }

            return isSaved;
        }

    }


}
