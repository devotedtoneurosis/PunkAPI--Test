using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using PunkAPIProject.Filters;
using System.Web.Http.Cors;

namespace PunkAPIProject.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class BeerController : Controller
    {
        RatingDB ratingDB;
        PunkAPI punkAPIController;

        //initailize the controller with the necessary objects for PunkAPI querying and RatingDB access
        public BeerController()
        {
            ratingDB = new RatingDB();
            punkAPIController = new PunkAPI();
        }

        //Retrieve ratings left by ohter users for the beer the user searches for (using a partial or full name)
        [HttpGet]
        public string GetRatings()
        {
            string beerName = Request.Params.Get("q");

            //grab beers with matching names from the PunkAPI resource
            List<Beer> beerList = punkAPIController.GetBeersByName(beerName);
            if (beerList != null && beerList.Count > 0)
            {
                //Form a list of search results by formatting the PunkAPI beer list and adding matching ratings
                List<RatingSearchResult> searchResults = new List<RatingSearchResult>();
                for (int i = 0; i < beerList.Count; i++)
                {
                    List<Rating> ratingList = ratingDB.GetRatingsByBeer(beerList[i].id);
                    RatingSearchResult rSR = new RatingSearchResult(beerList[i], ratingList);
                    searchResults.Add(rSR);
                }

                //return data in JSON format to API user
                string data = JsonConvert.SerializeObject(searchResults, new JsonSerializerSettings());
                return data;
            }
            else
            {
                return null;
            }

        }

        //Allow API users to post a rating for a specific beer corresponding to a PunkAPI beer entry
        [HttpPost]
        [UsernameActionFilter] //Apply the username action filter to ensure a valid username is passed
        public ActionResult PostRating(Rating rate)
        {
            //extract the PunkAPI beer id
            string[] path = Request.Path.Split('/');
            int beerId = -1;
            if (int.TryParse(path[path.Length - 1], out beerId))
            {
                rate.id = beerId;

                //ensure rating is within valid ranges
                if (rate.rating > 0 && rate.rating < 6)
                {
                    //ensure beer is valid
                    if (punkAPIController.ValidateBeer(rate.id))
                    {

                        //attempt to add rating to the database
                        if (ratingDB.AddRating(rate))
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.OK);

                        }
                        else
                        {
                            //Exposing opaque error code as this means there's an issue with the data layer, information
                            //which should not be made public but would be included in developer documentation so if the
                            //code is reported we're able to troubleshoot effectively. 
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Encountered System Error: 10");
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Beer id not valid.");
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Rating is not within valid range, must be a whole number from 1-5 (inclusive).");
                }

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "A valid beer id URL parameter is required");
            }

        }


    }
}
