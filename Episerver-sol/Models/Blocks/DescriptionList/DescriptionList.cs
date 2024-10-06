using EPiServer;
using EPiServer.Cms.Shell.Extensions;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.Cms.Shell.UI.ObjectEditing;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EpiserverBH.Models.Blocks.DescriptionList
{
    public class DescriptionList
    {

        [Display(Name = "List Description")]
        [CultureSpecific]
        public virtual XhtmlString ListDescription { get; set; }

        [Display(Name = "Foot Note")]
        [CultureSpecific]
        public virtual XhtmlString FootNote { get; set; }

        [Display(Name = "Link Text")]
        public virtual string LinkText { get; set; }        

        [Display(Name = "Link Position")]
        [SelectOne(SelectionFactoryType = typeof(LPosition))]
        [CultureSpecific]
        public virtual string LinkPosition { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        [Display(Name = "Link Url")]
        public virtual Url LinkUrl { get; set; }

        [Display(Name = "Link Type")]
        [SelectOne(SelectionFactoryType = typeof(LType))]
        [CultureSpecific]
        public virtual string LinkType { get; set; }

        [Display(Name = "Link Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference LinkImage { get; set; }

        [Display(Name = "Description Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference DescImage { get; set; }










    }



    public class IListLayout : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                   new SelectItem() { Text = "FullWidth", Value = "fullwidth" },
                   new SelectItem() { Text = "Container-Width", Value = "container-width" },
            };
        }
    }

    public class DType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "Products List", Value = "productslist" },
                    new SelectItem() { Text = "Description List", Value = "descriptionlist" },
                    new SelectItem() { Text = "Description With Image List", Value = "descriptionwithimagelist" },
                    new SelectItem() { Text = "Type1", Value = "type1" },
                    new SelectItem() { Text = "Type2", Value = "type2" },
            };
        }
    }

    public class LPosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "Left", Value = "left" },
                    new SelectItem() { Text = "Right", Value = "right" },
                    new SelectItem() { Text = "Center", Value = "center" },
                    new SelectItem() { Text = "Right Vertically Center", Value = "rightverticallycenter" },
                    new SelectItem() { Text = "Left Vertically Center", Value = "rightverticallycenter" },
            };
        }
    }

    public class LType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "Image", Value = "image" },
                    new SelectItem() { Text = "Button", Value = "button" },
                    new SelectItem() { Text = "Text", Value = "text" },
                    new SelectItem() { Text = "Type1", Value = "type1" },
                    new SelectItem() { Text = "Type2", Value = "type2" },
                    new SelectItem() { Text = "Type3", Value = "type3" },
            };
        }
    }


    [PropertyDefinitionTypePlugIn]
    public class DescriptionListProperty : PropertyList<DescriptionList> { }

}