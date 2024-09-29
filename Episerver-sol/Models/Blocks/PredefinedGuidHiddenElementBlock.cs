using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EpiserverBH.Models.Blocks
{
    /// <summary>
    /// Custom predefined hidden field with Guid populated
    /// </summary>
    [ContentType(GUID = "F3864C57-3397-4013-B94C-FDBF899FBF9A", DisplayName = "Predefined GUID Hidden Field", Description = "A predefined hidden field with guid prepopulated", Order = 100)]
    public class PredefinedGuidHiddenElementBlock : PredefinedHiddenElementBlock
    {
        public override string GetDefaultValue()
        {
            return Guid.NewGuid().ToString();
        }
    }
}