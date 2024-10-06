using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.Bauschhealthpap
{
    [ContentType(DisplayName = "PAPFormContainer", GUID = "EB9A52EF-9B32-4CB9-8A13-3CCB352562D4", Description = "Custom PAP Form Container")]
    public class PAPFormContainer : FormContainerBlock
    {
        [Display(Name = "PDF Template URL (With Pdf name)", Order = 1, Description = "PDF Template URL Path (Path with filename.pdf)", GroupName = SystemTabNames.Content)]

        [Required]

        public virtual string PAPPDFTemplatePath { get; set; }
    }
}