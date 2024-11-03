using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EpiserverBH.Models.Pages;
using EpiserverBH.Models.Pages.Development;
using EpiserverBH.Models.Pages.Xifaxan;
using EpiserverBH.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace EpiserverBH.Business
{
    [ServiceConfiguration]
    public class PageViewContextFactory
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly IDatabaseMode _databaseMode;
        private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

        public PageViewContextFactory(
            IContentLoader contentLoader,
            UrlResolver urlResolver,
            IDatabaseMode databaseMode,
            IOptionsMonitor<CookieAuthenticationOptions> optionMonitor)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _databaseMode = databaseMode;
            _cookieAuthenticationOptions = optionMonitor.Get(IdentityConstants.ApplicationScheme);
        }
        public void ConfigureRoutes(bool TrailingSlash)
        {
            var routingOptions = ServiceLocator.Current.GetInstance<RoutingOptions>();
            routingOptions.UseTrailingSlash = TrailingSlash;
        }
        public virtual LayoutModel CreateLayoutModel(ContentReference currentContentLink, HttpContext requestContext)
        {

            var content = _contentLoader.Get<IContent>(currentContentLink);


            if (content is PageData)
            {
                var startPageContentLink = SiteDefinition.Current.StartPage;
                var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

                string type = content.Property["PageTypeName"].ToString();
                PageData currentdata = repository.Get<PageData>(currentContentLink);
                if (currentContentLink.CompareToIgnoreWorkID(startPageContentLink))
                {
                    startPageContentLink = currentContentLink;
                }

                if (type == "HCP")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<HCP>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                       
                        var pages = _contentLoader.GetChildren<Home>(startPageContentLink);
                        var HCPPage = pages.Where(i => i.PageTypeName == "HCP").FirstOrDefault();

                        if (HCPPage == null)
                        {
                            var startPage = _contentLoader.Get<HCP>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            if (HCPPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = HCPPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = HCPPage.GoogleTagManager,
                                SiteTemplate = HCPPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }


                }
                else if (type == "GenericHCP")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<GenericHCP>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        var pages = _contentLoader.GetChildren<SitePageData>(startPageContentLink);
                        var HCPPage = pages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                        //PageReference listRoot = HCPPage.PageLink;
                        //var HCPPages = _contentLoader.GetChildren<GenericHCP>(listRoot).OfType<GenericHCP>();
                        var GenericHCPPage = (EpiserverBH.Models.Pages.Development.GenericHCP)HCPPage;//HCPPages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                        //var GenericHCPPage = HCPPages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                        if (GenericHCPPage == null)
                        {
                            var startPage = _contentLoader.Get<GenericHCP>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = GenericHCPPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(HCPPage)),
                                GoogleTagManager = GenericHCPPage.GoogleTagManager,
                                SiteTemplate = GenericHCPPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }

                }

                else if (type == "GenericHEHCP")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<GenericHEHCP>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        //var HCPPage = _contentLoader.GetBySegment(startPageContentLink,"/ecp/", CultureInfo.InvariantCulture);
                        var pages = _contentLoader.GetChildren<SitePageData>(startPageContentLink);
                        var HCPPage = pages.Where(i => i.PageTypeName == "GenericHCP").FirstOrDefault();

                        PageReference listRoot = HCPPage.PageLink;
                        var HCPPages = _contentLoader.GetChildren<GenericHEHCP>(listRoot).OfType<GenericHEHCP>();
                        var GenericHEHCPPage = HCPPages.Where(i => i.PageTypeName == "GenericHEHCP").FirstOrDefault();

                        if (GenericHEHCPPage == null)
                        {
                            var startPage = _contentLoader.Get<GenericHEHCP>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = GenericHEHCPPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = GenericHEHCPPage.GoogleTagManager,
                                SiteTemplate = GenericHEHCPPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }
                }

                else if (type == "IBSDHCP")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<IBSDHCP>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        var pages = _contentLoader.GetChildren<Home>(startPageContentLink);
                        var HCPPage = pages.Where(i => i.PageTypeName == "HCP").FirstOrDefault();

                        PageReference listRoot = HCPPage.PageLink;
                        var HCPPages = _contentLoader.GetChildren<IBSDHCP>(listRoot).OfType<IBSDHCP>();
                        var IBSDHCPPage = HCPPages.Where(i => i.PageTypeName == "IBSDHCP").FirstOrDefault();

                        if (IBSDHCPPage == null)
                        {
                            var startPage = _contentLoader.Get<IBSDHCP>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = IBSDHCPPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = IBSDHCPPage.GoogleTagManager,
                                SiteTemplate = IBSDHCPPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }

                }

                else if (type == "HEHCP")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<HEHCP>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        //var HCPPage = _contentLoader.GetBySegment(startPageContentLink,"/ecp/", CultureInfo.InvariantCulture);
                        var pages = _contentLoader.GetChildren<Home>(startPageContentLink);
                        var HCPPage = pages.Where(i => i.PageTypeName == "HCP").FirstOrDefault();

                        PageReference listRoot = HCPPage.PageLink;
                        var HCPPages = _contentLoader.GetChildren<HEHCP>(listRoot).OfType<HEHCP>();
                        var HEHCPPage = HCPPages.Where(i => i.PageTypeName == "HEHCP").FirstOrDefault();

                        if (HEHCPPage == null)
                        {
                            var startPage = _contentLoader.Get<HEHCP>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = HEHCPPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = HEHCPPage.GoogleTagManager,
                                SiteTemplate = HEHCPPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }


                }
                else if (type == "GenericHEDTC")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<HEDTC>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        //var HCPPage = _contentLoader.GetBySegment(startPageContentLink,"/ecp/", CultureInfo.InvariantCulture);
                        var pages = _contentLoader.GetChildren<Home>(startPageContentLink);
                        var HEDTCPage = pages.Where(i => i.PageTypeName == "GenericHEDTC").FirstOrDefault();

                        if (HEDTCPage == null)
                        {
                            var startPage = _contentLoader.Get<HEDTC>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = HEDTCPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = HEDTCPage.GoogleTagManager,
                                SiteTemplate = HEDTCPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }


                }
                else if (type == "HEDTC")
                {
                    if (SiteDefinition.Current.StartPage == currentdata.ContentLink)
                    {
                        var startPage = _contentLoader.Get<HEDTC>(startPageContentLink);
                        if (startPage.TrailingSlash == true)
                        {
                            ConfigureRoutes(false);
                        }
                        else
                        {
                            ConfigureRoutes(true);
                        }
                        return new LayoutModel
                        {
                            Logotype = startPage.SiteLogotype,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = startPage.GoogleTagManager,
                            SiteTemplate = startPage.SiteTemplate,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                        };
                    }
                    else
                    {
                        //var HCPPage = _contentLoader.GetBySegment(startPageContentLink,"/ecp/", CultureInfo.InvariantCulture);
                        var pages = _contentLoader.GetChildren<Home>(startPageContentLink);
                        var HEDTCPage = pages.Where(i => i.PageTypeName == "HEDTC").FirstOrDefault();

                        if (HEDTCPage == null)
                        {
                            var startPage = _contentLoader.Get<HEDTC>(startPageContentLink);
                            if (startPage.TrailingSlash == true)
                            {
                                ConfigureRoutes(false);
                            }
                            else
                            {
                                ConfigureRoutes(true);
                            }
                            return new LayoutModel
                            {
                                Logotype = startPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = startPage.GoogleTagManager,
                                SiteTemplate = startPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }
                        else
                        {
                            return new LayoutModel
                            {
                                Logotype = HEDTCPage.SiteLogotype,
                                LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                                GoogleTagManager = HEDTCPage.GoogleTagManager,
                                SiteTemplate = HEDTCPage.SiteTemplate,
                                LoggedIn = requestContext.User.Identity.IsAuthenticated,
                                LoginUrl = new Microsoft.AspNetCore.Html.HtmlString("/"),
                                IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                            };
                        }

                    }


                }
                else if (type == "Home")
                {
                    var startPage = _contentLoader.Get<Home>(startPageContentLink);
                    if (startPage.TrailingSlash == true)
                    {
                        ConfigureRoutes(false);
                    }
                    else
                    {
                        ConfigureRoutes(true);
                    }
                    return new LayoutModel
                    {
                        Logotype = startPage.SiteLogotype,
                        LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                        GoogleTagManager = startPage.GoogleTagManager,
                        SiteTemplate = startPage.SiteTemplate,
                        LoggedIn = requestContext.User.Identity.IsAuthenticated,
                        LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                        IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
                    };
                }
                else
                {
                    var SiteStartPage = ContentReference.StartPage;
                    PageData startPage = null;
                    startPage = _contentLoader.Get<SitePageData>(SiteStartPage);

                    bool trailingslash = false;
                    try
                    {
                        if (startPage.Property.Contains("TrailingSlash") && startPage.Property["TrailingSlash"].Value != null)
                        {
                            trailingslash = (bool)startPage.Property["TrailingSlash"].Value;
                        }
                        else { trailingslash = false; }

                    }
                    catch (Exception ex)
                    {
                        trailingslash = false;
                    }

                    if (startPage.Property.Contains("FooterLinks"))
                    {
                        return new LayoutModel
                        {
                            Logotype = (Models.Blocks.SiteLogotypeBlock)startPage.Property["SiteLogotype"].Value,
                            GoogleTagManager = (string)startPage.Property["GoogleTagManager"].Value,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly,
                            FooterLinks = (EPiServer.SpecializedProperties.LinkItemCollection)startPage.Property["FooterLinks"].Value
                        };
                    }
                    else
                    {
                        return new LayoutModel
                        {
                            Logotype = (Models.Blocks.SiteLogotypeBlock)startPage.Property["SiteLogotype"].Value,
                            LogotypeLinkUrl = new Microsoft.AspNetCore.Html.HtmlString(_urlResolver.GetUrl(SiteDefinition.Current.StartPage)),
                            GoogleTagManager = (string)startPage.Property["GoogleTagManager"].Value,
                            SiteTemplate = (string)startPage.Property["SiteTemplate"].Value,
                            LoggedIn = requestContext.User.Identity.IsAuthenticated,
                            LoginUrl = new Microsoft.AspNetCore.Html.HtmlString(""),
                            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly

                        };
                    }

                }
            }
            else
            {
                return new LayoutModel
                {

                };
            }

            // Use the content link with version information when editing the startpage,
            // otherwise the published version will be used when rendering the props below.

        }


        private string GetLoginUrl(ContentReference returnToContentLink)
        {
            return $"{_cookieAuthenticationOptions?.LoginPath.Value ?? Globals.LoginPath}?ReturnUrl={_urlResolver.GetUrl(returnToContentLink)}";
        }

        public virtual IContent GetSection(ContentReference contentLink)
        {
            var currentContent = _contentLoader.Get<IContent>(contentLink);

            static bool isSectionRoot(ContentReference contentReference) =>
               ContentReference.IsNullOrEmpty(contentReference) ||
               contentReference.Equals(SiteDefinition.Current.StartPage) ||
               contentReference.Equals(SiteDefinition.Current.RootPage);

            if (isSectionRoot(currentContent.ParentLink))
            {
                return currentContent;
            }

            return _contentLoader.GetAncestors(contentLink)
                .OfType<PageData>()
                .SkipWhile(x => !isSectionRoot(x.ParentLink))
                .FirstOrDefault();
        }
    }
}