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
    [Authorize(Users = "admin@gmail.com")]
    public class ProductosController : Controller
    {
        private VirtualShopModelContainer db = new VirtualShopModelContainer();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Imagen,Categoria,Cantidad,Precio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if(db.Productos.FirstOrDefault(p => p.Nombre == producto.Nombre) == null) { 
                Categoria cat = db.Categorias.FirstOrDefault(u => u.Nombre == producto.Categoria.Nombre);
                producto.Categoria = cat;
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Imagen,Categoria,Cantidad,Precio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if(db.Productos.FirstOrDefault(u => u.Id == producto.Id).Nombre == producto.Nombre ||
                    db.Productos.FirstOrDefault(u => u.Nombre == producto.Nombre) == null)
                {
                    Categoria cat = db.Categorias.FirstOrDefault(u => u.Nombre == producto.Categoria.Nombre);
                    Producto prod = db.Productos.FirstOrDefault(u => u.Id == producto.Id);
                    prod.Categoria = cat;
                    prod.Nombre = producto.Nombre;
                    prod.Imagen = producto.Imagen;
                    prod.Cantidad = producto.Cantidad;
                    prod.Precio = producto.Precio;
                    Producto productoStock = null;
                    if (db.Stocks.Count() > 0)
                    {
                        productoStock = db.Stocks.First().Producto.FirstOrDefault(p => p.Nombre == prod.Nombre);
                    }
                    if(prod.Cantidad>1 && productoStock != null)
                    {
                        db.Stocks.First().Producto.Remove(productoStock);
                    }
                    if (prod.Cantidad < 2 && db.Stocks.First().Producto.FirstOrDefault(p => p.Nombre == prod.Nombre) == null)
                    {
                        Stock stock = db.Stocks.First();
                        stock.Producto.Add(prod);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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
