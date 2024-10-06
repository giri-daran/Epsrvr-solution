using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiserverBH.Models.Blocks.RepFinder
{
    [ContentType(DisplayName = "RepFinderBlock", GUID = "87cea548-b500-422a-a019-f9a075e88ec7", Description = "Bausch RepFinder Block",Order = 201)]
    [SiteImageUrl("/assets/Bauschsurgical/img/gfx/map.png")]
    public class RepFinderBlock : BlockData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Name",
                    Description = "Name field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string Name { get; set; }
         */
    }
}