using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.UI.Rest;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EpiserverBH.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using EpiserverBH;
using Microsoft.AspNetCore.Http;


namespace EpiserverBH.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(XhtmlString), EditorDescriptorBehavior = EditorDescriptorBehavior.PlaceLast)]
    public class CustomXhtmlStringEditorDescriptor : EditorDescriptor
    {

        private static IHttpContextAccessor _httpContextAccessor;
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            try
            {
                _httpContextAccessor = new HttpContextAccessor();
                if (metadata.CustomEditorSettings.ContainsKey("uiParams"))
                {
                    var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();

                    base.ModifyMetadata(metadata, attributes);



                    string type = string.Empty;
                    string url = string.Empty;
                    if (metadata.DisplayName == "Black Box Message")
                    {
                        var type1 = ((EPiServer.Core.PageData)metadata.Parent.Model).PageTypeName;

                        var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
                        var url1 = urlResolver.GetUrl(((EPiServer.Core.PageData)metadata.Parent.Model).ContentLink);
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddHours(1);
                        
                        var response = _httpContextAccessor.HttpContext.Response;


                        response.Cookies.Delete("Editor");
                        response.Cookies.Delete("type");
                        response.Cookies.Delete("url");
                        response.Cookies.Append("type", type1, option);
                        response.Cookies.Append("url", url1, option);


                    }
                    else
                    {
                        var request = _httpContextAccessor.HttpContext.Request;

                        if (Convert.ToString(request.Cookies["type"]) != null)
                        {
                            type = Convert.ToString(request.Cookies["type"].ToString());
                            // templateName = Convert.ToString(reqCookies["templateName"].ToString());
                            url = Convert.ToString(request.Cookies["url"].ToString());

                        }

                        KeyValuePair<string, object> tinyMceSettings = metadata.EditorConfiguration.FirstOrDefault(x => x.Key == "settings");
                        var dictionary = tinyMceSettings.Value as Dictionary<string, object>;
                        if (dictionary != null)
                        {
                            dictionary.Remove("content_css");


                            // var test = SiteDefinition.Current.GetType<

                            if (SiteDefinition.Current.Name.ToLower().Equals("bauschhealth"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Bauschhealth/css/bootstrap.min.css, /Assets/Bauschhealth/css/main.css, /Assets/Bauschhealth/css/font-awesome.min.css, /Assets/Bauschhealth/css/grid.css, /Assets/Bauschhealth/css/supplier-diversity-profileform.css, /Assets/Bauschhealth/css/medical-inqry.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("bauschhealthgrants"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/BauschhealthGrants/css/bootstrap.min.css, /Assets/BauschhealthGrants/css/main.css, /Assets/BauschhealthGrants/css/font-awesome.min.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("altreno.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Altreno-DTC/css/bootstrap.min.css, /Assets/Altreno-DTC/css/main.css, /Assets/Altreno-DTC/css/slick.min.css, /Assets/Altreno-DTC/css/aos.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("altrenohcp.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Altreno-HCP/css/bootstrap.min.css, /Assets/Altreno-HCP/css/main.css, /Assets/Altreno-HCP/css/slick.min.css, /Assets/Altreno-HCP/css/animate.min.css, /Assets/Altreno-HCP/css/aos.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("aprisorx.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Aprisorx-HCP/css/ie.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Aprisorx-DTC/css/Aprisorx-DTC.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("arazlo.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Arazlo-HCP/css/bootstrap.min.css, /Assets/Arazlo-HCP/css/reset.css, /Assets/Arazlo-HCP/css/main.css");

                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Arazlo-DTC/css/bootstrap.min.css, /Assets/Arazlo-DTC/css/main.min.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("arestin.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Arestin/css/vendor/bootstrap.min.css, /Assets/Arestin/css/vendor/font-awesome.min.css, /Assets/Arestin/css/aos.css, /Assets/Arestin/css/main.css");


                            else if (SiteDefinition.Current.Name.ToLower().Equals("xifaxan.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xifaxan-HCP/css/main.min.css, /Assets/Xifaxan-HCP/css/vendor/font-awesome.min.css");
                                if (type == "HEHCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xifaxan-HCP-HE/css/vendor/font-awesome.min.css, /Assets/Xifaxan-HCP-HE/css/main.min.css, /Assets/Xifaxan-HCP-HE/css/ie/ie.css");
                                if (type == "IBSDHCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xifaxan-HCP-IBSD/css/vendor/font-awesome.min.css, /Assets/Xifaxan-HCP-IBSD/css/main.min.css, /Assets/Xifaxan-HCP-IBSD/css/simplebar.min.css, /Assets/Xifaxan-HCP-IBSD/css/ie/ie.css");
                                if (type == "HEDTC")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xifaxan-HE/css/main.css, /Assets/Xifaxan-HE/css/custom.css, /Assets/Xifaxan-HE/css/reset.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xifaxan-DTC/css/reset.min.css, /Assets/Xifaxan-DTC/css/main.min.css, /Assets/Xifaxan-DTC/css/owl.carousel.min.css, /Assets/Xifaxan-DTC/css/slick.min.css, /Assets/Xifaxan-DTC/css/slick-theme.min.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("clearandbrilliant.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Clearandbrilliant-HCP/css/bootstrap.min.css, /Assets/Clearandbrilliant-HCP/css/main.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Clearandbrilliant-DTC/css/bootstrap.min.css, /Assets/Clearandbrilliant-DTC/css/main.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("orapharmaosixx.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Ossixusa/css/vendor/font-awesome.min.css, /Assets/Ossixusa/css/vendor/bootstrap.min.css, /Assets/Ossixusa/css/main.min.css, /Assets/Ossixusa/css/floating.min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Orapharma/css/reset.css, /Assets/Orapharma/css/bootstrap.min.css, /Assets/Orapharma/css/main.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("fraxel.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Fraxel-HCP/css/bootstrap.min.css, /Assets/Fraxel-HCP/css/skin-min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Fraxel-HCP/css/bootstrap.min.css, /Assets/Fraxel-DTC/css/skin.css");
                            }

                            else if (SiteDefinition.Current.Name.ToLower().Equals("thermage.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Thermage-HCP/css/bootstrap.min.css, /Assets/Thermage-HCP/css/skin.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Thermage-DTC/css/bootstrap.min.css, /Assets/Thermage-DTC/css/skin.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("trulance.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Trulance-HCP/css/bootstrap.min.css, /Assets/Trulance-HCP/css/reset.css, /Assets/Trulance-HCP/css/main.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Trulance-DTC/css/vendor/font-awesome.min.css, /Assets/Trulance-DTC/css/vendor/bootstrap.min.css, /Assets/Trulance-DTC/css/main.min.css, /Assets/Trulance-DTC/css/custom.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("uceris.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Uceris-HCP/css/global.css, /Assets/Uceris-HCP/css/modal.css, /Assets/Uceris-HCP/css/hcp.css, /Assets/Uceris-HCP/css/footer.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Uceris-DTC/css/global.css, /Assets/Uceris-DTC/css/modal.css, /Assets/Uceris-DTC/css/patient.css, /Assets/Uceris-DTC/css/footer.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("ucerisfoam.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Ucerisfoam-HCP/css/main-min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Ucerisfoam-DTC/css/main-min.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("vaser.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Vaser-HCP/css/bootstrap.min.css, /Assets/Vaser-HCP/css/skin.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Vaser-DTC/css/bootstrap.min.css, /Assets/Vaser-DTC/css/skin.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("xerese.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xerese-HCP/css/bootstrap.min.css, /Assets/Xerese-HCP/css/main.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Xerese-DTC/css/_pages.css, /Assets/Xerese-DTC/css/bootstrap.min.css, /Assets/Xerese-DTC/css/main.css");
                            }

                            else if (SiteDefinition.Current.Name.ToLower().Equals("retinamicro.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Retinamicro-HCP/css/bootstrap.min.css, /Assets/Retinamicro-HCP/css/main.css, /Assets/Retinamicro-HCP/css/modal.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Retinamicro-DTC/css/bootstrap.min.css, /Assets/Retinamicro-DTC/css/main.css, /Assets/Retinamicro-DTC/css/modal.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("onexton.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Onexton-HCP/css/main.css, /Assets/Onexton-HCP/css/slider.css, /Assets/Onexton-HCP/css/floatingISI.css, /Assets/Onexton-HCP/css/modal.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Onexton-DTC/css/main.css, /Assets/Onexton-DTC/css/home.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("siliq.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Siliq-HCP/css/reset-min.css, /Assets/Siliq-HCP/css/main.css, /Assets/Siliq-HCP/css/twentytwenty-min.css, /Assets/Siliq-HCP/css/slick-min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Siliq-DTC/css/reset-min.css, /Assets/Siliq-DTC/css/main-min.css, /Assets/Siliq-DTC/css/twentytwenty-min.css, /Assets/Siliq-DTC/css/slick-min.css, /Assets/Siliq-DTC/css/animate.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("jubliarx.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Jubliarx-HCP/css/skin.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Jubliarx-DTC/css/default.css, /Assets/Jubliarx-DTC/css/skin.css, /Assets/Jubliarx-DTC/css/portals.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("gastrohubapp.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Gastrohub-HE/css/main.min.css, /Assets/Gastrohub-HE/css/slick.css, /Assets/Gastrohub-HE/css/reset.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Gastrohub-IBSD/css/main.min.css, /Assets/Gastrohub-IBSD/css/slick.css, /Assets/Gastrohub-IBSD/css/reset.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("duobrii.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Duobrii-HCP/css/main.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Duobrii-DTC/css/main.css, /Assets/Duobrii-DTC/css/custom.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("cycloset.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Cycloset-HCP/css/main.min.css, /Assets/Cycloset-HCP/css/modal.min.css, /Assets/Cycloset-HCP/css/font-awesome.min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Cycloset-DTC/css/main.css, /Assets/Cycloset-DTC/css/modal.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("bryhali.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Bryhali-HCP/css/main.min.css, /Assets/Bryhali-HCP/css/home.min.css, /Assets/Bryhali-HCP/css/skin.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Bryhali-DTC/css/main.min.css, /Assets/Bryhali-DTC/css/home.min.css, /Assets/Bryhali-DTC/css/skin.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("aplenzin.com"))
                            {
                                if (type == "HCP")
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Aplenzin-HCP/css/global.css, /Assets/Aplenzin-HCP/css/bootstrap.min.css");
                                else
                                    dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Aplenzin-DTC/css/global.css, /Assets/Aplenzin-DTC/css/bootstrap.min.css");
                            }
                            else if (SiteDefinition.Current.Name.ToLower().Equals("hcp.myorafit.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/OraFit-HCP/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("myorafit.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/OraFit-DTC/css/global.css");

                            else if (SiteDefinition.Current.Name.ToLower().Equals("moviprepsalix.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Moviprepsalix/css/content.css, /Assets/Moviprepsalix/css/nav.css, /Assets/Moviprepsalix/css/patient.css, /Assets/Moviprepsalix/css/screen.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("orapharma patents"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/OraPharmaPatents/css/style.css, /Assets/OraPharmaPatents/css/datatable.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("ortho-dermatologics patents"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/OrthoPatents/css/style.css, /Assets/OrthoPatents/css/datatable.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("salix patents"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/SalixPatents/css/style.css, /Assets/SalixPatents/css/datatable.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("confrontconstipation.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Confrontconstipation/css/style-mkto.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("arestinprofessional.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Arestinprofessional/css/skin.css, /Assets/Arestinprofessional/css/portal.css, /Assets/Arestinprofessional/css/container.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("aspirehigherscholarships.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Aspirehigherscholarships/css/MyFontsWebfontsKit.css, /Assets/Aspirehigherscholarships/css/fonts.css, /Assets/Aspirehigherscholarships/css/style.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("clindagel.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Clindagel-DTC/css/reset.css, /Assets/Clindagel-DTC/css/main.css, /Assets/Clindagel-DTC/css/responsive.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("cuprimine.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Cuprimine/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("demser.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Demser/css/default.css, /Assets/Demser/css/skin.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("diastat.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Diastat/css/style.css, /Assets/Diastat/css/jquery-ui.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("enhanceyoursrp.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Enhanceyoursrp/css/main.css, /Assets/Enhanceyoursrp/css/magnific-popup.min.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("guthealthnow.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Guthealthnow/css/main.css, /Assets/Guthealthnow/css/reset-min.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("healthiertoes.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Healthiertoes/css/main.css, /Assets/Healthiertoes/css/main.min.css, /Assets/Healthiertoes/css/reset.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("hesback.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Hesback/css/foundation.css, /Assets/Hesback/css/app.css, /Assets/Hesback/css/defaultFooter768Breakpoint.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("ibsdtakesguts.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/IBSDtakesguts/css/main.min.css, /Assets/IBSDtakesguts/css/reset.css");

                            else if (SiteDefinition.Current.Name.ToLower().Equals("neutrasal.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Neutrasal-DTC/Resources/css/base-1.css, /Assets/Neutrasal-DTC/Resources/css/base.css, /Assets/Neutrasal-DTC/Resources/css/BBstyles.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("neutrasalprofessional.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Neutrasalprofessional/css/base-1.css, /Assets/Neutrasalprofessional/css/base.css, /Assets/Neutrasalprofessional/css/BBstyles.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("ortho-dermatologics.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/ortho-dermatologics/css/fonts.css, /Assets/ortho-dermatologics/css/global.css, /Assets/ortho-dermatologics/css/style.css, /Assets/ortho-dermatologics/css/slick.css, /Assets/ortho-dermatologics/css/carousel.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("orthorxaccess.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/orthorxaccess-DTC/css/main.css, /Assets/orthorxaccess-DTC/css/reset.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("perioeducationusa.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Perioeducationusa/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("periobacktobasics.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Back-to-Basics/css/MarketoDarkCSS.css, /Assets/Back-to-Basics/css/global.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("relistor.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Relistor-DTC/css/Relistor-DTC.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("relistorhcp.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Relistor-HCP/css/main.css, /Assets/Relistor-HCP/css/reset-min.css");

                            else if (SiteDefinition.Current.Name.ToLower().Equals("pedinol.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Pedinol/css/style.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("relistor.ca"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Relistor/css/styles.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("salix.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Salix/css/bootstrap.min.css, /Assets/Salix/css/main.css, /Assets/Salix/css/tablet.css, /Assets/Salix/css/mobile.css, /Assets/Salix/css/slider.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("solta.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Solta/css/bootstrap.min.css, /Assets/Solta/css/skin.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("soltapatents.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Soltapatents/css/bootstrap.min.css, /Assets/Soltapatents/css/style.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("syprine.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Syprine/css/bootstrap.min.css, /Assets/Syprine/css/reset.css, /Assets/Syprine/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("targretin.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Targretin-DTC/css/bootstrap.min.css, /Assets/Targretin-DTC/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("targretin-hcp"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Targretin-HCP/css/bootstrap.min.css, /Assets/Targretin-HCP/css/custom.min.css, /Assets/Targretin-HCP/css/aos.min.css, /Assets/Targretin-HCP/css/slick.min.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("thegutmicrobiome.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Thegutmicrobiome/css/bootstrap.min.css, /Assets/Thegutmicrobiome/css/reset.css, /Assets/Thegutmicrobiome/css/animate.css, /Assets/Thegutmicrobiome/css/main.css, /Assets/Thegutmicrobiome/css/hover.css, /Assets/Thegutmicrobiome/css/easy-responsive-tabs.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("understandinghe.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Understandinghe/css/reset.css, /Assets/Understandinghe/css/font-awesome.min.css");

                            else if (SiteDefinition.Current.Name.ToLower().Equals("understandingoic.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Understandingoic/css/bootstrap.min.css, /Assets/Understandingoic/css/reset.css, /Assets/Understandingoic/css/main.min.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("unseenepisodes.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Unseenepisodes/css/style.css, /Assets/Unseenepisodes/css/slider.css, /Assets/Unseenepisodes/css/aos.css, /Assets/Unseenepisodes/css/slick.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("valeantcoverageplus.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Valeantcoverageplus/css/bootstrap.min.css, /Assets/Valeantcoverageplus/css/custom_style.css, /Assets/Valeantcoverageplus/css/print.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("wdrxaccess.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/WDRxAccess/css/bootstrap.min.css, /Assets/WDRxAccess/css/main.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("wellbutrinxl"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Wellbutrinxl/css/vendor/bootstrap.min.css, /Assets/Wellbutrinxl/css/main.css, /Assets/Wellbutrinxl/css/home.css, /Assets/Wellbutrinxl/css/inner.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("renovarx.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Renova/css/vendor/bootstrap.min.css, /Assets/Renova/css/fonts.css, /Assets/Renova/css/global.css");
                            else if (SiteDefinition.Current.Name.ToLower().Equals("retin-a.com"))
                                dictionary.Add("content_css", "/Static/css/editor.css, /Assets/Retin-A/css/vendor/bootstrap.min.css, /Assets/Retin-A/css/fonts.css, /Assets/Retin-A/css/global.css");
                            else
                                dictionary.Add("content_css", "/Static/css/editor.css, /Templates/Grid/bootstrap5.min.css");

                        }

                    }
                }

            }

            catch (Exception ex)
            {
                //throw ;
            }

        }
    }
}
