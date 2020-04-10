using Delu.Delicia.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Delu.Delicia.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(LoadXML());
        }

        public ActionResult List()
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            return View(LoadXML());
        }

        public ActionResult Create()
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Create(FeedbacksModels feedback)
        {
            if (!ModelState.IsValid)
            {
                return View(feedback);
            }

            var last = LoadXML().LastOrDefault();
            try
            {
                feedback.Id = last.Id + 1;
            }
            catch
            {
                feedback.Id = 1;
            }

            CreateNodeXML(feedback);

            return RedirectToAction("List", "Home");
        }

        private void CreateNodeXML(FeedbacksModels feedback)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Server.MapPath("/App_Data/feedbacks.xml"));

            XmlElement rootProduct = xd.CreateElement("feedback");
            XmlAttribute attId = xd.CreateAttribute("id");
            XmlElement xmlDescription = xd.CreateElement("description");
            XmlElement xmlName = xd.CreateElement("name");

            attId.InnerText = feedback.Id.ToString();
            xmlDescription.InnerText = feedback.Description;
            xmlName.InnerText = feedback.Name;

            rootProduct.Attributes.Append(attId);
            rootProduct.AppendChild(xmlDescription);
            rootProduct.AppendChild(xmlName);

            xd.DocumentElement.AppendChild(rootProduct);

            xd.Save(Server.MapPath(@"/App_Data/feedbacks.xml"));
        }

        public ActionResult Delete(int id)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(@"/App_Data/feedbacks.xml"));

            XmlNode xmlnode = xmldoc.DocumentElement.SelectSingleNode("/feedbacks/feedback[@id='" + id + "']");
            xmlnode.ParentNode.RemoveChild(xmlnode);

            xmldoc.Save(Server.MapPath(@"/App_Data/feedbacks.xml"));
            LoadXML();

            return RedirectToAction("List", "Home");
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

                        feedback.Id = Convert.ToInt32(node.Attributes["id"].Value);
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