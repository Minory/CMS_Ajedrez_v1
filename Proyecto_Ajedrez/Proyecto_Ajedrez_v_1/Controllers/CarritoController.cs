using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
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
        //*********************************************CRUD CATEGORIA*******************************************
        //aqui crud de CATEGORIA
        public ActionResult ListaCategorias()
        {
            return View(db.categoria.ToList());
        }

        public ActionResult CreateCategoria()
        {
            return View(new categoria());
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult CreateCategoria(categoria obj)
        {
            if (!ModelState.IsValid)
            {

                return View(obj);
            }
            
            db.categoria.Add(obj);
            db.SaveChanges();
            return RedirectToAction("ListaCategorias");
        }

        public ActionResult DetailsCategoria(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria cate = db.categoria.Find(id);
            return View(cate);

        }


        public ActionResult EditCategoria(int id)
        {
            List<categoria> lstMarca = db.categoria.ToList();
            categoria ObjMarca = lstMarca.Where(p => p.id_categoria == id).FirstOrDefault();

            return View(ObjMarca);
        }

        [HttpPost]
        public ActionResult EditCategoria(categoria obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListaCategorias");
        }

        public ActionResult DeleteCategoria(int id)
        {
            List<categoria> lstMarca = db.categoria.ToList();
            categoria ObjMarca = lstMarca.Where(p => p.id_categoria == id).FirstOrDefault();
            return View(ObjMarca);
        }
        [HttpPost, ActionName("DeleteCategoria")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoria obj = db.categoria.Find(id);
            db.categoria.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("ListaCategorias");
        }


        /// <summary>
        /// aqui va todo de PRODUCTO 
        /// </summary>
        /// <returns></returns>
        //*****************************************CRUD DE PRODUCTO***********************************************
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


        public ActionResult EditProducto(int id)
        {
            ViewBag.cate = new SelectList(db.categoria.ToList(), "id_categoria", "desc_categoria");
            List<producto> lstprod = db.producto.ToList();
            producto Obj = lstprod.Where(p => p.idproducto == id).FirstOrDefault();

            return View(Obj);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProducto(producto obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            obj.fechaCreacion = DateTime.Now;
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //detalle
        public ActionResult DetailsProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto pro = db.producto.Find(id);
            return View(pro);
        }
        //Elimninar
        public ActionResult DeleteProducto(int id)
        {
            List<producto> lstMarca = db.producto.ToList();
            producto ObjMarca = lstMarca.Where(p => p.idproducto == id).FirstOrDefault();
            return View(ObjMarca);
        }
        [HttpPost, ActionName("DeleteProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            producto obj = db.producto.Find(id);
            db.producto.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        /*finaliza CRUD DE PRODUCTOS Y CATEGORIAS-----*/

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

        public ActionResult AgregarCarritoDetalle(int? Id)
        {
            producto p = db.producto.Where(x => x.idproducto == Id).SingleOrDefault();
            return View(p);

        }

        //List<cart> li = new List<cart>();
        List<CarritoItem> compras = new List<CarritoItem>();

        [HttpPost]
        public ActionResult AgregarCarritoDetalle(producto pi, string qty, int? Id)
        {
            producto p = db.producto.Where(x => x.idproducto == Id).SingleOrDefault();

           // CarritoItem c = new CarritoItem();
            
           // c.Producto.idproducto = p.idproducto;
            //c.Producto.precio = p.precio;
            //c.Cantidad = Convert.ToInt32(qty);
            //c.bill = c.price * c.qty;
           // c.productname = p.pro_name;
            if (TempData["carrito"] == null)
            {
                //compras.Add(c);
                compras.Add(new CarritoItem(db.producto.Find(p.idproducto), Convert.ToInt32(qty)));
                //TempData["cart"] = li;
                TempData["carrito"]=compras;
            }
            else
            {
                List<CarritoItem> li2 = TempData["carrito"] as List<CarritoItem>;
                li2.Add(new CarritoItem(db.producto.Find(p.idproducto), Convert.ToInt32(qty)));
                TempData["carrito"] = li2;
            }

            TempData.Keep();



            return RedirectToAction("ListarProductos");
        }


        public ActionResult checkout()
        {
            TempData.Keep();


            return View();
        }


        /*
    [HttpPost]
    public JsonResult AgregarCarrito(int id, int cant)
    {
        if (Session["carrito"] == null)
        {
            List<CarritoItem> compras = new List<CarritoItem>();
            compras.Add(new CarritoItem(db.producto.Find(id), cant));
            Session["carrito"] = compras;
        }
        else
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            int IndexExistente = getIndex(id);
            if (IndexExistente == -1)
                compras.Add(new CarritoItem(db.producto.Find(id), cant));
            else
                compras[IndexExistente].Cantidad += cant;
            Session["carrito"] = compras;
        }
        return Json(new { Response = true }, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    public ActionResult AgregarCarrito()
    {
        return View();
    }


    */


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
