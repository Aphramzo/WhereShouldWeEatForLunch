using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Igloo.SharpSquare.Core;

namespace WhereShouldWeEatLunch.APIs
{
    public  class FourSquare : RESTfulAPI
    {
        public static List<String> FindEateriesNear(string zipCode)
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var searchParams = new Dictionary<string, string>();
            searchParams.Add("near", zipCode);
            var venues = sharpSquare.SearchVenues(searchParams);

            return venues.Select(x => x.name).ToList();
        }

        public static List<Igloo.SharpSquare.Entities.Venue> FindEateriesNearLatLong(decimal lat, decimal lon, String categoryId)
        {
            SharpSquare sharpSquare = new SharpSquare(ConfigurationManager.AppSettings["fourSquareClientId"], ConfigurationManager.AppSettings["fourSquareClientSecret"]);
            var searchParams = new Dictionary<string, string>();
            searchParams.Add("ll", String.Format("{0},{1}",lat,lon));
            if(!String.IsNullOrEmpty(categoryId))
                searchParams.Add("categoryId", categoryId);
            var venues = sharpSquare.SearchVenues(searchParams);
            return venues;
        }

        public static object FindEateriesNearLatLong(decimal lat, decimal lon)
        {
            return FindEateriesNearLatLong(lat, lon, null);
        }
    }

    
} ;