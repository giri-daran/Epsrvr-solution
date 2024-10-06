using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System.Collections.Generic;

namespace EpiserverBH.Models.Pages.Development
{
    [ContentType(DisplayName = "Generic-HCP", GUID = "21AD3316-F77F-4333-B1C3-64331CE300B4", Description = "Generic HCP Page Type", GroupName = "Development")]
    public class GenericHCP : GenericHome
    {
    }
}