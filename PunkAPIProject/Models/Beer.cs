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

    public class RatingSearchResult
    {
        public RatingSearchResult(Beer b, List<Rating> rL)
        {
            id = b.id;
            name = b.name;
            description = b.description;
            userRatings = rL;
        }

        public int id;
        public string name;
        public string description;
        public List<Rating> userRatings;
    }
}
