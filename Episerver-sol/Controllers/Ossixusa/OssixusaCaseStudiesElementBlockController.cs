using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.Ossixusa;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;


namespace EpiserverBH.Controllers.Ossixusa
{
    public class OssixusaCaseStudiesElementBlockController : BlockComponent<OssixusaCaseStudiesElementBlock>
    {
        private static OssixusaCaseStudiesElementBlock _block;

        protected override IViewComponentResult InvokeComponent(OssixusaCaseStudiesElementBlock currentBlock)
        {
            _block = currentBlock;
            //return base.InvokeComponent(currentBlock);
            return View("/Views/Shared/Blocks/OssixusaCaseStudiesElementBlock.cshtml", _block);
            //OssixusaCaseStudiesElementBlock
        }

        [HttpPost]
         [ValidateAntiForgeryToken]
        public ActionResult BuildContent(int ArtID)
        {
            bool IsSuccess = false;
            string Title = string.Empty;
            string SubTitle = string.Empty;
            string Article = string.Empty;
            string lcArticle = string.Empty;
            string lcPara = string.Empty;

            string ConStringnew = SQLGetConnection.GetConnection().ConnectionString;



            using (SqlConnection connection = new SqlConnection(ConStringnew))
            {

                connection.Open();

                using (SqlCommand cmd = new SqlCommand("select Title, SubTitle, Article from EasyDNNNews WHERE PortalID = 257 and ArticleID = " + ArtID, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Title = String.Format("{0}", reader["Title"]);
                                Title = Title.Replace("CASE STUDY: ", "CASE STUDY:&nbsp;");
                                SubTitle = String.Format("{0}", reader["SubTitle"]);
                                lcArticle = String.Format("{0}", reader["Article"]);

                                lcArticle = HttpUtility.HtmlDecode(lcArticle);
                                Article = lcArticle.Replace("portals/257", "siteassets/Ossix");
                            }

                            IsSuccess = true;
                        }
                    }
                }
            }

            switch (ArtID)
            {
                case 601:
                    lcPara = "Case study photographs courtesy of Dr. Rodrigo Neiva, Philadelphia, PA.";
                    break;
                case 600:
                    lcPara = "Case study photographs courtesy of Paul S. Rosen, DMD, Yardley, PA.";
                    break;
                case 599:
                    lcPara = "Case study photographs courtesy of Gustavo Avila-Ortiz, DDS, MS, PhD, Iowa City, IA.";
                    break;
                case 598:
                    lcPara = "Case study photographs courtesy of Bradley A. Ross, DMD.";
                    break;
                case 602:
                    lcPara = "Case study photographs courtesy of Dr. Barry Levin, Jenkintown, PA; restoration by Dr. J Rhode.";
                    break;
                case 603:
                    lcPara = "Case study photographs courtesy of Dr. Yuval Zubery, Ramat Hasharon, Israel.";
                    break;
                case 604:
                    lcPara = "Case study photographs courtesy of Dr. Barry Levin, Jenkintown, PA; restoration by Dr. Raj Shah, Norristown, PA.";
                    break;
                case 605:
                    lcPara = "Case study photographs courtesy of Dr. Barry Levin, Jenkintown, PA; restoration by Dr. D. Frank.";
                    break;
                case 606:
                    lcPara = "Case study photographs courtesy of Dr. Barry Levin, Jenkintown, PA; restoration by Dr. D. Frank.";
                    break;
                case 607:
                    lcPara = "Case study photographs courtesy of Dr. Rodrigo Neiva, Philadelphia, PA.";
                    break;
                case 608:
                    lcPara = "Dr. Yuval Zubery, Ramat Hasharon, Israel.";
                    break;
                case 609:
                    lcPara = "Dr. Barry Levin, Jenkintown, PA; restoration by Dr. Gregg Rothstein, Richboro, PA.";
                    break;
                case 610:
                    lcPara = "Dr. Scott Froum, New York, NY.";
                    break;
                default:
                    lcPara = "";
                    break;
            }


            //return Json(new { IsSuccess = IsSuccess, Title = Title, SubTitle = SubTitle, Article = Article, Para = lcPara }, JsonRequestBehavior.AllowGet);

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    IsSuccess = IsSuccess,
                    Title = Title,
                    SubTitle = SubTitle,
                    Article = Article,
                    Para = lcPara
                }),

                ContentType = "application/json",
            };
        }
    }
}