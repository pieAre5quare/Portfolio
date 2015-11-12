using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BugTrackerLanding()
        {
            return View();
        }

        public ActionResult JSexercises()
        {
            return View();
        }
    }
}