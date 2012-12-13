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

    }

    
} ;