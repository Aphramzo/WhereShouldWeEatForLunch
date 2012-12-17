using System;
using System.Web;
using OAuth;
using RestSharp;
using YelpSharp;
using YelpSharp.Data.Options;

namespace WhereShouldWeEatLunch.APIs
{
    public class Yelps : RESTfulAPI
    {
        private const String _ConsumerKey = "GET YOUR OWN";
        private const String _ConsumerSecret = "o8dFWf-GET YOUR OWN";
        private const String _Token = "GET YOUR OWN";
        private const String _TokenSecret = "GET YOUR OWN";
        
        public void SimpleTest()
        {

            string _term = "food";
            string _location = "Denver";

            var y = new Yelp(new Options()
                        {
                            ConsumerKey = _ConsumerKey,
                            ConsumerSecret = _ConsumerSecret,
                            AccessToken = _Token,
                            AccessTokenSecret = _TokenSecret
                        });

            var searchOptions = new SearchOptions();
            searchOptions.GeneralOptions = new GeneralOptions()
            {
                term = _term
            };

            searchOptions.LocationOptions = new LocationOptions()
            {
                location = _location
            };


            var results = y.Search(searchOptions);
        }
    }
}