using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;


namespace EpiserverBH.Models.Blocks
{
    [ContentType(DisplayName = "GenericAccordionBlock", GUID = "19576A78-732A-4E5C-B418-1DBA7D5272A3", Description = "This Block is used to add Accordion", GroupName = EpiserverBH.Globals.GroupNames.GenericAccordionBlock)]
    [SiteImageUrl("/assets/Bauschsurgical/img/gfx/layout.png")]
    public class GenericAccordionBlock : BlockData
    {
        [Display(Name = "Data & Id name")]
        [CultureSpecific]
        public virtual string DataIdName { get; set; }

        [Display(Name = "Bootstrap Version")]
        [SelectOne(SelectionFactoryType = typeof(BootstrapClass))]
        public virtual string BootstrapVersion { get; set; }

        [Display(Name = "parentid")]
        [CultureSpecific]
        public virtual string ParentId { get; set; }

        [Display(Name = "Headerid (Only for Bootstrap 5)")]
        [CultureSpecific]
        public virtual string HeaderId { get; set; }

        [CultureSpecific]
        [UIHint("AccordionContentArea")]
        public virtual ContentArea AccordionBlocks { get; set; }


        
    }

    public class BootstrapClass : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                  new SelectItem() { Text = "Select Bootstrap Version", Value = "none" },
                   new SelectItem() { Text = "Bootstrap Version 4", Value = "BT4" },
                   new SelectItem() { Text = "Bootstrap Version 5", Value = "BT5" },
            };
        }
    }
}