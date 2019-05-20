﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Ajedrez_v_1.Models;


namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class CarritoController : Controller
    {

        Ajedrez_v1Entities1 db = new Ajedrez_v1Entities1();
        // GET: Carrito
        public ActionResult Index()
        {
            return View(db.producto.ToList());
        }

        public ActionResult CreateProducto()
        {
            ViewBag.cate = new SelectList(db.categoria.ToList(), "id_categoria", "desc_categoria");
            return View(new producto());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateProducto(producto obj)
        {
            if (!ModelState.IsValid)
            {
                
                return View(obj);
            }
            obj.fechaCreacion=DateTime.Now;
            db.producto.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListarProductos(int? id=9)
        {

            ViewBag.cate = new SelectList(db.categoria.ToList(), "id_categoria", "desc_categoria", id);
            if (id==9)
            {
                return View(db.producto.ToList());
            }

            else
            {
                return View(db.producto.Where(p => p.id_categoria==id).ToList());
            }
            
        }


    }
}