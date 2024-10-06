using EPiServer.Shell.ObjectEditing;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using System.ComponentModel.DataAnnotations;



namespace EpiserverBH.Models.Blocks.HKAccordion
{
    [ContentType(DisplayName = "HKAccordionFAQ-Block", GUID = "8E347D7E-65A0-4C71-8AA4-61B36025761C", Description = "This Block is used Accordion Content Blocks", GroupName = Globals.GroupNames.HK)]
    //[SiteImageUrl("/assets/Bauschsurgical/img/gfx/layout.png")]
    public class HKAccordionBlock : BlockData
    {
        [Display(Name = "Accordion Content List", GroupName = "Information", Description = "Heading", Order = 1)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<HKAccordionList>))]
        public virtual IList<HKAccordionList> AccordionList { get; set; }

       

    }
}