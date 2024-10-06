using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using Newtonsoft.Json;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.PlugIn;
using EPiServer;

namespace EpiserverBH.Models.Blocks.BadgeGrid
{
    public class LinkList
    {


        [Display(Name = "Link Type")]
        [SelectOne(SelectionFactoryType = typeof(LType))]
        [CultureSpecific]
        public virtual string LinkType { get; set; }

        [Display(Name = "Link Text")]
        public virtual string LinkText { get; set; }

        


        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        [Display(Name = "Link Url")]
        public virtual Url LinkUrl { get; set; }



        [Display(Name = "Link Position")]
        [SelectOne(SelectionFactoryType = typeof(TPosition))]
        [CultureSpecific]
        public virtual string LinkPosition { get; set; }


        [Display(Name = "Link Image")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference LinkImage { get; set; }


    }



    [PropertyDefinitionTypePlugIn]
    public class BadgeLinkList1Property : PropertyList<LinkList> { }


}