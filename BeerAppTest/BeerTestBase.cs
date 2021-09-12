using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace BeerAppTest
{
    class BeerTestBase
    {
        private const string punkapi = "https://api.punkapi.com/v2";
        private RestClient client;
        protected BeerTestBase()
        {
            client = new RestClient(punkapi);
        }

        protected List<Beer> GetBeerProducedAfter(string date)
        {
            var request = new RestRequest($"beers?brewed_after={date}", DataFormat.Json);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject<List<Beer>>(response.Content);
        }

        protected List<Beer> GetRundomBeer()
        {
            var request = new RestRequest("beers/random", DataFormat.Json);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject<List<Beer>>(response.Content);
        }

        protected List<Beer> GetBeerInCount(int numberOfBeers)
        {
            var request = new RestRequest($"beers?page=2&per_page={numberOfBeers}", DataFormat.Json);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject<List<Beer>>(response.Content);
        }

        protected List<Beer> GetBeerWithId(int beerId)
        {
            var request = new RestRequest($"beers/{beerId}", DataFormat.Json);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject<List<Beer>>(response.Content);
        }

    }
}
