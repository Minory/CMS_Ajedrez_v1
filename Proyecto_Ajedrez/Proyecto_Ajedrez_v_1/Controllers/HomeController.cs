using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using Proyecto_Ajedrez_v_1.Models;

namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class HomeController : Controller
    {

        Ajedrez_v1Entities1 db = new Ajedrez_v1Entities1();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult NoticiaDetalle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo noticia = db.articulo.Find(id);
            return View(noticia);

        }
    }
}