using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Delu.Delicia.Site.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            return View();
        }

        
    }
}