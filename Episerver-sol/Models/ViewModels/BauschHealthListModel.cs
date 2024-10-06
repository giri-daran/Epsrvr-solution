using System.Collections.Generic;
using EPiServer.Core;
using EpiserverBH.Models.Blocks;
using EpiserverBH.Models.Blocks.BauschHealth;

namespace EpiserverBH.Models.ViewModels
{
    public class BauschHealthListModel
    {
        public BauschHealthListModel(BauschHealthNewsBlock block)
        {
            Heading = block.Heading;
            ShowIntroduction = block.IncludeIntroduction;
            ShowPublishDate = block.IncludePublishDate;
        }
        public string Heading { get; set; }
        public IEnumerable<PageData> Pages { get; set; }
        public bool ShowIntroduction { get; set; }
        public bool ShowPublishDate { get; set; }
    }
}
