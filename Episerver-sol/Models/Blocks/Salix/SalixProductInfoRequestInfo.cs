using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.Salix
{
    public class SalixProductInfoRequestInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Clinicalinformation { get; set; }
        public string Iama { get; set; }
        public string ProfessionSpecialty { get; set; }
        public string Credentials { get; set; }
        public string AdditionalCredentials { get; set; }
        public string Daytimephone { get; set; }
        public string InstitutionPractice { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ChkInterested { get; set; }
        public string ChkCertify { get; set; }
        public string requestforclinicleinformation { get; set; }
       
        public bool IsChkCertify
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ChkCertify))
                {
                    return (ChkCertify.Trim().ToUpper() == "Y");
                }
                return false;
            }
        }

        public bool IsChkInterested
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ChkInterested))
                {
                    return (ChkInterested.Trim().ToUpper() == "Y");
                }
                return false;
            }
        }
       
        public int PortalID { get; set; }
        public int FormID { get; set; }
        public string FormName { get; set; }
    }
}