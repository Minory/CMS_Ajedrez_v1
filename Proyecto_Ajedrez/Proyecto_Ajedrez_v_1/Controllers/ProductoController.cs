using Proyecto_Ajedrez_v_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class ProductoController : Controller
    {

        Ajedrez_v1Entities1 db = new Ajedrez_v1Entities1();
        // GET: Producto
        public ActionResult Index()
        {
            return View(db.producto.ToList());// cuando cargue listara todos los productos en filas 
        }

        public ActionResult CrearProducto()
        {
            return View(new producto());
        }

        [HttpPost]
        public ActionResult CrearProducto(producto obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            obj.fechaCreacion = DateTime.Now;
            db.producto.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}