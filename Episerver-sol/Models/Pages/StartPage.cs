using EPiServer.SpecializedProperties;
using EpiserverBH.Models.Blocks;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Pages
{
    ///<summary>
    /// Used for the site's start page and also acts as a container for site settings
    ///</summary>
    [ContentType(
        GUID = "19671657-B684-4D95-A61F-8DD4FE60D559",
        GroupName = Globals.GroupNames.Specialized)]
    [SiteImageUrl]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[]
        {
        typeof(ContainerPage),
        typeof(ProductPage),
        typeof(StandardPage),
        typeof(ISearchPage),
        typeof(LandingPage),
        typeof(ContentFolder) }, // Pages we can create under the start page...
        ExcludeOn = new[]
        {
        typeof(ContainerPage),
        typeof(ProductPage),
        typeof(StandardPage),
        typeof(ISearchPage),
        typeof(LandingPage)
        })] // ...and underneath those we can't create additional start pages
    public class StartPage : SitePageData
    {
        [Display(GroupName = SystemTabNames.Settings, Order = 1)]
        public virtual String SiteTemplate { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 2)]
        public virtual LinkItemCollection SiteCSS { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 3)]
        public virtual LinkItemCollection SiteJs { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [CultureSpecific]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 300)]
        public virtual LinkItemCollection ProductPageLinks { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 350)]
        public virtual LinkItemCollection CompanyInformationPageLinks { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 400)]
        public virtual LinkItemCollection NewsPageLinks { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings, Order = 450)]
        public virtual LinkItemCollection CustomerZonePageLinks { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual PageReference GlobalNewsPageLink { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual PageReference ContactsPageLink { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual PageReference SearchPageLink { get; set; }

        [Display(GroupName = Globals.GroupNames.SiteSettings)]
        public virtual SiteLogotypeBlock SiteLogotype { get; set; }
    }
}