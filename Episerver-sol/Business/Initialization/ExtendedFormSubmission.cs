using EPiServer.Forms.Core.Internal;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.Forms.Implementation.Elements;



namespace EpiserverBH.Business.Initialization
{
    public class ExtendedFormSubmission : DataSubmissionService// CampaignDataSubmissionService
    {
        private readonly PlaceHolderService _placeHolderService;
        private static IHttpContextAccessor _httpContextAccessor;
        public ExtendedFormSubmission(PlaceHolderService placeHolderService, IHttpContextAccessor IHttpContextAccessor)
        {
            _placeHolderService = placeHolderService ?? throw new ArgumentNullException("placeHolderService");
            _httpContextAccessor = IHttpContextAccessor;
        }

        protected override SubmitActionResult BuildReturnResultForSubmitAction(bool isJavaScriptSupport, bool isSuccess,
       string message, HttpContext httpContext, FormContainerBlock formContainer = null,
       Dictionary<string, object> additionalParams = null, SubmissionInfo submissionInfo = null,
       Submission submission = null, bool isProgressiveSubmit = false, string redirectUrl = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
#pragma warning disable Epi1002 // Avoid using Internal namespaces
                var placeHolders = _placeHolderService.GetAllAvailablePlaceHolders(formContainer, httpContext.Request, submission,
                    true, false);
#pragma warning restore Epi1002 // Avoid using Internal namespaces

#pragma warning disable Epi1002 // Avoid using Internal namespaces
                message = _placeHolderService.Replace(message, placeHolders, true);
#pragma warning restore Epi1002 // Avoid using Internal namespaces
            }

#pragma warning disable Epi1002 // Avoid using Internal namespaces
            return base.BuildReturnResultForSubmitAction(isJavaScriptSupport, isSuccess, message, httpContext,
                formContainer, additionalParams, submissionInfo, submission, isProgressiveSubmit, redirectUrl);
#pragma warning restore Epi1002 // Avoid using Internal namespaces
        }

    }
}