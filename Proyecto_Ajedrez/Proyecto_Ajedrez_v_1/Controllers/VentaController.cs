using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Ajedrez_v_1.Models;


namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class VentaController : Controller
    {
        Ajedrez_v1Entities1 db = new Ajedrez_v1Entities1();
        // GET: Venta
        public ActionResult Index()
        {
            return View(db.venta.ToList().OrderBy(x => x.diaventa));
        }
        public ActionResult Detalles(int Id)
        {
            return View(db.venta.Find(Id));
        }
    }
}