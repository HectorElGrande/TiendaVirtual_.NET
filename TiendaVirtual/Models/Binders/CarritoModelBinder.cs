using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Models.Binders
{
    public class CarritoModelBinder : IModelBinder
    {
        private static string KEY_CARRITO = "KEY:1234";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CarritoCompra cc = (CarritoCompra)controllerContext.HttpContext.Session[KEY_CARRITO];

            if (cc == null)
            {
                cc = new CarritoCompra();
                controllerContext.HttpContext.Session[KEY_CARRITO] = cc;
            }
            return cc;
        }
    }
}