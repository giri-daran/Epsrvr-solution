using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using static EpiserverBH.Globals;

namespace EpiserverBH.Models.Blocks
{
    [ContentType(DisplayName = "HTML Content", GUID = "5fe99abe-b5f4-4184-9631-0750065b6ef8", Description = "")]
    [Access(Roles = "Everyone")]
    public class RawHTML : BlockData
    {
       
        [CultureSpecific]
        [Required(AllowEmptyStrings = true)]
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 1)]
       
       // [DefaultDisplayOption(ContentAreaTags.HalfWidth)]
        public virtual XhtmlString HTMLContent { get; set; }
        public DisplayOptionEnum DefaultDisplayOption => DisplayOptionEnum.Full;


        public interface IDefaultDisplayOption
        {
            DisplayOptionEnum DefaultDisplayOption { get; }
        }
        public enum DisplayOptionEnum
        {
            Unknown,

            [BootstrapClass(Name = "col-md-12")]
            Full = 12,

            [BootstrapClass(Name = "col-md-9")]
            ThreeQuaters = 9,

            [BootstrapClass(Name = "col-md-6")]
            Half = 6,

            [BootstrapClass(Name = "col-md-3")]
            Quater = 3,
        }
    }

    internal class BootstrapClassAttribute : Attribute
    {
        public string Name { get; set; }
    }
}