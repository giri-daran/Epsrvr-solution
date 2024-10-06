using EPiServer;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.HKImageContent
{

    public class FormList
    {
        [Display(Name = "label Name")]
        public virtual String FieldsText { get; set; }

        [Display(Name = " Field Type")]
        [SelectOne(SelectionFactoryType = typeof(FieldTypeList))]
        public virtual string FieldType { get; set; }

        [Display(Name = "Required Field?")]
        [SelectMany(SelectionFactoryType = typeof(FieldTypeListSingle))]
        public virtual string Optional { get; set; }
    }

    public class AnchorList
    {
        [Display(Name = " Sub Heading")]
        public virtual XhtmlString Subheading { get; set; }

        [Display(Name = "Link Name")]
        public virtual XhtmlString NameText { get; set; }
        [Display(Name = "Link Url")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url HLink { get; set; }

        [Display(Name = " LinkType")]
        [SelectOne(SelectionFactoryType = typeof(LinkType))]
        public virtual string LinkType { get; set; }

        [Display(
                Name = "Anchor Unique Id")]
        [RegularExpression("^[A-Za-z0-9_-]*$")]
        public virtual string Id { get; set; }
        [Display(Name = "Internal Link URL")]
        public virtual string LinkInt { get; set; }
    }

    public class TermsList
    {
        [Display(Name = "Country  Name")]
        public virtual XhtmlString CountryName { get; set; }

        [Display(Name = "List of Pdf")]
        public virtual LinkItemCollection PdfList { get; set; }

        [Display(
                Name = "Unique Id")]
        [RegularExpression("^[A-Za-z0-9_-]*$")]
        public virtual string Id { get; set; }
    }

    public class ProcurementList
    {
        [Display(Name = "Procurement List")]
        public virtual XhtmlString CountryName { get; set; }

        [Display(Name = "List of Pdf")]
        public virtual LinkItemCollection PdfList { get; set; }

        [Display(
                Name = "Unique Id")]
        [RegularExpression("^[A-Za-z0-9_-]*$")]
        public virtual string Id { get; set; }

        [Display(Name = "List of effective dates")]
        public virtual XhtmlString EffectiveDate { get; set; }
    }
    public class FooterList1
    {
        [Display(Name = "Link Name")]
        public virtual string Text1 { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link1 { get; set; }
        [Display(Name = "Internal Link URL")]
        public virtual string LinkInt { get; set; }
    }
    public class FooterList2
    {
        [Display(Name = "Link Name")]
        public virtual string Text2 { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link2 { get; set; }
        [Display(Name = "Internal Link URL")]
        public virtual string LinkInt { get; set; }
    }
    public class FooterList3
    {
        [Display(Name = "Link Name")]
        public virtual string Text3 { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link3 { get; set; }
    }
    public class FooterList4
    {
        [Display(Name = "Link Name")]
        public virtual string Text4 { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link4 { get; set; }
    }
    public class FooterList5
    {
        [Display(Name = "Link Name")]
        public virtual string Text5 { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link5 { get; set; }
    }

    public class SiteMapList
    {
        [Display(Name = "Page")]
        public virtual string Page { get; set; }

        [Display(Name = "Link")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link { get; set; }
    }

   
    public class FieldTypeList : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "text", Value = "text" }, new SelectItem() { Text = "File", Value = "file" }, new SelectItem() { Text = "Email", Value = "email" }, new SelectItem() { Text = "Numeric", Value = "number" }, new SelectItem() { Text = "Radio button", Value = "radio" }, new SelectItem() { Text = "Text Area", Value = "textarea" }
            };
        }

    }
    public class FieldTypeListSingle : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Required", Value = "required" }
            };
        }

    }
    public class BackgroundColorCoverage : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Apply Background Color to Screen Width?", Value = "yes" }
            };
        }

    }
    public class LiveTextType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Bottom-Left", Value = "bottom-left" }, new SelectItem() { Text = "Top-Left", Value = "top-left" }, new SelectItem() { Text = "Top-Right", Value = "top-right" }, new SelectItem() { Text = "Bottom-Right", Value = "bottom-right" }
            };
        }

    }

    public class CouponsList
    {
        [Display(
                        Name = "Image",
                        GroupName = SystemTabNames.Content,
                        Order = 10)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [Display(Name = "AltText",
                    Description = "AltText",
                    GroupName = SystemTabNames.Content,
                    Order = 11
                    )]
        public virtual string AltText { get; set; }

        [Display(Name = "Link1 Name")]
        public virtual string Link1Name { get; set; }

        [Display(Name = "Link1 URL")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link1 { get; set; }

        [Display(Name = "Table Text",
             Description = "Text",
             GroupName = SystemTabNames.Content,
             Order = 35
             )]
        public virtual XhtmlString Text { get; set; }

        [Display(Name = "Link2 Name")]
        public virtual string Link2Name { get; set; }

        [Display(Name = "Link2 URL")]
        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        public virtual Url Link2 { get; set; }

    }


    public class NumberOfColumns : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Single", Value = "Single" }, new SelectItem() { Text = "Multiple", Value = "Multiple" }
            };
        }

    }

    public class StockSticker : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Yes", Value = true }, new SelectItem() { Text = "No", Value = false }
            };
        }

    }

    public class ButtonTarget : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Self", Value = "True" }
            };
        }

    }

    public class ImagePosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Top", Value = "top" }, new SelectItem() { Text = "Bottom", Value = "content-direction-ttb" }, new SelectItem() { Text = "Right", Value = "content-direction-rtl" }, new SelectItem() { Text = "Left", Value = "content-direction-ltr" }
            };
        }

    }

    public class ImageShape : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Round", Value = "rounded-circle" }, new SelectItem() { Text = "Square", Value = "img-thumbnail" }
            };
        }

    }

    public class BackGroundColor : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "#009aa6", Value = "#009aa6" }, new SelectItem() { Text = "#a1338c", Value = "#a1338c" },new SelectItem() { Text = "#edf6f9", Value = "#edf6f9" },new SelectItem() { Text = "White", Value = "#ffffff" },new SelectItem() { Text = "Transparent", Value = "transparent" }
            };
        }

    }
    public class TextAlignment : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Left", Value = "text-start" }, new SelectItem() { Text = "Center", Value = "text-center" },new SelectItem() { Text = "Right", Value = "text-end" }
            };
        }

    }

    public class ButtonColor : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Blue", Value = "btn-custom" }, new SelectItem() { Text = "White", Value = "btn-custom btn-custom-white" }
            };
        }

    }

    public class Alignment : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Left", Value = "float-lg-start" }, new SelectItem() { Text = "Right", Value = "float-lg-end" }
            };
        }

    }

    public class LinkType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Website", Value = "website" }, new SelectItem() { Text = "PDF", Value = "pdf" } , new SelectItem() { Text = "None", Value = "none" }
            };
        }

    }

    public class NoOfBlocks : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Two", Value = "Two" }, new SelectItem() { Text = "Three", Value = "Three" }
            };
        }

    }

    public class ArticleType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Newsroom", Value = "Newsroom" }, new SelectItem() { Text = "Internal Announcement", Value = "Internal" }
            };
        }

    }

    public class FormType : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Supplier Form", Value = "Supplier Form" }, new SelectItem() { Text = "Medical Form", Value = "Medical Form" }
            };
        }

    }

    public class CurvePosition : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] { new SelectItem() { Text = "Left", Value = "curve-left" }, new SelectItem() { Text = "Right", Value = "curve-right" }, new SelectItem() { Text = "No Curve", Value = "NoCurve" }
            };
        }

    }


    public class AccordionList
    {

        [Display(Name = "QuestionNo")]
        public virtual string QuestionNo { get; set; }


        [Display(
                   Name = "QuestionName")]
        public virtual XhtmlString QuestionName { get; set; }


        [Display(Name = "Question Description")]
        public virtual XhtmlString QDescription { get; set; }
    }
}

