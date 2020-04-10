using Delu.Delicia.Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Delu.Delicia.Site.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Products
        public ActionResult Index(int? id)
        {
            Title(id);
            return View(LoadXML(id));
        }

        public ActionResult List(int? id)
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            return View(LoadXML());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["Login"].ToString() != "D&lu")
                return RedirectToAction("Index", "Home");

            DdlCategories();

            return View();
        }

        private void DdlCategories()
        {
            ViewBag.IdCategory = new SelectList
                            (
                                new ProductsModels().Categories(),
                                "IdCategory",
                                "Category"
                            );
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase Img, ProductsModels prod)
        {
            if (!ModelState.IsValid)
            {
                DdlCategories();

                return View(prod);
            }

            string path = string.Empty;
            ProductsModels p = new ProductsModels();
            var last = LoadXML().LastOrDefault();
            try
            {
                p.Id = last.Id + 1;
            }
            catch 
            {
                p.Id = 1;
            }
            p.Description = prod.Description;
            p.IdCategory = Convert.ToInt32(prod.IdCategory);
            p.Title = prod.Title;

            if (Img != null && Img.ContentLength > 0)
            {
                try
                {
                    Random r = new Random();
                    string imgFinal = Path.GetFileName("img_" + r.Next() + "_" + p.Id + "_" + Img.FileName);
                    path = Path.Combine(Server.MapPath("~/ImgProducts/"), imgFinal);
                    p.Img = "/ImgProducts/" + imgFinal;
                    Img.SaveAs(path);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    RedirectToAction("Erro", "Produtos");
                }
            }

            CreateNodeXML(p);

            DdlCategories();

            return RedirectToAction("List", "Produtos");
        }

        private void CreateNodeXML(ProductsModels p)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Server.MapPath("/App_Data/products.xml"));

            XmlElement rootProduct = xd.CreateElement("product");
            XmlAttribute attId = xd.CreateAttribute("id");
            XmlElement xmlIdCategory = xd.CreateElement("idCategory");
            XmlElement xmlDescription = xd.CreateElement("description");
            XmlElement xmlTitle = xd.CreateElement("title");
            XmlElement xmlImg = xd.CreateElement("image");

            attId.InnerText = p.Id.ToString();
            xmlIdCategory.InnerText = p.IdCategory.ToString();
            xmlDescription.InnerText = p.Description;
            xmlTitle.InnerText = p.Title;
            xmlImg.InnerText = p.Img.Replace("\\","//");

            rootProduct.Attributes.Append(attId);
            rootProduct.AppendChild(xmlIdCategory);
            rootProduct.AppendChild(xmlDescription);
            rootProduct.AppendChild(xmlTitle);
            rootProduct.AppendChild(xmlImg);

            xd.DocumentElement.AppendChild(rootProduct);

            xd.Save(Server.MapPath(@"/App_Data/products.xml"));
        }

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

        public ActionResult Delete(int id)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(@"/App_Data/products.xml"));

            XmlNode xmlnode = xmldoc.DocumentElement.SelectSingleNode("/products/product[@id='" + id + "']");
            xmlnode.ParentNode.RemoveChild(xmlnode);

            xmldoc.Save(Server.MapPath(@"/App_Data/products.xml"));

            System.IO.File.Delete(Server.MapPath(xmlnode.ChildNodes[3].InnerText)); // 3 = image

            LoadXML();

            return RedirectToAction("List", "Produtos");
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
                    int idFilter = Convert.ToInt32(node.SelectSingleNode("idCategory").InnerText);

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

        private List<ProductsModels> LoadXML()
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
                    try
                    {
                        product = new ProductsModels();

                        product.Id = Convert.ToInt32(node.Attributes["id"].Value);
                        product.IdCategory = Convert.ToInt32(node.SelectSingleNode("idCategory").InnerText);
                        LoadCategory(product);
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
            catch (System.IO.FileNotFoundException)
            {
                // Exception
            }

            return products;
        }

        private void LoadCategory(ProductsModels p)
        {
            switch (p.IdCategory)
            {
                case 1: p.Category = "Bolos para festas"; break;
                case 2: p.Category = "Bolos tradicionais"; break;
                case 3: p.Category = "Tortas doces e salgados"; break;
                case 4: p.Category = "Cupcakes"; break;
                case 5: p.Category = "Trufas diversas"; break;
                case 6: p.Category = "Outros produtos"; break;
                default:
                    break;
            }
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
