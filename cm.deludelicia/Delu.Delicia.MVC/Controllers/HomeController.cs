using Delu.Delicia.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Delu.Delicia.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(LoadXML());
        }

        private List<FeedbacksModels> LoadXML()
        {
            XmlDocument xd = new XmlDocument(); //var xdoc = XDocument.Load(Server.MapPath("/App_Data/products.xml"))
            List<FeedbacksModels> feedbacks = new List<FeedbacksModels>();

            try
            {
                xd.Load(Server.MapPath("/App_Data/feedbacks.xml"));

                XmlNodeList nodelist = xd.SelectNodes("/feedbacks/feedback"); // get all </products/product> nodes
                FeedbacksModels feedback;

                foreach (XmlNode node in nodelist) // for each <product> node
                {
                    try
                    {
                        feedback = new FeedbacksModels();

                        feedback.Description = node.SelectSingleNode("description").InnerText;
                        feedback.Name = node.SelectSingleNode("name").InnerText;

                        feedbacks.Add(feedback);
                    }
                    catch (Exception)
                    {
                        // Exception
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // Exception
            }

            return feedbacks;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}