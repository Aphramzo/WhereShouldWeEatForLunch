using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;

namespace WhereShouldWeEatLunch.APIs.FourSquare
{
    public  class API : RESTfulAPI
    {
        public const String FoodCategoryId = "4d4b7105d754a06374d81259";

        public static List<Venue> FindEateriesNearLatLong(double lat, double lon, String categoryId)
        {
            var client = GetRestClient();
            var request = GetNonUserRequest("v2/venues/search");
            
            request.AddParameter("ll", String.Format("{0},{1}", lat, lon));
            request.AddParameter("limit", 15);
            
            request.AddParameter("intent", "browse");

            if (!String.IsNullOrEmpty(categoryId) && categoryId != "0")
            {
                request.AddParameter("categoryId", categoryId);
                request.AddParameter("radius", 16000); //widen the search if they are looking for something in particular
            }
            else
            {
                request.AddParameter("categoryId", String.Join(",", GetFoodCategoryList()));
                request.AddParameter("radius", 8000);
            }
                
            var response = client.Execute<VenueResult>(request);

            var venues = response.Data.response.venues;
            venues.ForEach(x=>x.SetCurrentCoords(lat,lon));
            return venues;
        }
        public static Venue GetVenue(string id)
        {
            var client = GetRestClient();
            var request = GetNonUserRequest("v2/venues/{id}");
            request.AddUrlSegment("id", id);
            var response = client.Execute<VenueByIdResult>(request);
            return response.Data.response.venue;
        }

        private static RestRequest GetNonUserRequest(String endpoint)
        {
            var request = new RestRequest(endpoint, Method.GET);
            request.AddParameter("client_id", ConfigurationManager.AppSettings["fourSquareClientId"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            request.AddParameter("v", DateTime.Now.ToString("yyyyMMdd"));
            return request;
        }

        private static RestClient GetRestClient()
        {
            var baseUrl = "https://api.foursquare.com";
            var client = new RestClient(baseUrl);
            return client;
        }

        private static List<String> GetFoodCategoryList()
        {
            var foodCats = GetCategories();
            return foodCats.Select(v => v.id).ToList();
        }

        public static List<VenueCategory> GetCategories()
        {
            var client = GetRestClient();
            var request = GetNonUserRequest("v2/venues/categories");
            var response = client.Execute<VenueCategoriesResult>(request);

            return response.Data.response.categories.Where(x=>x.id ==FoodCategoryId).Select(c=>c.categories).First();
        }

        public static object FindEateriesNearLatLong(double lat, double lon)
        {
            return FindEateriesNearLatLong(lat, lon, null);
        }
    }

    #region DTOs

    internal class VenueByIdResult
    {
        public VenueByIdResponse response { get; set; }
    }

    internal class VenueByIdResponse
    {
        public Venue venue { get; set; }
    }

    internal class VenueCategoriesResult
    {
        public VenueCategoriesResponse response { get; set; }
    }

    internal class VenueCategoriesResponse
    {
        public List<VenueCategory> categories { get; set; }
    }

    internal class VenueResult
    {
        public VenueResultResponse response { get; set; }
    }

    internal class VenueResultResponse
    {
        public List<Venue> venues { get; set; }
    }

    public class VenueCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<VenueCategory> categories { get; set; }
    }

    public class Venue
    {
        public String name { get; set; }
        public String id { get; set; }
        public Location location { get; set; }
        public VenueContact contact { get; set; }
        public VenueMenu menu { get; set; }
        public Hours hours { get; set; }
        public String url { get; set; }
        public double rating { get; set; }
        private double currentLat { get; set; }
        private double currentLong { get; set; }

        public double Distance
        {
            get
            {
                var myCoords = new GeoCoordinate(currentLat, currentLong);
                var venueCoords = new GeoCoordinate(location.lat, location.lng);

                //getDistanceTo returns meters - I want to show miles
                return myCoords.GetDistanceTo(venueCoords) * 0.000621371;
            }
        }

        public void SetCurrentCoords(double lat, double lon)
        {
            currentLat = lat;
            currentLong = lon;
        }
    }

    public class VenueContact
    {
        public String formattedPhone { get; set; }
        public String phone { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public String address { get; set; }
        public String crossStreet { get; set; }
    }

    public class Hours
    {
        public bool isOpen { get; set; }
        public String status { get; set; }
        public List<TimeFrame> timeframes { get; set; } 
    }

    public class TimeFrame
    {
        public string days { get; set; }
        public List<TimeFrameSegments> segments { get; set; }
        public List<TimeFrameHours> open { get; set; } 
    }

    public class TimeFrameHours
    {
        public string renderedTime { get; set; }
    }

    public class TimeFrameSegments
    {
        public string renderedTime { get; set; }
        public string label { get; set; }
    }

    public class VenueMenu
    {
        public String mobileUrl { get; set; }
        public String url { get; set; }
    }

    #endregion
};