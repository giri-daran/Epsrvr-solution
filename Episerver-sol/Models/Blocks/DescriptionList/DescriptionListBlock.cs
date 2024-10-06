using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Cms.Shell.Json.Internal;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using Newtonsoft.Json;

namespace EpiserverBH.Models.Blocks.DescriptionList
{
    [ContentType(DisplayName = "DescriptionListBlock", GUID = "9CA86B52-8C9C-4CAA-9952-236D996FEF62", Description = "Block used to display Description List", GroupName = Globals.GroupNames.DescriptionList)]
    [SiteImageUrl("/assets/BadgeGrid/img/gfx/slider.png")]
    public class DescriptionListBlock : BlockData
    {
        [Display(Name = "Header Title", GroupName = "Information", Description = "Header Title", Order = 1)]
        [CultureSpecific]
        public virtual XhtmlString HeaderTitle { get; set; }

        [Display(Name = "Product Title", GroupName = "Information", Description = "Product Title", Order = 2)]
        [CultureSpecific]
        public virtual XhtmlString ProductTitle { get; set; }

        [Display(Name = "Description", GroupName = "Information", Description = "Description", Order = 3)]
        [CultureSpecific]
        public virtual string Description { get; set; }

        [Display(Name = "Category Title", GroupName = "Information", Description = "Category Title", Order = 4)]
        [CultureSpecific]
        public virtual string CategoryTitle { get; set; }

        [Display(Name = "List Layout", GroupName = "Information", Description = "List Layout", Order = 5)]
        [SelectOne(SelectionFactoryType = typeof(IListLayout))]
        public virtual string ListLayout { get; set; }


        [Display(Name = "List Type", GroupName = "Information", Description = "List Type", Order = 6)]
        [SelectOne(SelectionFactoryType = typeof(DType))]
        [CultureSpecific]
        public virtual string ListType { get; set; }



        [Display(Name = "Footer Url Text", GroupName = "Information", Description = "Footer Url Text", Order = 7)]
        public virtual string FooterUrlText { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(UrlConverter))]
        [Display(Name = "Footer Url", GroupName = "Information", Description = "Footer Url", Order = 8)]
        public virtual Url FooterUrl { get; set; }



        [Display(Name = "Description Title List", GroupName = "Information", Description = "Description Title List", Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<DescriptionTitleList>))]
        public virtual IList<DescriptionTitleList> DescripTitleList { get; set; }


        




    }
}