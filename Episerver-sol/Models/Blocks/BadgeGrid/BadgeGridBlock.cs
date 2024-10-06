using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;

namespace EpiserverBH.Models.Blocks.BadgeGrid
{
    [ContentType(DisplayName = "BadgeGridBlock", GUID = "F2FD7335-6681-4D5C-AF67-4DF6050FAC4F", Description = "Block used to display Badge Grid", GroupName = Globals.GroupNames.BadgeGrid)]
    
    public class BadgeGridBlock : BlockData
    {

		[Display(Name = "BlockId", Order = 1)]
		public virtual string BlockId { get; set; }

        [Display(Name = "BlockClass", Order = 2)]
        public virtual string BlockClass { get; set; }

        [Display(Name = "BadgeGrid Title Background Color", GroupName = "Information", Description = "BadgeGrid Title Background Color", Order = 3)]
        [CultureSpecific]
        public virtual string BadgeGridTitleBackground { get; set; }

        [Display(Name = "BadgeGrid Title", GroupName = "Information", Description = "BadgeGrid Title", Order = 4)]
        [CultureSpecific]
        public virtual XhtmlString BadgeGridTitle { get; set; }


        [Display(Name = "Background Layout", GroupName = "Information", Description = "Background Layout", Order = 5)]
        [SelectOne(SelectionFactoryType = typeof(IContainerLayout))]
        public virtual string BackgroundLayout { get; set; }


        [Display(Name = "Background Color", GroupName = "Information", Description = "Background Color", Order = 6)]
        [CultureSpecific]
        public virtual string BackgroundColor { get; set; }


        [Display(Name = "Badge Layout Type", GroupName = "Information", Description = "Badge Layout Type", Order = 7)]
        [SelectOne(SelectionFactoryType = typeof(IContainerLayout))]
        public virtual string BadgeLayoutType { get; set; }


        [Display(Name = "Badge Shadow", GroupName = "Information", Description = "Badge Shadow", Order = 8)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string BadgeShadow { get; set; }


        [Display(Name = "Badge Type", GroupName = "Information", Description = "Badge Type", Order = 9)]
        [SelectOne(SelectionFactoryType = typeof(BType))]
        [CultureSpecific]
        public virtual string BadgeType { get; set; }


        [Display(Name = "Badge Max Width", GroupName = "Information", Description = "Badge Max Width", Order = 10)]
        [SelectOne(SelectionFactoryType = typeof(IBadgeMaxWidth))]
        public virtual string BadgeMaxWidth { get; set; }

        #region New Fields


        [Display(Name = "Max Number of Badge", GroupName = "Information", Description = "MaxNumberofBadge", Order = 11)]
        [SelectOne(SelectionFactoryType = typeof(MaxNumberofBadge))]
        public virtual string MaxNumberofBadge { get; set; }

        #endregion

        [Display(Name = "Badge List", GroupName = "Information", Description = "Badge List", Order = 12)]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<BadgeList>))]
        public virtual IList<BadgeList> BadgeGridListBlock { get; set; }


        




    }
}