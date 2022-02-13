using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunkAPIProject
{
    public class Beer
    {
        public int id { get; set; }
        public string name{ get; set; }
        public string tagline { get; set; }
        public string first_brewed { get; set; }
        public string description { get; set; }

    }

    public class Rating
    {
        public Rating() { }
        public Rating(int bId, string un, int r, string com)
        {
            id = bId;
            username = un;
            rating = r;
            comments = com;
        }

        public int id { get; set; }
        public string username { get; set; }
        public int rating { get; set; }
        public string comments { get; set; }
    }

    //The standard Rating class is more useful for regular application usage and database storage, however,
    //the API spec document indicates that id should not be shown in displayed rating collections. For this
    //purpose, we use DisplayRating
    public class DisplayRating
    {
        public DisplayRating(string un, int r, string com)
        {
            username = un;
            rating = r;
            comments = com;
        }
        public string username { get; set; }
        public int rating { get; set; }
        public string comments { get; set; }
    }

    public class RatingSearchResult
    {
        public RatingSearchResult(Beer b, List<Rating> rL)
        {
            id = b.id;
            name = b.name;
            description = b.description;

            if (rL != null && rL.Count > 0)
            {
                //convert Rating list to a DisplayRating for instant API display according to specification documents.
                userRatings = new List<DisplayRating>();
                for (int i = 0; i < rL.Count; i++) { userRatings.Add(new DisplayRating(rL[i].username, rL[i].rating, rL[i].comments)); }
            }
        }

        public int id;
        public string name;
        public string description;
        public List<DisplayRating> userRatings;
    }
}
