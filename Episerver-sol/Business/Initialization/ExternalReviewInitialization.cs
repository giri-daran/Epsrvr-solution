using Advanced.CMS.ExternalReviews;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EpiserverBH.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(FrameworkInitialization))]
    public class ExternalReviewInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            TimeSpan interval = new TimeSpan(360, 0, 0);
            context.Services.Configure<ExternalReviewOptions>(options =>
            {
                options.EditableLinksEnabled = true;
                options.PinCodeSecurity.Enabled = true;
                options.PinCodeSecurity.Required = false;
                options.PinCodeSecurity.CodeLength = 4;
                options.ProlongDays = 15;
                options.ViewLinkValidTo = interval;
            });
        }

        public void Initialize(InitializationEngine context) {
            ServiceLocator.Current.GetInstance<ExternalReviewOptions>().EditableLinksEnabled = true;
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}