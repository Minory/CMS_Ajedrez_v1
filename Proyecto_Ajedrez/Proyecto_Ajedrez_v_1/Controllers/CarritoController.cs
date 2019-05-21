using System;
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

        public ActionResult AgregarCarrito(int id)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(db.producto.Find(id), 1));
                Session["carrito"] = compras;
            }
            else
            {
                List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
                int IndexExistente = getIndex(id);
                if (IndexExistente == -1)
                {
                    compras.Add(new CarritoItem(db.producto.Find(id), 1));
                }

                else
                {
                    compras[IndexExistente].Cantidad++;
                    Session["carrito"] = compras;
                }
                
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            return View("AgregarCarrito");
        }

        public ActionResult FinalizaCompra()
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            if (compras !=null && compras.Count>0)
            {
                venta nuevaVenta = new venta();
                nuevaVenta.diaventa = DateTime.Now;
                nuevaVenta.subtotal = (double)compras.Sum(x => x.Producto.precio * x.Cantidad);//aqui agregué conversion a double xq
                //subtotal , igv y los otros datos estan en float que aqui en .Net los lee como double.
                nuevaVenta.igv = nuevaVenta.subtotal * 0.18;
                nuevaVenta.Total = nuevaVenta.subtotal + nuevaVenta.igv;

                nuevaVenta.listaventa = (from producto in compras
                                         select new listaventa
                                         {
                                             idproducto = producto.Producto.idproducto,
                                             Cantidad = producto.Cantidad,
                                             total = producto.Cantidad * (double)producto.Producto.precio
                                         }).ToList();
                db.venta.Add(nuevaVenta);
                db.SaveChanges();
                Session["carrito"] = new List<CarritoItem>();

            }
            return View();
        }





        private int getIndex(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.idproducto== id)
                    return i;
            }

            return -1;
        }

    }

}
