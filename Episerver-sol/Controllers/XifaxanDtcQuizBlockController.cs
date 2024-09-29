using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EpiserverBH.Infrastructure.Cms.Users;
using EpiserverBH.Models.Blocks.Xifaxan;
//using Soap;
using EpiserverBH.Models.Media;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EpiserverBH.Controllers.Xifaxan
{
    public class XifaxanDtcQuizBlockController : BlockController<XifaxanDtcQuizBlock>
    {
        private static XifaxanDtcQuizBlock _block;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public readonly ApplicationSignInManager<SiteUser> _SignInManager;



        public XifaxanDtcQuizBlockController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor iHttpContextAccessor, IConfiguration _configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _IHttpContextAccessor = iHttpContextAccessor;
        }
        public override ActionResult Index(XifaxanDtcQuizBlock currentContent)
        {
            _block = currentContent;
            return View("/Views/Shared/Blocks/XifaxanDtcQuizBlock.cshtml", _block);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Execute(DDGinfo datainfo)
        {
            bool isSuccess = false;
            string pdfFilepath = string.Empty;
            string pdfFileName = string.Empty;

            try
            {
                string method = _IHttpContextAccessor.HttpContext.Request.Method;
                string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];

                if (method == "POST")
                {
                    if (requestedWith == "XMLHttpRequest")
                    {
                        string pdfFilePath = string.Empty;
                        string pdfTemplatePath = string.Empty;

                        //pdfFilePath = Server.MapPath("~/Assets/Xifaxan-DTC/Template");
                        pdfFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Xifaxan");
                        //_IHttpContextAccessor.HttpContext.Request.GetDisplayUrl()+ "/siteassets/pdfs";

                        if (datainfo.strQy1 == "Yes")
                        {
                            pdfTemplatePath = Path.Combine(pdfFilePath, "ddga.pdf");
                        }
                        else
                        {
                            pdfTemplatePath = Path.Combine(pdfFilePath, "ddgb.pdf");
                        }

                        string currentDateTime = DateTime.Now.ToString("MMddyyy-HHmmss");

                        PdfReader reader = new PdfReader(pdfTemplatePath);

                        string directory = String.Empty;
                        directory = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", "Xifaxan");
                        //directory = directory == "" ? "~/Assets/Xifaxan-DTC/DoctorDiscussionGuide" : directory;
                        //directory = directory == "" ? "/pdfs/Xifaxan" : directory;

                        //bool exists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory));
                        //if (!exists)
                        //    System.IO.Directory.CreateDirectory(_hostingEnvironment .Current.Server.MapPath(directory));

                        pdfFilepath = System.IO.Path.Combine(directory, "cic-discussion-guide-" + currentDateTime + ".pdf");

                        using (PdfDocument document = new PdfDocument(reader, new PdfWriter(pdfFilepath)))
                        {
                            //reader.SelectPages("1-2");
                            //var pageSize = reader.GetPageSize(1);
                            //PdfContentByte pbover = stamper.GetOverContent(1);

                            var pdfFormFields = iText.Forms.PdfAcroForm.GetAcroForm(document, false);

                            var ans10 = pdfFormFields.GetField("Ans10");
                            var ans11 = pdfFormFields.GetField("Ans11");
                            var ans20 = pdfFormFields.GetField("Ans20");
                            var ans21 = pdfFormFields.GetField("Ans21");
                            var ans30 = pdfFormFields.GetField("Ans30");
                            var ans31 = pdfFormFields.GetField("Ans31");
                            var ans40 = pdfFormFields.GetField("Ans40");
                            var ans41 = pdfFormFields.GetField("Ans41");
                            var ans50 = pdfFormFields.GetField("Ans50");
                            var ans51 = pdfFormFields.GetField("Ans51");

                            if (datainfo.strQy1 == "Yes")
                            {
                                if (datainfo.strQy1 == "Yes")
                                {
                                    ans10.SetValue(datainfo.strQy1, false);
                                    ans11.SetValue("No", false);
                                }
                                else if (datainfo.strQy1 == "No")
                                {
                                    ans10.SetValue(datainfo.strQy1, false);
                                    ans11.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans10.SetValue("", false);
                                    ans11.SetValue("", false);
                                }

                                if (datainfo.strQy2 == "Yes")
                                {
                                    ans20.SetValue(datainfo.strQy2, false);
                                    ans21.SetValue("No", false);
                                }
                                else if (datainfo.strQy2 == "No")
                                {
                                    ans20.SetValue(datainfo.strQy2, false);
                                    ans21.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans20.SetValue("", false);
                                    ans21.SetValue("", false);
                                }

                                if (datainfo.strQy3 == "Yes")
                                {
                                    ans30.SetValue(datainfo.strQy3, false);
                                    ans31.SetValue("No", false);
                                }
                                else if (datainfo.strQy3 == "No")
                                {
                                    ans30.SetValue(datainfo.strQy3, false);
                                    ans31.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans30.SetValue("", false);
                                    ans31.SetValue("", false);
                                }

                                if (datainfo.strQy4 == "Yes")
                                {
                                    ans40.SetValue(datainfo.strQy4, false);
                                    ans41.SetValue("No", false);
                                }
                                else if (datainfo.strQy4 == "No")
                                {
                                    ans40.SetValue(datainfo.strQy4, false);
                                    ans41.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans40.SetValue("", false);
                                    ans41.SetValue("", false);
                                }

                                if (datainfo.strQy5 == "Yes")
                                {
                                    ans50.SetValue(datainfo.strQy5, false);
                                    ans51.SetValue("No", false);
                                }
                                else if (datainfo.strQy5 == "No")
                                {
                                    ans50.SetValue(datainfo.strQy5, false);
                                    ans51.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans50.SetValue("", false);
                                    ans51.SetValue("", false);
                                }
                            }
                            else if (datainfo.strQy1 == "No")
                            {
                                if (datainfo.strQy1 == "Yes")
                                {
                                    ans10.SetValue(datainfo.strQy1, false);
                                    ans11.SetValue("No", false);
                                }
                                else if (datainfo.strQy1 == "No")
                                {
                                    ans10.SetValue(datainfo.strQy1, false);
                                    ans11.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans10.SetValue("", false);
                                    ans11.SetValue("", false);
                                }

                                if (datainfo.strQn2 == "Yes")
                                {
                                    ans20.SetValue(datainfo.strQn2, false);
                                    ans21.SetValue("No", false);
                                }
                                else if(datainfo.strQn2 == "No")
                                {
                                    ans20.SetValue(datainfo.strQn2, false);
                                    ans21.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans20.SetValue("", false);
                                    ans21.SetValue("", false);
                                }

                                if (datainfo.strQn3 == "Yes")
                                {
                                    ans30.SetValue(datainfo.strQn3, false);
                                    ans31.SetValue("No", false);
                                }
                                else if(datainfo.strQn3 == "No")
                                {
                                    ans30.SetValue(datainfo.strQn3, false);
                                    ans31.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans30.SetValue("", false);
                                    ans31.SetValue("", false);
                                }

                                if (datainfo.strQn4 == "Yes")
                                {
                                    ans40.SetValue(datainfo.strQn4, false);
                                    ans41.SetValue("No", false);
                                }
                                else if(datainfo.strQn4 == "No")
                                {
                                    ans40.SetValue(datainfo.strQn4, false);
                                    ans41.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans40.SetValue("", false);
                                    ans41.SetValue("", false);
                                }

                                if (datainfo.strQn5 == "Yes")
                                {
                                    ans50.SetValue(datainfo.strQn5, false);
                                    ans51.SetValue("No", false);
                                }
                                else if(datainfo.strQn5 == "No")
                                {
                                    ans50.SetValue(datainfo.strQn5, false);
                                    ans51.SetValue("Yes", false);
                                }
                                else
                                {
                                    ans50.SetValue("", false);
                                    ans51.SetValue("", false);
                                }
                            }

                            pdfFormFields.FlattenFields();
                            document.Close();
                        }

                        isSuccess = true;
                        pdfFileName = "cic-discussion-guide-" + currentDateTime + ".pdf";

                        UploadPdfinCms(pdfFilepath);

                        System.IO.File.Delete(pdfFilepath);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { IsSuccess = isSuccess, PdfFileName = pdfFileName, Errormessage = ex.Message }),
                    ContentType = "application/json",
                };
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { IsSuccess = isSuccess, PdfFileName = pdfFileName }),
                ContentType = "application/json",
            };
        }

        public void UploadPdfinCms(string filepath)
        {
            try
            {
                var imageExtension = ".pdf";

                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

                var rootFolders = contentRepository.GetChildren<ContentFolder>(SiteDefinition.Current.SiteAssetsRoot);
                foreach (var rootFolder in rootFolders)
                {
                    if (rootFolder.Name == "Quiz-PDF")
                    {
                        var newFile = contentRepository.GetDefault<GenericMedia>(rootFolder.ContentLink);
                        var blobFactory = ServiceLocator.Current.GetInstance<IBlobFactory>();

                        using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            var blob = blobFactory.CreateBlob(newFile.BinaryDataContainer, imageExtension);

                            BinaryReader br = new BinaryReader(fs);
                            Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));

                            using (var s = blob.OpenWrite())
                            {
                                var w = new StreamWriter(s);
                                w.Write(fs);
                                w.Flush();
                            }

                            blob.WriteAllBytes(dataBytes);
                            newFile.BinaryData = blob;
                            newFile.Name = System.IO.Path.GetFileName(filepath);
                            contentRepository.Save(newFile, SaveAction.Publish, EPiServer.Security.AccessLevel.Read);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }
    }

    public class DDGinfo
    {
        public string strQy1 { get; set; }
        public string strQy2 { get; set; }
        public string strQy3 { get; set; }
        public string strQy4 { get; set; }
        public string strQy5 { get; set; }
        public string strQn1 { get; set; }
        public string strQn2 { get; set; }
        public string strQn3 { get; set; }
        public string strQn4 { get; set; }
        public string strQn5 { get; set; }
        public string filepath { get; set; }
    }

}
