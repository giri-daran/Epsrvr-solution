
using System.ComponentModel.DataAnnotations;
using EPiServer.Cms.Shell.Json.Internal;
using Newtonsoft.Json;
using EPiServer.Shell.ObjectEditing;
using EPiServer.PlugIn;
using EPiServer.Web;

namespace EpiserverBH.Models.Blocks.Accordion
{

   

    public class AccordionList
    {
        [Display(Name = "Desktop Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference DesktopImage {  get; set; }
        [Display(Name = "Mobile Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MobileImage { get; set; }

        [Display(Name = "Alt Tag")]
        [CultureSpecific]
        public virtual string AltTag { get; set; }

        [Display(Name = "List Title")]
        [CultureSpecific]
        public virtual string ListTitle { get; set; }

        //[Display(Name = "Title Position")]
        //[CultureSpecific]
        //public virtual string TitlePosition { get; set; }

        //[Display(Name = "XDescription")]
        //[CultureSpecific]
        //public virtual XhtmlString XDescription { get; set; }

        [Display(Name = "Description")]
        [CultureSpecific]
        public virtual XhtmlString Description { get; set; }


        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link { get; set; }

        [Display(Name = "Link Title")]
        [CultureSpecific]
        public virtual string LinkTitle { get; set; }



        [Display(
           Name = "Active",
          Order = 16)]
        [CultureSpecific]
        public virtual bool Active { get; set; }
    }



    public class IContainerLayout : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {     
                   new SelectItem() { Text = "Container", Value = "container" }, 
                   new SelectItem() { Text = "container-full-width", Value = "container-full-width" },
};
        }
    }


    public class IContainerLayoutSize : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                  new SelectItem() { Text = "Select container Width", Value = "none" },                 
                   new SelectItem() { Text = "50%", Value = "50%" },
                   new SelectItem() { Text = "75%", Value = "75%" },
                   new SelectItem() { Text = "100%", Value = "100%" },
};
        }
    }

    public class TPosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {
                
                    new SelectItem() { Text = "Select Position", Value = "none" },
                    new SelectItem() { Text = "Left", Value = "left" },
                    new SelectItem() { Text = "Center", Value = "center" },
                    new SelectItem() { Text = "Right", Value = "right" },
                    
            };
        }

    }
    public class imgTitleDecPosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {

                    new SelectItem() { Text = "end", Value = "end" },
                    new SelectItem() { Text = "start", Value = "start" },
                    new SelectItem() { Text = "center", Value = "center" },
                        new SelectItem() { Text = "space-around", Value = "space-around" },
                            new SelectItem() { Text = "space-between", Value = "space-between" },

            };
        }

    }
        public class closeimgTitleDecPosition : ISelectionFactory
        {
            public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
            {
                return new ISelectItem[] {

                    new SelectItem() { Text = "start", Value = "start" },
                    new SelectItem() { Text = "center", Value = "center" },
                    new SelectItem() { Text = "end", Value = "end" },

            };
            }

        }

        public class MaxNumberofImage : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {
                     new SelectItem() { Text = "3", Value = "3" },
                     new SelectItem() { Text = "4", Value = "4" },
                       new SelectItem() { Text = "5", Value = "5" },
                         new SelectItem() { Text = "6", Value = "6" },
                           new SelectItem() { Text = "7", Value = "7" },
                             new SelectItem() { Text = "8", Value = "8" },

            };
        }

    }


    [PropertyDefinitionTypePlugIn]
    public class SurgicalSliderProperty : PropertyList<AccordionList> { }
   
}