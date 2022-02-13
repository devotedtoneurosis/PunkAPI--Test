using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace PunkAPIProject
{
    public class RatingDB
    {
        string dbFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"/PunkAPIProject/";
        string dbFileName = "database.json";

        public RatingDB()
        {
            //on initialization, see if the DB file exists, if it does not, create it.
            if (File.Exists(dbFilePath+dbFileName) == false)
            {
                Directory.CreateDirectory(dbFilePath);
                FileStream fS = File.Create(dbFilePath+dbFileName);
                fS.Close();
            }
        }

        //Streamreader/writer provider routines
        private StreamWriter FileProviderWrite()
        {
            StreamWriter sR = new StreamWriter(File.Open(dbFilePath + dbFileName, FileMode.OpenOrCreate));
            return sR;
        }
        private StreamReader FileProviderRead()
        {
            StreamReader sR = new StreamReader(File.Open(dbFilePath + dbFileName, FileMode.Open));
            return sR;
        }

        //For writing the final assembled list to the db file
        private bool WriteJSONList(List<Rating> ratings)
        {
            try
            {
                //serialize the list to the json DB file
                StreamWriter sW = FileProviderWrite();
                JsonWriter jW = new JsonTextWriter(sW);
                jW.Formatting = Formatting.Indented;
                JsonSerializer jS = new JsonSerializer();
                jS.Serialize(jW, ratings);
                sW.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Problem serializing RatingList to JSON file.");
                return false;
            }
        }

        //For adding a beer rating
        public bool AddRating(Rating rev)
        {
            try
            {
                //get current ratings in the DB and append the list
                List<Rating> ratingList = GetAllRatings();
                if(ratingList == null) { ratingList = new List<Rating>(); }

                //add the rating
                ratingList.Add(rev);

                //write to database file
                WriteJSONList(ratingList);
                return true;
            }
            catch
            {
                Console.WriteLine("Attempt to add rating failed.");
                return false;
            }

        }

        //Gets all ratings from the database with a beer id that matches the passed parameter
        public List<Rating> GetRatingsByBeer(int beerId)
        {
            List<Rating> allRatings = GetAllRatings();

            if (allRatings != null && allRatings.Count > 0)
            {
                List<Rating> matchingRatings = allRatings.FindAll(e => (e.id == beerId));

                if (matchingRatings.Count > 0)
                {
                    return matchingRatings;
                }
                else
                {
                    Console.WriteLine("No ratings matching this beer id found.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("No ratings matching this beer id found.");
                return null;
            }
        }

        //Gets all ratings from the database.
        public List<Rating> GetAllRatings()
        {
            List<Rating> ratingList;

            using (StreamReader r = FileProviderRead())
            {
                string json = r.ReadToEnd();
                ratingList = JsonConvert.DeserializeObject<List<Rating>>(json);
                r.Close();
                if (ratingList != null && ratingList.Count > 0)
                {
                    return ratingList;
                }
            }

            return null;
        }

    }
}
