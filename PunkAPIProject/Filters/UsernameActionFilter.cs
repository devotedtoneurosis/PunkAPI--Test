using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;

namespace PunkAPIProject.Filters
{
    public class UsernameActionFilter : ActionFilterAttribute, IActionFilter
    {
        //The specification for the API stated to use RegEx however recent years have made email addresses available
        //that are not easily caught as valid based on historical Regexes, this may be revisited in a future release.
        string emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                        + "@"
                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";


        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            StreamReader r = new StreamReader(HttpContext.Current.Request.InputStream);
            r.BaseStream.Seek(0, SeekOrigin.Begin);
            string bodyText = r.ReadToEnd();
            Rating rating = JsonConvert.DeserializeObject<Rating>(bodyText);
            

            //Check if the email matching an appropriately formed address, otherwise, notify the user.
            if (Regex.IsMatch(rating.username, emailRegex) == false)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Username is not a valid email address.");
            }

            OnActionExecuting(filterContext);
        }

    }
}