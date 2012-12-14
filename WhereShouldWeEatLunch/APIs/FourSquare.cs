using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static List<Venue> FindEateriesNearLatLong(decimal lat, decimal lon, String categoryId)
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var searchParams = new Dictionary<string, string>();
            searchParams.Add("ll", String.Format("{0},{1}",lat,lon));
            if(!String.IsNullOrEmpty(categoryId) && categoryId != "0")
                searchParams.Add("categoryId", categoryId);
            else
                searchParams.Add("categoryId",String.Join(",",GetFoodCategoryList(sharpSquare)));
            var venues = sharpSquare.SearchVenues(searchParams);
            return venues;
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

        public static object FindEateriesNearLatLong(decimal lat, decimal lon)
        {
            return FindEateriesNearLatLong(lat, lon, null);
        }
    }

    
} ;