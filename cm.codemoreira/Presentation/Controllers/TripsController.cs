using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class TripsController : Controller
    {
        //
        // GET: /Trips/
        public ActionResult Index()
        {
            string caminho = Server.MapPath("\\Content\\Images\\trips\\USA\\NewYork");

            string[] files = Directory.GetFiles(caminho);

            List<FileInfo> l = new List<FileInfo>();
            foreach (var item in files)
            {
                FileInfo f = new FileInfo(item);
                l.Add(f);
            }
                

            return View(l);
        }
	}
}