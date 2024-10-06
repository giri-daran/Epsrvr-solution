using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;
using EpiserverBH.Models.Pages.Development;

namespace EpiserverBH.Models.Pages.Development
{
    [ContentType(DisplayName = "Generic-HE-HCP", GUID = "229FDBDB-15FE-4147-A5C4-B0BB453AC295", Description = "Generic HE HCP Page Type", GroupName = "Development")]
    public class GenericHEHCP : GenericHome
    {
        
    }
}