using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekQuiz.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //var variables = Environment.GetEnvironmentVariables();

            //var vars = "";

            //foreach(var ev in variables.Keys)
            //{
            //    vars+=string.Format("{0}={1};  ",ev,variables[ev]);
            //}

            //ViewBag.Variables = vars;

            ViewBag.ConnectionString = ConfigurationManager.ConnectionStrings["MyDefaultConnection"];

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
