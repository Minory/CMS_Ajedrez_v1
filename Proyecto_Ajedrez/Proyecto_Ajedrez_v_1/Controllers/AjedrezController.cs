using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Proyecto_Ajedrez_v_1.Models;

namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class AjedrezController : Controller
    {

        Ajedrez_v1Entities1 db = new Ajedrez_v1Entities1();
        // GET: Ajedrez
        public ActionResult Index()
        {
            return View(db.articulo.ToList());
        }

        public ActionResult CreateArticulo()
        {
            return View(new articulo());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateArticulo(articulo obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            obj.usuario_id = 2;
            db.articulo.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id) {

            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo noticia = db.articulo.Find(id);
            return View(noticia);

        }

        public ActionResult ConsultaXtitulo(string nom) {

            //db.articulo.ToList();
          
            return View(db.articulo.Where(p=>p.titulo.Equals(nom)).ToList());
        }


    }
}