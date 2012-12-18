using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using Igloo.SharpSquare.Core;
using Igloo.SharpSquare.Entities;

namespace WhereShouldWeEatLunch.APIs
{
    public  class FourSquare : RESTfulAPI
    {
        public const String FoodCategoryId = "4d4b7105d754a06374d81259";
        public static List<String> FindEateriesNear(string zipCode)
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var searchParams = new Dictionary<string, string>();
            searchParams.Add("near", zipCode);
            var venues = sharpSquare.SearchVenues(searchParams);

            return venues.Select(x => x.name).ToList();
        }

        public static List<FourSquareVenue> FindEateriesNearLatLong(double lat, double lon, String categoryId)
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var searchParams = new Dictionary<string, string>();
            searchParams.Add("ll", String.Format("{0},{1}",lat,lon));
            if(!String.IsNullOrEmpty(categoryId) && categoryId != "0")
                searchParams.Add("categoryId", categoryId);
            else
                searchParams.Add("categoryId",String.Join(",",GetFoodCategoryList(sharpSquare)));
            var venues = sharpSquare.SearchVenues(searchParams);
            return venues.Select(x=>new FourSquareVenue(x, lat, lon)).ToList();
        }

        private static List<String> GetFoodCategoryList(SharpSquare sharpSquare)
        {
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

    public class FourSquareVenue
    {
        public String name { get { return _venue.name; } }
        public String id { get { return _venue.id; } }
        public Location location { get { return _venue.location; } }
        public Contact contact { get { return _venue.contact; } }
        private Venue _venue { get; set; }
        private double _currentLat { get; set; }
        private double _currentLong { get; set; }
        public FourSquareVenue(Venue venue, double currentLat, double currentLong)
        {
            _venue = venue;
            _currentLat = currentLat;
            _currentLong = currentLong;
        }

        public double Distance
        {
            get
            {
                var myCoords = new GeoCoordinate(_currentLat, _currentLong);
                var venueCoords = new GeoCoordinate(_venue.location.lat, _venue.location.lng);

                //getDistanceTo returns meters - I want to show miles
                return myCoords.GetDistanceTo(venueCoords) * 0.000621371;   
            }
        }
    }
    
} ;