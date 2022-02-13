using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace PunkAPIProject
{
    public class PunkAPI
    {
        private const string URL = "https://api.punkapi.com/v2/";
        private const string beerQueryParameter = "beers/";
        private const string beerQueryNameSearchParameter = "?beer_name=";
        private HttpClient punkClient;

        private HttpClient HTTPClientProvider()
        {
            if (punkClient == null)
            {
                punkClient = new HttpClient();
                punkClient.BaseAddress = new Uri(URL);
                punkClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return punkClient;
        }

        //Get the Beer object for the corresponding beerid if it is a valid beer id in the PunkAPI. 
        //this routine not returning null can also be used as validation.
        public Beer GetBeer(int beerId)
        {
            HttpClient client = HTTPClientProvider();
            HttpResponseMessage response = client.GetAsync(beerQueryParameter+beerId.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                IEnumerable<Beer> b = response.Content.ReadAsAsync<IEnumerable<Beer>>().Result;
                Beer[] beers = b.ToArray<Beer>();
                if (beers != null && beers.Length > 0)
                {
                    return beers[0];
                }
                else
                {
                    Console.WriteLine("PunkAPI is available but no beer matches the ID");
                }
            }
            else
            {
                Console.WriteLine("Issue with retrieving beer from PunkAPI");
            }

            return null;
        }

        //Retrieve all beers from the PunkAPI with a matching name. Name can be a partial match, match handling
        //is handled by the PunkAPI so we just pass along values
        public List<Beer> GetBeersByName(string nameParam)
        {
            HttpClient client = HTTPClientProvider();
            HttpResponseMessage response = client.GetAsync(beerQueryParameter + beerQueryNameSearchParameter + nameParam).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                IEnumerable<Beer> b = response.Content.ReadAsAsync<IEnumerable<Beer>>().Result;
                List<Beer> beers = b as List<Beer>;
                if (beers != null && beers.Count<Beer>() > 0)
                {
                    return beers;
                }
                else
                {
                    Console.WriteLine("PunkAPI is available but no matching beers found.");
                }

            }
            else
            {
                Console.WriteLine("Issue with retrieving beer from PunkAPI");
            }

            return null;
        }

        //Just checks to see if PunkAPI can validate the beer id
        public bool ValidateBeer(int beerId)
        {
            return GetBeer(beerId) != null;
        }

    }
}
