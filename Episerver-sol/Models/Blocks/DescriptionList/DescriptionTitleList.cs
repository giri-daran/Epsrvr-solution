using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiserverBH.Models.Blocks.DescriptionList
{
    public class DescriptionTitleList
    {

        [Display(Name = "List Title")]
        [CultureSpecific]
        public virtual XhtmlString ListTitle { get; set; }

        [Display(Name = "List Seperator")]
        public virtual bool ListSeperator { get; set; }

        [Display(Name = "List Seperator Color")]
        [CultureSpecific]
        public virtual string ListSeperatorColor { get; set; }

        [Display(Name = "List Seperator Thickness")]
        [CultureSpecific]
        public virtual string ListSeperatorThickness { get; set; }


        [Display(Name = "Description Details")]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<DescriptionList>))]
        public virtual IList<DescriptionList> DescripList { get; set; }
    }


    [PropertyDefinitionTypePlugIn]
    public class DescriptionTitleListProperty : PropertyList<DescriptionTitleList> { }

}