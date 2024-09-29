using EPiServer.Web.Mvc;
using EpiserverBH.Helpers;
using EpiserverBH.Models.Blocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;

namespace EpiserverBH.Controllers
{
    public class PhysicianFinderElementBlockController : BlockComponent<PhysicianFinderElementBlock>
    {
        private static PhysicianFinderElementBlock _block;
        private static IHttpContextAccessor _IHttpContextAccessor;

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private string GetConnectionString()
        {

            return SQLGetConnection.GetConnection().ConnectionString;
        }
        public PhysicianFinderElementBlockController(IWebHostEnvironment hostingEnvironment, IConfiguration iconfig, IHttpContextAccessor _iHttpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = iconfig;
            _IHttpContextAccessor = _iHttpContextAccessor;
        }

        protected override IViewComponentResult InvokeComponent(PhysicianFinderElementBlock currentContent)
        {
            _block = currentContent;

            if (currentContent != null)
            {
                var assetFolder = string.Format("Assets/PhysicianFinder/Templates/{0}/", currentContent.TemplateName);
                var assetsPath = Path.Combine(_hostingEnvironment.WebRootPath, assetFolder);

                var viewFilePath = System.IO.Path.Combine(assetsPath, "View.htm");

                if (System.IO.File.Exists(viewFilePath))
                {
                    ViewBag.HtmlContent = System.IO.File.ReadAllText(viewFilePath);
                }
                else
                {
                    ViewBag.HtmlContent = "";
                }
            }

            return View(currentContent);
        }

