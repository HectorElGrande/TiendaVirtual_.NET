using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class CarritoCompraController : Controller
    {
        // GET: CarritoCompra
        public ActionResult Index(CarritoCompra carrito)
        {
            return View(carrito);
        }

        // GET: CarritoCompra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarritoCompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoCompra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoCompra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarritoCompra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoCompra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarritoCompra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
