using EPiServer;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EpiserverBH.Models.Blocks.HKAccordion
{

    public class HKAccordionList
    {
       
        [Display(Name = "QuestionNo")]
        public virtual string QuestionNo { get; set; }

        
        [Display(
                   Name = "QuestionName")]
        public virtual XhtmlString QuestionName { get; set; }

       
        [Display(Name = "Question Description")]
        public virtual XhtmlString QDescription { get; set; }
    }


    [PropertyDefinitionTypePlugIn]
    public class AccordionListContent : PropertyList<HKAccordionList> { }

}