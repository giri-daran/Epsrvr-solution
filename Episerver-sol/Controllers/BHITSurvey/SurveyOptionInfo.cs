
namespace EpiserverBH.Controllers.BHITSurvey
{
    public class SurveyOptionInfo
    {
        public int SurveyOptionId
        {
            get; set;
        }

        public int SurveyId
        {
            get; set;
        }

        public int ViewOrder
        {
            get; set;
        }

        public string OptionName
        {
            get; set;
        }

        public int Votes
        {
            get; set;
        }

        public bool IsCorrect
        {
            get; set;
        }

        public int VotePercentage { get; set; }

    }

    public class SurveyResponse
    {
        public SurveyResponse()
        {
            Options = new List<SurveyOptionInfo>();
        }
        public string Question { get; set; }

        public List<SurveyOptionInfo> Options { get; set; }
    }
}