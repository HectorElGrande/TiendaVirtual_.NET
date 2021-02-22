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
    public class CategoriasController : Controller
    {
        private VirtualShopModelContainer db = new VirtualShopModelContainer();

        public ActionResult AddToCart(int id, int cantidad, CarritoCompra carrito)
        {
            List<Producto> productos = db.Productos.ToList();
            Producto p = productos.Find(producto => producto.Id == id);
            LineaPedido l = carrito.Find(linea => linea.Producto.Nombre == p.Nombre);
            if(l == null)
            {
                LineaPedido lineaPedido = new LineaPedido();
                lineaPedido.Cantidad = cantidad;
                lineaPedido.Producto = p;
                carrito.Add(lineaPedido);
            }
            else
            {
                l.Cantidad = cantidad;
            }
            return RedirectToAction("Index", "CarritoCompra");
        }

        // GET: Categorias
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/CategoryProducts/5
        public ActionResult CategoryProducts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria.Producto == null)
            {
                return HttpNotFound();
            }
            return View(categoria.Producto.ToList());
        }

        // GET: Categorias/CategoryProductsDetails/5
        public ActionResult CategoryProductsDetails(int? id)
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

        // GET: Categorias/Details/5
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (db.Categorias.FirstOrDefault(c => c.Nombre == categoria.Nombre) == null)
                {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Create", "Productos");
                }
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (db.Categorias.FirstOrDefault(u => u.Id == categoria.Id).Nombre == categoria.Nombre ||
                    db.Categorias.FirstOrDefault(u => u.Nombre == categoria.Nombre) == null)
                {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@gmail.com")]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            var productos = categoria.Producto;
            List<int> ids = new List<int> { };
            foreach(Producto producto in productos)
            {
                Producto prod = db.Productos.FirstOrDefault(p => p.Nombre == producto.Nombre);
                prod.Categoria = null;
                ids.Add(prod.Id);
            }
            db.Categorias.Remove(categoria);

            foreach (int i in ids)
            {
                Producto producto2 = db.Productos.Find(i);
                db.Productos.Remove(producto2);
            }
     
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
