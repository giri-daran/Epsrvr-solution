using Microsoft.AspNetCore.Mvc.Razor;

namespace EpiserverBH.Business.Rendering
{
    public class SiteViewEngineLocationExpander : IViewLocationExpander
    {
        private static readonly string[] AdditionalPartialViewFormats = new[]
        {
        TemplateCoordinator.BlockFolder + "{0}.cshtml",
        TemplateCoordinator.PagePartialsFolder + "{0}.cshtml",
              TemplateCoordinator.BauschBlockPageFolder + ".cshtml"
    };
       


        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            

            
            foreach (var location in viewLocations)
            {
                // yield return location
                // .Replace("{feature}", featureName);
                yield return location;
                    
                    //.Replace("~/Views/Bausch/BLDefaultPage/Components/", "~/Views/Bausch/");
            }
            for (var i = 0; i < AdditionalPartialViewFormats.Length; i++)
            {
                yield return AdditionalPartialViewFormats[i];
                
            }

            yield return ("/Views/Bausch/" + context.ViewName + "/Index.cshtml").Replace("/Components","").Replace("/Default", "");
            //yield return ("/Views/Shared/Blocks/" + context.ViewName + ".cshtml").Replace("/Components", "").Replace("/Default", "");

        }

        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}