        [HttpPost]
        public IActionResult Locate(string zip, int radius, int ModuleID)
        {
            string ConStringnew = GetConnectionString();
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];
            string _TableName = _block.TableName.ToString();
            string _Key = _block.MapGeoCodeKey.ToString();
            int _ModuleID = Convert.ToInt32(ModuleID);
            //string _TableName = TableName.ToString();
            Locator_results locator_results = new Locator_results();
         
          
                locator_results.zip = zip;
                locator_results.radius = radius;
                if (string.IsNullOrEmpty(_TableName))
                {
                    //do nothing
                }
                else
                {
                    if (method == "POST")
                    {
                        if (requestedWith == "XMLHttpRequest")
                        {
                           
                            try
                            {

                                List<Practice_location> locresult = new List<Practice_location>();

                                using (SqlConnection connection = new SqlConnection(ConStringnew))
                                {
                                    string sql = string.Format("SELECT bp.[id],bp.[name],[address_1] ,[address_2] ,[city] ,[state_id] ,[zip] ,[phone] ,[website] ,CAST([latitude] as float) as lat,CAST([longitude] as float) as lng  ,[category_tag] ,[country],[user_1_text],[user_2_text],[user_3_text],[user_4_text]  ,[user_5_text],[user_6_text] ,[user_7_text],[user_8_text],[user_9_text],[user_10_text],[user_11_text] ,[user_12_text] ,[user_13_text] ,[user_14_text] ,[user_15_text] ,[module_id] ,sl.name as state  FROM {0} bp inner join GeoSprawl_StatesList sl on bp.state_id=sl.id and bp.module_id=@ModuleID", _TableName);
                                    connection.Open();
                                    var cmd = connection.CreateCommand();

                                    SqlParameter cultureParam1 = cmd.CreateParameter();
                                    cultureParam1.ParameterName = "@ModuleID";
                                    cultureParam1.SqlDbType = SqlDbType.Int;
                                    cultureParam1.Value = _ModuleID;
                                    cmd.Parameters.Add(cultureParam1);

                                    cmd.CommandText = sql;
                                    cmd.CommandType = System.Data.CommandType.Text;

                                    var reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        var loc = new Practice_location();

                                        loc.name = Convert.ToString(reader["name"]);
                                        loc.address_1 = Convert.ToString(reader["address_1"]);
                                        loc.address_2 = Convert.ToString(reader["address_2"]);
                                        loc.city = Convert.ToString(reader["city"]);
                                        loc.state = Convert.ToString(reader["state"]);
                                        loc.zip = Convert.ToString(reader["zip"]);
                                        loc.phone = Convert.ToString(reader["phone"]);
                                        loc.website = Convert.ToString(reader["website"]);

                                        loc.lat = Convert.ToDouble(reader["lat"]);
                                        loc.lng = Convert.ToDouble(reader["lng"]);

                                        loc.category_tag = Convert.ToString(reader["category_tag"]);
                                        loc.country = Convert.ToString(reader["country"]);
                                        loc.user_1_text = Convert.ToString(reader["user_1_text"]);
                                        loc.user_2_text = Convert.ToString(reader["user_2_text"]);
                                        loc.user_3_text = Convert.ToString(reader["user_3_text"]);
                                        loc.user_4_text = Convert.ToString(reader["user_4_text"]);
                                        loc.user_5_text = Convert.ToString(reader["user_5_text"]);
                                        loc.user_6_text = Convert.ToString(reader["user_6_text"]);
                                        loc.user_7_text = Convert.ToString(reader["user_7_text"]);
                                        loc.user_8_text = Convert.ToString(reader["user_8_text"]);
                                        loc.user_9_text = Convert.ToString(reader["user_9_text"]);
                                        loc.user_10_text = Convert.ToString(reader["user_10_text"]);
                                        loc.user_11_text = Convert.ToString(reader["user_11_text"]);
                                        loc.user_12_text = Convert.ToString(reader["user_12_text"]);
                                        loc.user_13_text = Convert.ToString(reader["user_13_text"]);
                                        loc.user_14_text = Convert.ToString(reader["user_14_text"]);
                                        loc.user_15_text = Convert.ToString(reader["user_15_text"]);

                                        loc.module_id = Convert.ToInt32(reader["module_id"]);

                                        locresult.Add(loc);
                                    }
                                }

                                //get coordinates of zip code
                                string address = zip + " ,USA";
                                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false&key={1}", address, _Key);


                                WebRequest request = WebRequest.Create(requestUri);
                                request.ContentType = "application/json; charset=utf-8";
                                WebResponse response = request.GetResponse();
                                if (((System.Net.HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                                {
                                    var webResponseStream = response.GetResponseStream();
                                    double lat = 0, lng = 0;
                                    if (webResponseStream != null && webResponseStream != Stream.Null)
                                    {
                                        using (Stream responseStream = response.GetResponseStream())
                                        {
                                            StreamReader r = new StreamReader(responseStream);
                                            string line = "";
                                        if ((line = r.ReadToEnd()) != null)
                                        {
                                            if (!line.Contains("ZERO_RESULTS"))
                                            {
                                                Geo_results geo = JsonConvert.DeserializeObject<Geo_results>(line);
                                                lat = geo.results[0].geometry.location.lat;
                                                lng = geo.results[0].geometry.location.lng;
                                                locator_results.lat = lat;
                                                locator_results.lng = lng;
                                            }

                                        }

                                    }

                                        if (lat != 0 && lng != 0)
                                        {

                                            var results = from l in locresult
                                                          select new Practice_result
                                                          {
                                                              name = l.name,
                                                              address_1 = l.address_1,
                                                              address_2 = l.address_2,
                                                              practice = l.practice,
                                                              address = l.address,
                                                              city = l.city,
                                                              state = l.state,
                                                              zip = l.zip,
                                                              phone = l.phone,
                                                              website = l.website,
                                                              lat = l.lat,
                                                              lng = l.lng,
                                                              category_tag = l.category_tag,
                                                              country = l.country,
                                                              user_1_text = l.user_1_text,
                                                              user_2_text = l.user_2_text,
                                                              user_3_text = l.user_3_text,
                                                              user_4_text = l.user_4_text,
                                                              user_5_text = l.user_5_text,
                                                              user_6_text = l.user_6_text,
                                                              user_7_text = l.user_7_text,
                                                              user_8_text = l.user_8_text,
                                                              user_9_text = l.user_9_text,
                                                              user_10_text = l.user_10_text,
                                                              user_11_text = l.user_11_text,
                                                              user_12_text = l.user_12_text,
                                                              user_13_text = l.user_13_text,
                                                              user_14_text = l.user_14_text,
                                                              user_15_text = l.user_15_text,
                                                              module_id = l.module_id,
                                                              distance = Haversine.calculate(lat, lng, l.lat, l.lng)
                                                          };

                                            if (radius != 0)
                                            {
                                                results = results.Where(l => l.distance <= radius);
                                            }
                                            results = results.OrderBy(l => l.distance).ThenBy(n => n.name);
                                            if (radius == 0)
                                            {
                                                results = results.Take(30);
                                            }

                                            locator_results.locations = results.ToArray();
                                        }


                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                string site = string.Empty;
                                
                                    site = "Physician Locator";
                                MailService.SendAlert(_configuration, "no-reply@bauschhealth.com", site + " - Physician Locator Exception", ex.StackTrace, "websitealerts@bauschhealth.com");

                                //locator_results.error = "Not a Relevant Request";
                            }
                        }
                    }
                }
           
            ContentResult result = new ContentResult();
            result.Content = Convert.ToString(locator_results);
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocateArestin(string zip)
        {
            string ConStringnew = GetConnectionString();
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];
            ArestinLocator_results locator_results = new ArestinLocator_results();

            var radius = 10;

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    string _TableName = _block.TableName.ToString();
                    string _Key = _block.MapGeoCodeKey.ToString();

                    if (!string.IsNullOrEmpty(_TableName))
                    {
                        if (!string.IsNullOrEmpty(_Key))
                        {
                            try
                            {
                                List<Arestin_location> locresult = new List<Arestin_location>();

                                using (SqlConnection connection = new SqlConnection(ConStringnew))
                                {
                                    string sql = string.Format("SELECT Id, Zipcode, Name, Address1, City, State, Country, Phone, Email, Latitude, Longitude FROM {0}", _TableName);
                                    connection.Open();
                                    var cmd = connection.CreateCommand();
                                    cmd.CommandText = sql;
                                    cmd.CommandType = System.Data.CommandType.Text;

                                    var reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        var loc = new Arestin_location();

                                        loc.Name = Convert.ToString(reader["Name"]);
                                        loc.Address1 = Convert.ToString(reader["Address1"]);
                                        loc.City = Convert.ToString(reader["City"]);
                                        loc.State = Convert.ToString(reader["State"]);
                                        loc.Zipcode = Convert.ToString(reader["Zipcode"]);
                                        loc.Phone = Convert.ToString(reader["Phone"]);
                                        loc.Latitude = Convert.ToDouble(reader["Latitude"]);
                                        loc.Longitude = Convert.ToDouble(reader["Longitude"]);
                                        loc.Country = Convert.ToString(reader["Country"]);
                                        loc.Email = Convert.ToString(reader["Email"]);

                                        locresult.Add(loc);
                                    }
                                }

                                //get coordinates of zip code
                                string address = zip + " ,USA";
                                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false&key={1}", address, _Key);

                                WebRequest request = WebRequest.Create(requestUri);
                                request.ContentType = "application/json; charset=utf-8";
                                WebResponse response = request.GetResponse();

                                if (((System.Net.HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                                {
                                    double lat = 0, lng = 0;
                                    using (Stream responseStream = response.GetResponseStream())
                                    {
                                        StreamReader r = new StreamReader(responseStream);
                                        string line = "";
                                        if ((line = r.ReadToEnd()) != null)
                                        {
                                            if (!line.Contains("ZERO_RESULTS"))
                                            {
                                                Geo_results geo = JsonConvert.DeserializeObject<Geo_results>(line);
                                                if (geo.results.Any())
                                                {
                                                    lat = geo.results[0].geometry.location.lat;
                                                    lng = geo.results[0].geometry.location.lng;
                                                }

                                                locator_results.Latitude = lat;
                                                locator_results.Longitude = lng;
                                            }
                                        }
                                    }

                                    var results = from l in locresult
                                                  select new Arestin_result
                                                  {
                                                      Name = l.Name,
                                                      Address1 = l.Address1,
                                                      Address2 = l.Address2,
                                                      City = l.City,
                                                      State = l.State,
                                                      Zipcode = l.Zipcode,
                                                      Phone = l.Phone,
                                                      Latitude = l.Latitude,
                                                      Longitude = l.Longitude,
                                                      Country = l.Country,
                                                      Email = l.Email,
                                                      Distance = Haversine.calculate(lat, lng, l.Latitude, l.Longitude)
                                                  };

                                    if (radius != 0)
                                    {
                                        results = results.Where(l => l.Distance <= radius);
                                    }
                                    results = results.OrderBy(l => l.Distance).ThenBy(n => n.Name);
                                    if (radius == 0)
                                    {
                                        results = results.Take(30);
                                    }

                                    locator_results.Locations = results.ToArray();
                                }
                            }
                            catch (Exception ex)
                            {
                                string site = "Arestin.com";
                                MailService.SendAlert(_configuration, "no-reply@bauschhealth.com", site + " Physician Locator Exception", ex.Message, "websitealerts@bauschhealth.com");
                                locator_results.Error = ex.Message;
                            }
                        }
                    }
                }
            }

            ContentResult result = new ContentResult();
            result.Content = Convert.ToString(JsonConvert.SerializeObject(locator_results, Formatting.Indented));
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocateAPI(string zip)
        {

            string ConStringnew = GetConnectionString();
            string method = _IHttpContextAccessor.HttpContext.Request.Method;
            string requestedWith = _IHttpContextAccessor.HttpContext.Request.Headers["X-Requested-With"];
            Locator_results locator_results = new Locator_results();

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    locator_results.radius = 10;
                    var radius = 10;
                    string _TableName = _block.TableName.ToString();
                    string _Key = _block.MapGeoCodeKey.ToString();

                    try
                    {

                        List<Practice_location> locresult = new List<Practice_location>();

                        using (SqlConnection connection = new SqlConnection(ConStringnew))
                        {
                            string sql = string.Format("SELECT bp.id,bp.name,address_1 ,address_2 ,[city] ,[state_id] ,[zip] ,[phone] ,[website] ,CAST([latitude] as float) as lat,CAST([longitude] as float) as lng  ,[category_tag] ,[country],[user_1_text],[user_2_text],[user_3_text],[user_4_text]  ,[user_5_text],[user_6_text] ,[user_7_text],[user_8_text],[user_9_text],[user_10_text],[user_11_text] ,[user_12_text] ,[user_13_text] ,[user_14_text] ,[user_15_text] ,[module_id] ,sl.name as state  FROM {0} bp inner join GeoSprawl_StatesList sl on bp.state_id=sl.id", _TableName);
                            connection.Open();
                            var cmd = connection.CreateCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;

                            var reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                var loc = new Practice_location();

                                loc.name = Convert.ToString(reader["name"]);
                                loc.address_1 = Convert.ToString(reader["address_1"]);
                                loc.address_2 = Convert.ToString(reader["address_2"]);
                                loc.city = Convert.ToString(reader["city"]);
                                loc.state = Convert.ToString(reader["state"]);
                                loc.zip = Convert.ToString(reader["zip"]);
                                loc.phone = Convert.ToString(reader["phone"]);
                                loc.website = Convert.ToString(reader["website"]);

                                loc.lat = Convert.ToDouble(reader["lat"]);
                                loc.lng = Convert.ToDouble(reader["lng"]);

                                loc.category_tag = Convert.ToString(reader["category_tag"]);
                                loc.country = Convert.ToString(reader["country"]);
                                loc.user_1_text = Convert.ToString(reader["user_1_text"]);
                                loc.user_2_text = Convert.ToString(reader["user_2_text"]);
                                loc.user_3_text = Convert.ToString(reader["user_3_text"]);
                                loc.user_4_text = Convert.ToString(reader["user_4_text"]);
                                loc.user_5_text = Convert.ToString(reader["user_5_text"]);
                                loc.user_6_text = Convert.ToString(reader["user_6_text"]);
                                loc.user_7_text = Convert.ToString(reader["user_7_text"]);
                                loc.user_8_text = Convert.ToString(reader["user_8_text"]);
                                loc.user_9_text = Convert.ToString(reader["user_9_text"]);
                                loc.user_10_text = Convert.ToString(reader["user_10_text"]);
                                loc.user_11_text = Convert.ToString(reader["user_11_text"]);
                                loc.user_12_text = Convert.ToString(reader["user_12_text"]);
                                loc.user_13_text = Convert.ToString(reader["user_13_text"]);
                                loc.user_14_text = Convert.ToString(reader["user_14_text"]);
                                loc.user_15_text = Convert.ToString(reader["user_15_text"]);

                                loc.module_id = Convert.ToInt32(reader["module_id"]);

                                locresult.Add(loc);
                            }
                        }

                        //get coordinates of zip code
                        string address = zip + " ,USA";
                        string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false&key={1}", address, _Key);

                        WebRequest request = WebRequest.Create(requestUri);
                        request.ContentType = "application/json; charset=utf-8";
                        WebResponse response = request.GetResponse();

                        double lat, lng;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader r = new StreamReader(responseStream);
                            Geo_results geo = JsonConvert.DeserializeObject<Geo_results>(r.ReadToEnd());
                            lat = geo.results[0].geometry.location.lat;
                            lng = geo.results[0].geometry.location.lng;
                            locator_results.lat = lat;
                            locator_results.lng = lng;
                        }

                        var results = from l in locresult
                                      select new Practice_result
                                      {
                                          name = l.name,
                                          address_1 = l.address_1,
                                          address_2 = l.address_2,
                                          practice = l.practice,
                                          address = l.address,
                                          city = l.city,
                                          state = l.state,
                                          zip = l.zip,
                                          phone = l.phone,
                                          website = l.website,
                                          lat = l.lat,
                                          lng = l.lng,
                                          category_tag = l.category_tag,
                                          country = l.country,
                                          user_1_text = l.user_1_text,
                                          user_2_text = l.user_2_text,
                                          user_3_text = l.user_3_text,
                                          user_4_text = l.user_4_text,
                                          user_5_text = l.user_5_text,
                                          user_6_text = l.user_6_text,
                                          user_7_text = l.user_7_text,
                                          user_8_text = l.user_8_text,
                                          user_9_text = l.user_9_text,
                                          user_10_text = l.user_10_text,
                                          user_11_text = l.user_11_text,
                                          user_12_text = l.user_12_text,
                                          user_13_text = l.user_13_text,
                                          user_14_text = l.user_14_text,
                                          user_15_text = l.user_15_text,
                                          module_id = l.module_id,
                                          distance = Haversine.calculate(lat, lng, l.lat, l.lng)
                                      };

                        if (radius != 0)
                        {
                            results = results.Where(l => l.distance <= radius);
                        }
                        results = results.OrderBy(l => l.distance).ThenBy(n => n.name);
                        if (radius == 0)
                        {
                            results = results.Take(30);
                        }

                        locator_results.locations = results.ToArray();

                    }
                    catch (Exception ex)
                    {
                        string site = string.Empty;
                        site = "Siliq.com";
                        MailService.SendAlert(_configuration, "no-reply@bauschhealth.com", site + " - Physician Locator Exception", ex.StackTrace, "websitealerts@bauschhealth.com");
                        locator_results.error = ex.Message;
                    }
                }

            }
            ContentResult result = new ContentResult();
            result.Content = Convert.ToString(JsonConvert.SerializeObject(locator_results, Formatting.Indented));
            result.ContentType = "application/json";
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }










