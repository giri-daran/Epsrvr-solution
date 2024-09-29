using EPiServer.Core;
using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using System.Data;
using System.Data.SqlClient;

namespace EpiserverBH.Controllers.BHITSurvey
{
    public class SurveyDAL
    {
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }

        public Tuple<SurveyResponse, int> GetSurveyOptions(int SurveyID, int ModuleId, string Question, string Options, bool isOnlyResponse = false)
        {
            SurveyResponse surveyResponse = new SurveyResponse();

            var ConString = GetConnectionString();
            bool isNewSurveyQuestion = false;

            surveyResponse.Question = Question;

            if (!isOnlyResponse)
            {
                using (var con = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "usp_GetSurveyID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.Parameters.AddWithValue("@Options", Options);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleId);

                    var newQuestionParameter = cmd.CreateParameter();
                    newQuestionParameter.ParameterName = "@NewQuestion";
                    newQuestionParameter.SqlDbType = SqlDbType.Bit;
                    newQuestionParameter.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(newQuestionParameter);

                    con.Open();

                    SurveyID = Convert.ToInt32(cmd.ExecuteScalar());

                    isNewSurveyQuestion = Convert.ToBoolean(cmd.Parameters["@NewQuestion"].Value);

                    //Insert only if the Answer and Questine is new
                    if (isNewSurveyQuestion)
                    {
                        var optionList = Options.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                        {
                            sqlConn.Open();
                            int i = 0;
                            foreach (var option in optionList)
                            {
                                using (SqlCommand cmd1 = new SqlCommand("AddSurveyOption", sqlConn))
                                {

                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add("@SurveyID", SqlDbType.Int).Value = SurveyID;
                                    cmd1.Parameters.Add("@OptionName", SqlDbType.NVarChar, 500).Value = option;
                                    cmd1.Parameters.Add("@ViewOrder", SqlDbType.Int).Value = i;
                                    cmd1.Parameters.Add("@IsCorrect ", SqlDbType.Bit).Value = 0;

                                    cmd1.ExecuteNonQuery();

                                }

                                i++;
                            }
                        }

                    }
                }
            }


            using (SqlConnection sqlConn = new SqlConnection(ConString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand("GetSurveyOptions", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SurveyID", SqlDbType.Int).Value = Convert.ToInt32(SurveyID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SurveyOptionInfo colSurveyOptionInfo = new SurveyOptionInfo();

                            colSurveyOptionInfo.SurveyOptionId = Convert.ToInt32(dr["SurveyOptionID"]);
                            colSurveyOptionInfo.OptionName = Convert.ToString(dr["OptionName"]);
                            colSurveyOptionInfo.IsCorrect = Convert.ToBoolean(dr["IsCorrect"]);
                            colSurveyOptionInfo.Votes = Convert.ToInt32(dr["Votes"]);
                            colSurveyOptionInfo.ViewOrder = Convert.ToInt32(dr["ViewOrder"]);

                            surveyResponse.Options.Add(colSurveyOptionInfo);
                        }
                    }
                }

                //Update the vote percentage
                var totalVotes = surveyResponse.Options.Sum(t => t.Votes);
                if (totalVotes != 0)
                {
                    foreach (var option in surveyResponse.Options)
                    {
                        if (option.Votes != 0)
                        {
                            option.VotePercentage = Convert.ToInt32(((float)option.Votes / (float)totalVotes) * 100);
                        }
                        
                    }

                }
            }

            return new Tuple<SurveyResponse, int>(surveyResponse, SurveyID);
        }

        public void AddSurveyResult(SurveyResultInfo surveyResultInfo)
        {
            if (surveyResultInfo != null)
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand("AddSurveyResult", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@SurveyOptionID", SqlDbType.Int).Value = surveyResultInfo.SurveyOptionId;
                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = surveyResultInfo.UserId;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }



        #region Utility Functions
        public static int ConvertNullInteger(object Field)
        {
            if (Field == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(Field);
            }
        }
        #endregion
    }
}
