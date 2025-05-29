using P0006.Controllers;
using P0006.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P0006.Filters
{
	public class VerificarSession : ActionFilterAttribute
    {
        // Este método se ejecuta antes de que se ejecute la acción del controlador
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var miSession = (USER)HttpContext.Current.Session["Usuario"];

            // Verifica si la sesión del usuario es nula
            if (miSession == null)
            {
                if (filterContext.Controller is AccesoController == false)
                {
                    // Si la sesión es nula y el controlador no es AccesoController, redirige al usuario a la página de acceso
                    filterContext.HttpContext.Response.Redirect("~/Acceso/Login");
                }
                
            }
            else
            {
                if (filterContext.Controller is AccesoController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
	
}
