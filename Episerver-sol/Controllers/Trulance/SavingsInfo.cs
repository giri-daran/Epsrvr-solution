using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverBH.Controllers.Trulance
{
    public class SavingsInfo
    {
        public enum SalixProduct
        {
            Apriso, XifaxanHe, XifaxanIBSD, MoviPrep, RelistorDTC
        }

        public class Token
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }

        public class demographic
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public string dateOfBirth { get; set; }
            public string email { get; set; }
            public string mobilephone { get; set; }
            public string homephone { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
            public string memberNumber { get; set; }
            public string country { get; set; }
            public int personId { get; set; }
            public int communicationTypes { get; set; }
            public int QuestionId { get; set; }
            public int AnswerId { get; set; }
            public string AnswerText { get; set; }
            public bool Survey { get; set; }
            public string ProgramId { get; set; }
            public string pdfFolderPath { get; set; }
            public string pdfTemplateName { get; set; }
        }

        public class TrialCardResult
        {
            public string success { get; set; }
            public string[] messages { get; set; }
            //  public string[] accumulators { get; set; }

            public Dictionary<string, List<TrialCardInfo>> data { get; set; }
        }
        
        public class TrialCardActivateResult
        {
            public string success { get; set; }
            public string[] messages { get; set; }
        }
        
        public class TrialCardInfo
        {
            public string bin { get; set; }
            public string cardActivationDate { get; set; }
            public string cardEndDate { get; set; }
            public string sponsorNumber { get; set; }
            public string memberNumber { get; set; }
            public string groupNumber { get; set; }
            public double totalBenefitAmount { get; set; }
            public double remainingBenefitAmount { get; set; }
            public double totalPaidAmount { get; set; }
            public string[] accumulators { get; set; }

        }

        public class EmailSavingsCardResult
        {
            public string success { get; set; }
            public string[] messages { get; set; }
            public EmailSavingsCardInfo data { get; set; }
        }

        public class EmailSavingsCardInfo
        {
            public string bin { get; set; }
            public string cardActivationDate { get; set; }
            public string cardEndDate { get; set; }
            public string sponsorNumber { get; set; }
            public string memberNumber { get; set; }
            public string groupNumber { get; set; }
            public double totalBenefitAmount { get; set; }
            public double remainingBenefitAmount { get; set; }
            public double totalPaidAmount { get; set; }
        }

        public class TrulanceInfo
        {
            public string AgeCondition { get; set; }
            public bool IsAgeCondition
            {
                get
                {
                    if (!string.IsNullOrWhiteSpace(AgeCondition))
                    {
                        return (AgeCondition.Trim().ToUpper() == "Y");
                    }
                    return false;
                }
            }
            public string SubscribedToMarketing { get; set; }
            //public int CardID { get; set; }
            public string EmailAddress { get; set; }
            public string Checkboxagree { get; set; }
            public string LeadID { get; set; }
            public string hdnPDFFolderPath { get; set; }
            public string hdnPDFTemplateName { get; set; }
            public string hdnProgramId { get; set; }            
            public string CardID { get; set; }            
            public string SiteType { get; set; }
            public int PortalID { get; set; }
            public int FormID { get; set; }
            public string FormName { get; set; }
            public string SavingCardAction { get; set; }
            public string hdnSurvey { get; set; }
            
        }

        public class TrulanceCardInfo
        {
            public string EmailAddress { get; set; }
            public string Checkboxagree { get; set; }
            public string CardID { get; set; }
        }

    }
}