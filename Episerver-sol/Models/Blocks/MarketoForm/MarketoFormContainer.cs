using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.MarketoForm
{
    [ContentType(DisplayName = "MarketoFormContainer", GUID = "6d57c575-40d4-490f-99c5-a905255bffab", Description = "Marketo Epi Form Container")]
    public class MarketoFormContainer : FormContainerBlock
    {
        [Display(Name = "MarkettoForm ID", Order = 1, Description = "MarkettoForm ID", GroupName = SystemTabNames.Content)]
        [Required]
        public virtual string MktFormID { get; set; }
    }
}