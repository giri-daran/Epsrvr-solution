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
using System.Security.Policy;
using System.Web;

namespace EpiserverBH.Models.Blocks.BadgeGrid
{
    public class BadgeList
    {
        [Display(Name = "Badge Header")]
        [CultureSpecific]
        public virtual XhtmlString BadgeHeader { get; set; }

        [Display(Name = "Badge MiddeleHeader")]
        [CultureSpecific]
        public virtual XhtmlString BadgeMiddleHeader { get; set; }

		[Display(Name = "Desktop Image")]
		[UIHint(UIHint.Image)]
		public virtual ContentReference DescImage { get; set; }

		[Display(Name = "Mobile Image")]
		[UIHint(UIHint.Image)]
		public virtual ContentReference MobImage { get; set; }


        [Display(Name = "Label Text")]
        [CultureSpecific]
        public virtual string BadgeLabel { get; set; }


        [Display(Name = "Alt Text")]
        [CultureSpecific]
        public virtual string BadgeAlt { get; set; }

        [Display(Name = "Badge Background Color")]
        [CultureSpecific]
        public virtual string BadgeBgColor { get; set; }

        [Display(Name = "Content")]
        [CultureSpecific]
        public virtual XhtmlString Content { get; set; }


        [Display(Name = "Link Details")]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<LinkList>))]
        public virtual IList<LinkList> LinkListBlock { get; set; }

        [Display(Name = "Badge Footer")]
        [CultureSpecific]
        public virtual XhtmlString BadgeFooter { get; set; }


        [Display(Name = "Active", Order = 16)]
        [CultureSpecific]
        public virtual bool Active { get; set; }
    }


    public class TPosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "Left", Value = "left" },
                    new SelectItem() { Text = "Right", Value = "right" },
                    new SelectItem() { Text = "Center", Value = "center" },

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

    public class IContainerLayout : ISelectionFactory
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

    public class BType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "ImageTitle", Value = "imagetitle" },
                    new SelectItem() { Text = "ColorBackground", Value = "colorbackground" },
                    new SelectItem() { Text = "Normal", Value = "normal" },
                    new SelectItem() { Text = "SiteType", Value = "sitetype" },
                    new SelectItem() { Text = "MultiLink", Value = "multilink" },
                    new SelectItem() { Text = "IconWithText", Value = "iconwithtext" },
                    new SelectItem() { Text = "Type1", Value = "type1" },

            };
        }

    }

    public class IBadgeMaxWidth : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                  new SelectItem() { Text = "Select Badge Width", Value = "none" },
                   new SelectItem() { Text = "50%", Value = "50%" },
                   new SelectItem() { Text = "75%", Value = "75%" },
                   new SelectItem() { Text = "100%", Value = "100%" },
            };
        }
    }



    public class MaxNumberofBadge : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {
                    new SelectItem() { Text = "1", Value = "1" },
                    new SelectItem() { Text = "2", Value = "2" },
                    new SelectItem() { Text = "3", Value = "3" },
                    new SelectItem() { Text = "4", Value = "4" },
                    new SelectItem() { Text = "5", Value = "5" },
                    new SelectItem() { Text = "6", Value = "6" },
                    new SelectItem() { Text = "7", Value = "7" },
            };
        }

    }




    [PropertyDefinitionTypePlugIn]
    public class SurgicalSliderProperty : PropertyList<BadgeList> { }


}