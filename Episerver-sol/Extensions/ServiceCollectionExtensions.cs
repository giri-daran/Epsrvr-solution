using EPiServer.Cms.TinyMce.Core;
using EPiServer.Web;
using EpiserverBH.Business;
using EpiserverBH.Business.Channels;
using EpiserverBH.Business.Rendering;
using EpiserverBH.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace EpiserverBH.Extensions
{
    public static class ServiceCollectionExtensions
    {

        
        public static IServiceCollection AddAlloy(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()));
            
            
            services.Configure<DisplayOptions>(displayOption =>
            {
                displayOption.Add("full", "/displayoptions/full", Globals.ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full");
                displayOption.Add("wide", "/displayoptions/wide", Globals.ContentAreaTags.WideWidth, string.Empty, "epi-icon__layout--wide");
                displayOption.Add("half", "/displayoptions/half", Globals.ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half");
                displayOption.Add("narrow", "/displayoptions/narrow", Globals.ContentAreaTags.NarrowWidth, string.Empty, "epi-icon__layout--narrow");
            });

            services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());

            services.AddDisplayResolutions();
            services.AddDetection();

            return services;
        }

        private static void AddDisplayResolutions(this IServiceCollection services)
        {
            services.AddSingleton<StandardResolution>();
            services.AddSingleton<IpadHorizontalResolution>();
            services.AddSingleton<IphoneVerticalResolution>();
            services.AddSingleton<AndroidVerticalResolution>();
            services.AddSingleton<AlloyContentAreaItemRenderer>();
        }
        public static IServiceCollection AddTinyMceConfiguration(this IServiceCollection services)
        {
          
            services.Configure<TinyMceConfiguration>(config =>
            {
                // Add content CSS to the default settings.
                config.Default()
                    .ContentCss("/Static/css/editor.css")
                    .AddSetting("cleanup_on_startup", "false")
                     .AddSetting("cleanup", "false")
                      .AddSetting("image_caption", true)
.AddSetting("image_advtab", true)
.AddSetting("verify_html", false)
//.AddSetting("forced_root_block", false)
                    .AddSetting("valid_children", "+body[style],+div[span],+a[div|i],+a[div|p|h1|h2|h3|h4|h5|h6|ul|ol|li|strong|em|i|img|span],+span[*],+p[*]") // Allow body with style property, div with span inner elements and anchor with div or i inner elements

                    //.AddSetting("allow_html_in_named_anchor", "true") // If allows or not html in a named anchor
                    //.AddSetting("allow_script_urls", "true") // If it allows scripts urls, this is needed to enable js content


                      .AddSetting("extended_valid_elements", // List of valid elements in the editor, this includes scritps (for js), iframe, and several others. What you send inside the [] are the allowed inner elements for that tag 
                          "script[language|type|src],iframe[src|alt|title|width|height|align|name|style],picture,source[srcset|media],a[id|href|target|onclick|class],span[*],div[*],b,u,i,em,svg[*],defs[*],pattern[*],desc[*],metadata[*],g[*],mask[*],path[*],line[*],marker[*],rect[*],circle[*],ellipse[*],polygon[*],polyline[*],linearGradient[*],radialGradient[*],stop[*],image[*],view[*],text[*],textPath[*],title[*],tspan[*],glyph[*],symbol[*],switch[*],use[*],a[div|p|h1|h2|h3|h4|h5|h6|ul|ol|li|strong|em|i|img|span|class|style|href],h3[div|p|ul|ol|li|strong|em|i|img|a|span|class|style],span[class|style],span[*],a[*]")
                      .AddPlugin(
                        "epi-link epi-image-editor epi-dnd-processor epi-personalized-content preview searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap pagebreak nonbreaking anchor insertdatetime advlist lists  autosave save pagebreak nonbreaking help charmap emoticons" +
                        " help code") // A lot of epi features, it is not related to enable js or iframes
                    .Toolbar(
                        "code |formatselect styleselect | bold italic underline strikethrough subscript superscript | epi-personalized-content | removeformat | fullscreen preview | fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | preview save | insertfile image media template link anchor | ltr rtl"
                        , "") // Three parameter for first second and third row
                    .Menubar("edit insert view tools") // Menu bar elements

                    .BlockFormats("Paragraph=p;Span=s;Heading 2=h2;Heading 3=h3;Heading 4=h4;Heading 5=h5;Heading 6=h6");

                // This will clone the default settings object and extend it by
                // limiting the block formats for the MainBody property of an ArticlePage.


                // Passing a second argument to For<> will clone the given settings object
                // instead of the default one and extend it with some basic toolbar commands.
                config.For<EditorialBlock>(t => t.MainBody, config.Empty())
                    .AddEpiserverSupport()
                    // .DisableMenubar()
                    .Toolbar("bold italic underline strikethrough");
                var templatePath = $"/Templates/bootstrap/";
                config.For<RawHTML>(t => t.HTMLContent)

                .Menubar("file edit insert table  help").RemoveMenuItems("codesample")
                      .RemoveMenuItems("code")
                      .RemoveMenuItems("insertdatetime")
                      .RemoveMenuItems("toc")
                       .RemoveMenuItems("toc")
                                         .EnableImageTools()
                                          .AddEpiserverSupport()
                                           .AddSetting("image_caption", true)
.AddSetting("image_advtab", true)
.AddSetting("verify_html", false)
.AddSetting("forced_root_block", false)
.AddSetting("visualblocks", true)
                    .AddSetting("cleanup_on_startup", "false")
                     .AddSetting("cleanup", "false")

                                           .AddExternalPlugin("bootstrapplugin", "/Templates/ApplyClass/plugin.min.js")
                                           .AddExternalPlugin("Grid", "/Templates/Grid/plugin.js")
                .AddPlugin("epi-link").AddPlugin("epi-image-editor").AddPlugin("epi-dnd-processor")
                .AddPlugin("epi-personalized-content").AddPlugin("preview")
                .AddPlugin("autolink").AddPlugin("visualblocks").AddPlugin("visualchars")
                .AddPlugin("fullscreen").AddPlugin("image").AddPlugin("link").AddPlugin("media").AddPlugin("template").AddPlugin("table").AddPlugin("charmap").AddPlugin("tabfocus")
                .AddPlugin("pagebreak").AddPlugin("nonbreaking").AddPlugin("anchor").AddPlugin("advlist").AddPlugin("lists")
                .AddPlugin("wordcount").AddPlugin("imagetools").AddPlugin("help").AddPlugin("code")
                .AddSetting("extended_valid_elements", "span[*],p[*],a[*],iframe[src|alt|title|width|height|align|name],picture,source[srcset|media],script[language|type|src],iframe[src|alt|title|width|height|align|name|style],picture,source[srcset|media],div[*],section[*],article[*],aside[*],blockquote[*],hgroup[*],figure[*]")
                .AddSetting("table_default_styles", new { })
                    .AddSetting("table_default_attributes", new { })
                      .AddSetting("image_class_list", new[] { new { title = "None", value = "" }, new { title = "img-fluid", value = "img-fluid" }, new { title = "img-thumbnail", value = "img-thumbnail" }, new { title = "rounded float-start", value = "rounded float-start" }, new { title = "rounded float-end", value = "rounded float-end" }, new { title = "rounded mx-auto d-block", value = "rounded mx-auto d-block" }, new { title = "rounded", value = "rounded" } })
                       .AddSetting("table_class_list", new[] { new { title = "None", value = "" }, new { title = "No Borders", value = "table_no_borders" }, new { title = "table", value = "table" }, new { title = "table-dark", value = "table table-dark" }, new { title = "table-striped", value = "table table-striped" }, new { title = "table table-striped table-dark", value = "table table-striped table-dark" }, new { title = "table table-hover", value = "table table-bordered" }, new { title = "table table-bordered table-dark", value = "table table-bordered table-dark" }, new { title = "table table-hover", value = "table table-hover" }, new { title = "table table-hover table-dark", value = "table table-hover table-dark" }, new { title = "table table-sm", value = "table table-sm" }, new { title = "Custom CSS 1", value = "tblCustom1" }, new { title = "Custom CSS 2", value = "tblCustom2" }, new { title = "Custom CSS 3", value = "tblCustom3" }, new { title = "Custom CSS 4", value = "tblCustom4" }, new { title = "Custom CSS 5", value = "tblCustom5" }, new { title = "Custom CSS 6", value = "tblCustom6" } })
                         .AddSetting("link_class_list", new[] { new { title = "None", value = "" }, new { title = "btn btn-primary", value = "btn btn-primary" }, new { title = "btn btn-secondary", value = "btn btn-secondary" }, new { title = "btn btn-success", value = "btn btn-success" }, new { title = "btn btn-danger", value = "btn btn-danger" }, new { title = "btn btn-warning", value = "btn btn-warning" }, new { title = "btn btn-info", value = "btn btn-info" }, new { title = "btn btn-light", value = "btn btn-light" }, new { title = "btn btn-dark", value = "btn btn-dark" }, new { title = "btn btn-link", value = "btn btn-link" }, new { title = "btn btn-outline-primary", value = "btn btn-outline-primary" }, new { title = "btn btn-outline-secondary", value = "btn btn-outline-secondary" }, new { title = "btn btn-outline-success", value = "btn btn-outline-success" }, new { title = "btn btn-outline-danger", value = "btn btn-outline-danger" }, new { title = "btn btn-outline-warning", value = "btn btn-outline-warning" }, new { title = "btn btn-outline-info", value = "btn btn-outline-info" }, new { title = "btn btn-outline-light", value = "btn btn-outline-light" }, new { title = "btn btn-outline-dark", value = "btn btn-outline-dark" }, new { title = "btn btn-primary btn-lg btn-block", value = "btn btn-primary btn-lg btn-block" }, new { title = "btn btn-secondary btn-lg btn-block", value = "btn btn-secondary btn-lg btn-block" }, new { title = "Custom Button 1", value = "Custom Button 1" }, new { title = "Custom Button 2", value = "Custom Button 2" }, new { title = "Custom Button 3", value = "Custom Button 3" }, new { title = "Custom Button 4", value = "Custom Button 4" }, new { title = "Custom Button 5", value = "Custom Button 5" } })

                .AddSetting("inline_styles", true)
               .AddSetting("verify_html", true)
                  .AddSetting("templates", new[]
                    {
                       new {title= "Bootstrap Basic Table", url=$"{templatePath}/Basic_Table.html", description="Basic Bootstrap Table"},
                        new {title= "Bootstrap Responsive Table", url=$"{templatePath}/Responsive_Table.html", description="Bootstrap Responsive Table"},
                        new {title= "Bootstrap Flex", url=$"{templatePath}/Flex.html", description="Bootstrap Flex layouts"},
                          new {title= "Carousel: With Caption", url=$"{templatePath}/Carousel_WithCaption.html", description="Carousel: With Caption"},
                            new {title= "Carousel: With Control", url=$"{templatePath}/Carousel_WithControl.html", description="Carousel: With Control"},
                              new {title= "Carousel: With Indicators", url=$"{templatePath}/Carousel_WithIndicators.html", description="Carousel: With Indicators"},
                               new {title= "Carousel: Without Control", url=$"{templatePath}/Carousel_WithoutControl.html", description="Carousel: Without Control"},


                        new {title= "Article Template: 2 Image Columns", url=$"{templatePath}/article_template_1.html", description="Template w/Side by side Images"},
                        new {title= "Article Template: 3 Image Columns", url=$"{templatePath}/article_template_2.html", description="Template with three image colums"},
                        new {title= "Image Layout: 2 Columns", url=$"{templatePath}/image_template_2_col.html", description="Template with two images"},
                        new {title= "Image Layout: 3 Columns", url=$"{templatePath}/image_template_3_col.html", description="Template with three images"},
                        new {title= "Image Layout with Caption: 1 Column", url=$"{templatePath}/image_template_caption.html", description="Template with single column image and caption"},
                        new {title= "Image Layout with Caption: 2 Columns", url=$"{templatePath}/image_template_caption_2.html", description="Template with two columns: image and caption"},
                        new {title= "Image Layout with Caption: 3 Columns", url=$"{templatePath}/image_template_caption_3.html", description="Template with three columns image and caption"},
                        new {title= "General: 2 Column Layout: (50%/50%)", url=$"{templatePath}/two_columns.html", description="Template with two columns"},
                        new {title= "General: 2 Column Layout: Image Left / Text Right (33%/66%) - 1 Row", url=$"{templatePath}/two_columns_image_left.html", description="Template with two columns. Image on the left."},
                        new {title= "General: 2 Column Layout: Text Left / Image Right (66%/33%) - 1 Row", url=$"{templatePath}/two_columns_image_right.html", description="Template with two columns. Image on the right."},
                        new {title= "General: 3 Column Layout", url=$"{templatePath}/three_columns.html", description="Template with three columns"},
                        new {title= "Accordion", url=$"{templatePath}/accordion.html", description="Single accordion template prefilled with some terms and conditions"},
                        new {title= "Accordion Flush", url=$"{templatePath}/accordion_flush.html", description="Accordion Flush template prefilled with some terms and conditions"},
                          new {title= "Accordion Panel Stay Open", url=$"{templatePath}/accordionPanelsStayOpen.html", description="Accordion Panel Stay Open template prefilled with some terms and conditions"},

                    })
                   .AddSetting("end_container_on_empty_block", true)
                   .StyleFormats(new
                   {
                       title = "Headings",
                       items = new[]
                            {
                                new { title = "Heading 1", block = "h1" },
                                new { title = "Heading 2", block = "h2" },
                                new { title = "Heading 3", block = "h3" },
                                new { title = "Heading 4", block = "h4" },
                                new { title = "Heading 5", block = "h5" },
                                new { title = "Heading 6", block = "h6" }
                            }
                   }, new
                   {
                       title = "Containers",
                       items = new[]
                      {
                        new {title = "section", block = "section", wrapper="true",merge_siblings="false"},
                         new {title = "article", block = "article", wrapper="true",merge_siblings="false"},
                          new {title = "blockquote", block = "blockquote", wrapper="true",merge_siblings="false"},
                           new {title = "hgroup", block = "hgroup", wrapper="true",merge_siblings="false"},
                            new {title = "aside", block = "aside", wrapper="true",merge_siblings="false"},
                              new {title = "figure", block = "figure", wrapper="true",merge_siblings="false"},
                         }
                   },
                        // Blocks (applies HTML tags)
                        new
                        {
                            title = "Blocks",
                            items = new[]
                            {
                                new { title = "Paragraph", block = "p" },
                                new { title = "Blockquote", block = "blockquote" },
                                new { title = "Div", block = "div" },
                                new { title = "Pre", block = "pre" }
                            }
                        },
                        // Text alignment (applies to all block formats)
                        new
                        {
                            title = "Alignment",
                            items = new[]
                            {
                                new { title = "Left", selector = "p,h1,h2,h3,h4,h5,h6", classes = "leftAlign" },
                                new { title = "Center", selector = "p,h1,h2,h3,h4,h5,h6", classes = "centerAlign" },
                                new { title = "Right", selector = "p,h1,h2,h3,h4,h5,h6", classes = "rightAlign" },
                                new { title = "Justify", selector = "p,h1,h2,h3,h4,h5,h6", classes = "justifyAlign" }
                            }
                        })
                   .AddSetting("fullscreen_native", true)
                  .AddSetting("width", "960px")
                  .AddSetting("height", "580px")
                    //.ContentCss("/Static/css/editor.css", "/Assets/BauschSurgical/css/styles.css")
                    .AddPlugin($"template")

                .Toolbar("styleselect formatselect | bold italic strikethrough forecolor backcolor subscript superscript" +
                    "| code | epi-link | alignleft aligncenter alignright alignjustify " + "| borderstyle " +
                    "| numlist bullist outdent indent | removeformat | epi-image-editor | epi-personalized-content | insertfile image media  anchor | cut copy paste preview save | fullscreen | template | grid_insert | table | bootstrapplugin ") // Pipes represent separators in the editor UI
                .BlockFormats("Paragraph=p;Span=s;Header 1=h1;Header 2=h2;Header 3=h3; Header 4=h4;Header 5=h5;Header 6=h6");



            });
            return services;
        }
    }
}