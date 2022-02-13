using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace PunkAPIProject.Filters
{
    public class UsernameActionFilter : ActionFilterAttribute, IActionFilter
    {
        string emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                        + "@"
                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] username = filterContext.HttpContext.Request.Params.GetValues("username");

            //Check if the email matching an appropriately formed address, otherwise, notify the user.
            if (Regex.IsMatch(username[0], emailRegex) == false)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Username is not a valid email address.");
            }

            OnActionExecuting(filterContext);
        }

    }
}