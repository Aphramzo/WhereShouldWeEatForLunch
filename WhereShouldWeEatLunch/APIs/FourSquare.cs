using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using System.Web;
using Igloo.SharpSquare.Core;
using Igloo.SharpSquare.Entities;
using RestSharp;
using RestSharp.Authenticators;

namespace WhereShouldWeEatLunch.APIs
{
    public  class FourSquare : RESTfulAPI
    {
        public const String FoodCategoryId = "4d4b7105d754a06374d81259";

        public static List<FourSquareVenue> FindEateriesNearLatLong(double lat, double lon, String categoryId)
        {
            var client = GetRestClient();
            var request = GetNonUserRequest("v2/venues/search");
            
            request.AddParameter("ll", String.Format("{0},{1}", lat, lon));
            if (!String.IsNullOrEmpty(categoryId) && categoryId != "0")
                request.AddParameter("categoryId", categoryId);
            else
                request.AddParameter("categoryId", String.Join(",", GetFoodCategoryList()));
            var response = client.Execute<FourSquareVenueResult>(request);

            var venues = response.Data.response.venues;
            venues.ForEach(x=>x.SetCurrentCoords(lat,lon));
            return venues;
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
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var foodCats = GetCategories(sharpSquare);
            return foodCats.Select(v=>v.id).ToList();
        }

        private static List<Category> GetCategories(SharpSquare sharpSquare)
        {
            var cats = sharpSquare.GetVenueCategories();
            var foodCats = cats.Where(x => x.id == FoodCategoryId).Select(c => c.categories).First();
            return foodCats;
        }

        public static List<Category> GetCategories()
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            return GetCategories(sharpSquare);
        }

        public static object FindEateriesNearLatLong(double lat, double lon)
        {
            return FindEateriesNearLatLong(lat, lon, null);
        }
    }

    #region DTOs
    class FourSquareVenueResult
    {
        public FourSquareVenueResultResponse response { get; set; }
    }
    class FourSquareVenueResultResponse
    {
        public List<FourSquareVenue> venues { get; set; }
    }

    public class FourSquareVenue
    {
        public String name { get; set; }
        public String id { get; set; }
        public FourSquareLocation location { get; set; }
        public FourSquareVenueContact contact { get; set; }
        public FourSquareVenueMenu menu { get; set; }
        public String url { get; set; }
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

    public class FourSquareVenueContact
    {
        public String formattedPhone { get; set; }
        public String phone { get; set; }
    }

    public class FourSquareLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public String address { get; set; }
        public String crossStreet { get; set; }
    }

    public class FourSquareVenueMenu
    {
        public String mobileUrl { get; set; }
        public String url { get; set; }
    }
    #endregion


    
    
} ;