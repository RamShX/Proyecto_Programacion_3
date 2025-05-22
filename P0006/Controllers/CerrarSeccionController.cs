using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P0006.Controllers
{
    public class CerrarSeccionController : Controller
    {
        // GET: CerrarSeccion
        public ActionResult Logoff()
        {
            // Se cierra la sesión del usuario.
            Session["Usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}