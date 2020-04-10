using Delu.Delicia.Site.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;
using System.Linq;

namespace Delu.Delicia.Site.Controllers
{
    public class ContatoController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactModels contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            CreateNodeXML(contact);

            return View();
        }

        public ActionResult List(int? id)
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            return View(LoadXML().OrderByDescending(o => o.Id).ToList());
        }

        private List<ContactModels> LoadXML()
        {
            XmlDocument xd = new XmlDocument(); 
            List<ContactModels> contacts = new List<ContactModels>();

            try
            {
                xd.Load(Server.MapPath("/App_Data/contacts.xml"));

                XmlNodeList nodelist = xd.SelectNodes("/contacts/contact");
                ContactModels contact;

                foreach (XmlNode node in nodelist)
                {
                    try
                    {
                        contact = new ContactModels();

                        contact.Id = Convert.ToInt32(node.SelectSingleNode("id").InnerText);
                        contact.Name = node.SelectSingleNode("name").InnerText;
                        contact.Phone = node.SelectSingleNode("phone").InnerText;
                        contact.Email = node.SelectSingleNode("email").InnerText;
                        contact.Message = node.SelectSingleNode("message").InnerText;

                        contacts.Add(contact);
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

            return contacts;
        }

        private void CreateNodeXML(ContactModels contact)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Server.MapPath("/App_Data/contacts.xml"));

            XmlElement rootContact = xd.CreateElement("contact");
            XmlElement xmlId = xd.CreateElement("id");
            XmlElement xmlName = xd.CreateElement("name");
            XmlElement xmlPhone = xd.CreateElement("phone");
            XmlElement xmlEmail = xd.CreateElement("email");
            XmlElement xmlMessage = xd.CreateElement("message");

            var last = LoadXML().LastOrDefault();
            try
            {
                xmlId.InnerText = (last.Id + 1).ToString();
            }
            catch
            {
                xmlId.InnerText = "1";
            }

            xmlName.InnerText = contact.Name;
            xmlPhone.InnerText = contact.Phone;
            xmlEmail.InnerText = contact.Email;
            xmlMessage.InnerText = contact.Message;

            rootContact.AppendChild(xmlId);
            rootContact.AppendChild(xmlName);
            rootContact.AppendChild(xmlPhone);
            rootContact.AppendChild(xmlEmail);
            rootContact.AppendChild(xmlMessage);

            xd.DocumentElement.AppendChild(rootContact);

            xd.Save(Server.MapPath(@"/App_Data/contacts.xml"));
        }
    }
}
