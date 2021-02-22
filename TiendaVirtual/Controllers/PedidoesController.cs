using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class PedidoesController : Controller
    {
        private VirtualShopModelContainer db = new VirtualShopModelContainer();

        // GET: Pedidoes
        public ActionResult Index()
        {
            return View(db.Pedidos.ToList());
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create(CarritoCompra carrito)
        {
            Pedido pedido = new Pedido();
            Factura factura = new Factura();
            string userId = User.Identity.GetUserId();
            factura.Id_Cliente = userId;
            double precioTotal = 0.0;
            foreach(LineaPedido l in carrito)
            {
                precioTotal += l.Cantidad * l.Producto.Precio;
                l.Producto = db.Productos.FirstOrDefault(producto => producto.Nombre == l.Producto.Nombre);
                Producto productoAlmacen = db.Productos.FirstOrDefault(producto => producto.Nombre == l.Producto.Nombre);
                productoAlmacen.Cantidad -= l.Cantidad;
                if (productoAlmacen.Cantidad < 2 && db.Stocks.First().Producto.FirstOrDefault(p => p.Nombre == l.Producto.Nombre) == null)
                {
                    Stock stock = db.Stocks.First();
                    stock.Producto.Add(productoAlmacen);
                }
            }
            pedido.LineaPedido = carrito;
            factura.Precio = precioTotal;
            pedido.Factura = factura;
            db.Pedidos.Add(pedido);
            db.SaveChanges();
            List<int> ids = new List<int> { };
            foreach (LineaPedido linea in carrito)
            {
                ids.Add(linea.Id);
            }
            foreach (int i in ids)
            {
                LineaPedido linea2 = db.LineasPedido.Find(i);
                carrito.Remove(linea2);
            }
            return RedirectToAction("Details", "Facturas", factura);
        }


        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
