using FoodFinder_WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FoodFinder_WebApp.ViewModels
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public string aut { get; set; }
        public string aut2 { get; set; }
        public string controller { get; set; }
 

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (aut2 != null)
            {
                if ((string.Compare(context.HttpContext.Session.GetString("tipo"), aut) != 0) && (string.Compare(context.HttpContext.Session.GetString("tipo"), aut2) != 0))
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Utilizador",
                                action = "Denied"
                            }));
                }
            }
            else
            {
                if ((string.Compare(context.HttpContext.Session.GetString("tipo"), aut) != 0))
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Utilizador",
                                action = "Denied"
                            }));
                }
            }
        }

    }
}
