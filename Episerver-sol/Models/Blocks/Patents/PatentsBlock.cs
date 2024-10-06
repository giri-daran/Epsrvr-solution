using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;
using EPiServer.PlugIn;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;

namespace EpiserverBH.Models.Blocks.Patents
{
    [ContentType(DisplayName = "PatentsBlock", GUID = "cfb33b3c-ab53-409b-b5f6-a643f3dbc9a5", Description = "")]
    [SiteImageUrl("/assets/Bauschsurgical/img/gfx/jumbotron.png")]
    public class PatentsBlock : BlockData
    {
        [Display(Name = "Related Products",
           GroupName = SystemTabNames.Content, Description = "Related Products List",
           Order = 5
           )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<Patents>))]
        public virtual IList<Patents> PatentsListBlock { get; set; }
    }
    public class Patents
    {
        
        [Display(Name = "Product Name")]
        [CultureSpecific]
        public virtual string ProductName { get; set; }

        [Display(Name = "Patent Space seperated")]
        [CultureSpecific]
        public virtual string Patent { get; set; }

    }
    [PropertyDefinitionTypePlugIn]
    public class ProductListProperty : PropertyList<Patents> { }
}