        public IActionResult Finder(string zip, int radius)
        {
            string url = string.Format("/siliq/finder?zip={0}&radius={1}", zip, radius);

            _IHttpContextAccessor.HttpContext.Response.Redirect(url);
            return null;
        }
    }


    #region Arestin Classes

    public class Arestin_location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zipcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }        //
        public string Address3 { get; set; }        //
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }        //
        public double Longitude { get; set; }       //
    }

    public class Arestin_result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }        //
        public string Address3 { get; set; }        //
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }        //
        public double Longitude { get; set; }       //       
        public string Country { get; set; }



        public double Distance { get; set; }        //
    }

    public class ArestinLocator_results
    {
        public string Zipcode { get; set; }
        public int Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Arestin_result[] Locations { get; set; }
        public string Error { get; set; }
    }

    #endregion


    public class Haversine
    {
        //The formula to calculate the distance between 2 coordinates
        public static double calculate(double lat1, double lng1, double lat2, double lng2)
        {
            var miles_km_ratio = 0.621371192;
            var R = 6372.8; // In kilometers
            var dLat = toRadians(lat2 - lat1);
            var dLon = toRadians(lng2 - lng1);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            return R * 2 * Math.Asin(Math.Sqrt(a)) * miles_km_ratio;
        }
        public static double toRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
    public class Locator_results
    {
        public string zip { get; set; }
        public int radius { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public Practice_result[] locations { get; set; }
        public string error { get; set; }
    }

    public class Practice_location
    {
        public string name { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string practice { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string category_tag { get; set; }
        public string country { get; set; }
        public string user_1_text { get; set; }

        public string user_2_text { get; set; }
        public string user_3_text { get; set; }
        public string user_4_text { get; set; }

        public string user_5_text { get; set; }
        public string user_6_text { get; set; }
        public string user_7_text { get; set; }
        public string user_8_text { get; set; }
        public string user_9_text { get; set; }
        public string user_10_text { get; set; }
        public string user_11_text { get; set; }
        public string user_12_text { get; set; }
        public string user_13_text { get; set; }
        public string user_14_text { get; set; }
        public string user_15_text { get; set; }
        public int module_id { get; set; }


    }
    public class Practice_result
    {
        public string name { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string practice { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double distance { get; set; }

        public string category_tag { get; set; }
        public string country { get; set; }
        public string user_1_text { get; set; }

        public string user_2_text { get; set; }
        public string user_3_text { get; set; }
        public string user_4_text { get; set; }

        public string user_5_text { get; set; }
        public string user_6_text { get; set; }
        public string user_7_text { get; set; }
        public string user_8_text { get; set; }
        public string user_9_text { get; set; }
        public string user_10_text { get; set; }
        public string user_11_text { get; set; }
        public string user_12_text { get; set; }
        public string user_13_text { get; set; }
        public string user_14_text { get; set; }
        public string user_15_text { get; set; }
        public int module_id { get; set; }

    }

    public class Address_components
    {
        public string short_name { get; set; }
        public string long_name { get; set; }
        public string[] types { get; set; }
    }
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Viewport
    {
        public Location northeast { get; set; }
        public Location southwest { get; set; }
    }
    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }
    public class Result
    {
        public Address_components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }
    public class Geo_results
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }
}

