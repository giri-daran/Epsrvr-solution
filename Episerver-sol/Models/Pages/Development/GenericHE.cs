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
    [ContentType(DisplayName = "Generic-HE", GUID = "68BE6D18-9C84-4133-B1DE-A55D5915D01C", Description = "Generic HE Page Type", GroupName = "Development")]
    public class GenericHE : GenericHome
    {
        
    }
}