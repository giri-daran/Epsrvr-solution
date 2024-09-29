using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(DisplayName = "StateSelectionElementBlock", GUID = "ef6d8fde-187f-44c0-81c6-27a42616d9b4", Description = "Pre Populated States")]
    [SiteImageUrl("/assets/Global/img/selectionelementblock.png")]
    public class StateSelectionElementBlock : SelectionElementBlock
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        [Display(Name = "InputCSS", Order = 7, Description = "InputCSS", GroupName = SystemTabNames.Content)]
        public virtual String InputCSS { get; set; }
        [Display(Name = "Input Div CSS", Order = 8, Description = "Input Div CSS", GroupName = SystemTabNames.Content)]
        public virtual String InputDivCSS { get; set; }

        [Display(Name = "LabelCSS", Order = 5, Description = "LabelCSS", GroupName = SystemTabNames.Content)]
        public virtual String LabelCSS { get; set; }
        [Display(Name = "Label Div CSS", Order = 6, Description = "Label Div CSS", GroupName = SystemTabNames.Content)]
        public virtual String LabelDivCSS { get; set; }

        [Display(Name = "Control DivCSS", Order = 4, Description = "Control DivCSS", GroupName = SystemTabNames.Content)]
        public virtual String DivCSS { get; set; }
    }
    public class StatesItem
    {
        public StatesItem() { }
        public string Caption { get; set; }
        public string Value { get; set; }
    }
}