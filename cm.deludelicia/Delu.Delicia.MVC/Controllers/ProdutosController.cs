using Delu.Delicia.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Delu.Delicia.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Products
        public ActionResult Index(int? id)
        {
            Title(id);
            CreateNodeXML(id);
            return View(LoadXML(id));
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void CreateNodeXML(int? id)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Server.MapPath("/App_Data/products.xml"));
            
            XmlElement rootProduct = xd.CreateElement("product");
            XmlAttribute attId = xd.CreateAttribute("id");
            XmlElement xmlImage = xd.CreateElement("image");
            XmlElement xmlTitle = xd.CreateElement("title");
            XmlElement xmlDescription = xd.CreateElement("description");

            attId.InnerText = id.ToString();
            xmlImage.InnerText = "/content/images/Category/bolos_festa.jpg";
            xmlTitle.InnerText = "teste 1 desc";
            xmlDescription.InnerText = "teste 1 desc";
            
            rootProduct.Attributes.Append(attId);
            rootProduct.AppendChild(xmlImage);
            rootProduct.AppendChild(xmlTitle);
            rootProduct.AppendChild(xmlDescription);
            
            xd.DocumentElement.AppendChild(rootProduct);

            xd.Save(Server.MapPath(@"App_Data\products.xml"));
            //carregaDadosXML();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Methods

        private List<ProductsModels> LoadXML(int? id)
        {
            XmlDocument xd = new XmlDocument(); //var xdoc = XDocument.Load(Server.MapPath("/App_Data/products.xml"))
            List<ProductsModels> products = new List<ProductsModels>();

            try
            {
                xd.Load(Server.MapPath("/App_Data/products.xml"));

                XmlNodeList nodelist = xd.SelectNodes("/products/product"); // get all </products/product> nodes
                ProductsModels product;

                foreach (XmlNode node in nodelist) // for each <product> node
                {
                    int idFilter = Convert.ToInt32(node.Attributes["id"].Value);

                    if (idFilter == id)
                    {
                        try
                        {
                            product = new ProductsModels();

                            product.Img = node.SelectSingleNode("image").InnerText;
                            product.Title = node.SelectSingleNode("title").InnerText;
                            product.Description = node.SelectSingleNode("description").InnerText;

                            products.Add(product);
                        }
                        catch (Exception)
                        {
                            // Exception
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // Exception
            }

            return products;
        }

        private void Title(int? id)
        {
            switch (id)
            {
                case 1: ViewBag.Produtos = "Bolos para festas"; break;
                case 2: ViewBag.Produtos = "Bolos tradicionais"; break;
                case 3: ViewBag.Produtos = "Tortas doces e salgados"; break;
                case 4: ViewBag.Produtos = "Cupcakes"; break;
                case 5: ViewBag.Produtos = "Trufas diversas"; break;
                case 6: ViewBag.Produtos = "Outros produtos"; break;
                default:
                    ViewBag.Produtos = "Outros produtos";
                    break;
            }
        }

        #endregion
    }
}
