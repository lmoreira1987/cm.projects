using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Delu.Delicia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Signin(string user, string password)
        {
            // https://www.youtube.com/watch?v=Loa_xNcR4ck

            //Session["User"] = null;

            //if (user == "Andrea" || password == "d&lu")
            //    Sess

            return null;

            //if (usuario != null)
            //{
            //    Session["User"] = usuario;

            //    var validacao = "";
            //    validacao = loginData.ValidarUsuario(usuario.Id);

            //    return Json(validacao,);
            //}
            //else
            //{
            //    return Json("error");
            //}
        }
    }
}
