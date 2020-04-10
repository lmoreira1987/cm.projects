using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Delu.Delicia.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProdutosController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //// GET: /<controller>/
        public IActionResult Index(int? id)
        {
            Title(id);
            Listar();

            return View();
        }

        private void Listar()
        {
            string webRootPath = _hostingEnvironment.WebRootPath; // wwwroot
            string contentRootPath = _hostingEnvironment.ContentRootPath; // ../wwwroot
            //http://www.macoratti.net/09/04/asp_gxml.htm
            //http://www.c-sharpcorner.com/article/xml-crud-create-read-update-delete-operation-using-mvc/
            //return Content(webRootPath + "\n" + contentRootPath);

            //DataSet ds = new DataSet();
            //ds.ReadXml(contentRootPath + "/XML/products.xml");

            //DataSet ds = new DataSet();
            //ds.ReadXml(IServer.MapPath(@"App_Data\poemas.xml"));
            //if (ds.Tables.Count > 0)
            //{
            //    this.GridView1.DataSource = ds;
            //    this.GridView1.DataBind();
            //}
        }

        private void Incluir()
        {
            //if ((int)Session["Incluir"] == 1)
            //{
            //    if (TextBox1.Text.Equals("") || TextBox2.Text.Equals("") || TextBox3.Text.Equals(""))
            //    {
            //        this.RegisterClientScriptBlock("alertmessage", "<script>alert('Preencha os campos do formulário.')</script>");
            //    }
            //    else
            //    {
            //        //define um documento XML e carrega o seu conteúdo 
            //        XmlDocument xmldoc = new XmlDocument();
            //        xmldoc.Load(Server.MapPath(@"App_Data\poemas.xml"));

            //        //Cria um novo elemento poemas  e define os elementos autor, titulo e conteudo
            //        XmlElement novoelemento = xmldoc.CreateElement("poemas");
            //        XmlElement xmlAutor = xmldoc.CreateElement("autor");
            //        XmlElement xmlTitulo = xmldoc.CreateElement("titulo");
            //        XmlElement xmlConteudo = xmldoc.CreateElement("conteudo");
            //        //atribui o conteúdo das caixa de texto aos elementos xml
            //        xmlAutor.InnerText = this.TextBox1.Text.Trim();
            //        xmlTitulo.InnerText = this.TextBox2.Text.Trim();
            //        xmlConteudo.InnerText = this.TextBox3.Text.Trim();
            //        //inclui os novos elementos no elemento poemas
            //        novoelemento.AppendChild(xmlAutor);
            //        novoelemento.AppendChild(xmlTitulo);
            //        novoelemento.AppendChild(xmlConteudo);
            //        //inclui o novo elemento no XML
            //        xmldoc.DocumentElement.AppendChild(novoelemento);
            //        //Salva a inclusão no arquivo XML
            //        xmldoc.Save(Server.MapPath(@"App_Data\poemas.xml"));
            //        this.Session["Incluir"] = 0;
            //        //exibe os dados no GridView
            //        carregaDadosXML();
            //    }
            //}
            //else
            //{
            //    this.RegisterClientScriptBlock("alertmessage", "<script>alert('Limpe os campos do formulário para incluir um novo item.')</script>");
            //}
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
    }
}
