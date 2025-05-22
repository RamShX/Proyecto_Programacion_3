using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using P0006.Models;

namespace P0006.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Enter(string usuario, string password)
        {
            using(var db = new DBMVCEntities()) 
            {
                // Select de la tabla USERS donde el email y password coincidan y el estatus sea 1, o sea, activo.
                var read = from u in db.USERS
                           where u.Email == usuario
                           && u.Password == password
                           && u.idEstatus == 1
                           select u;

                // Se hace un conteo de los registros que cumplen con la consulta anterior.
                // Preguntamos si encontramos el registro.
                if (read.Count() > 0) 
                {
                    // Si el usuario existe, se guarda en la session y se retorna 1 a la vista para indicar que el login fue exitoso.
                    Session["Usuario"] = read.First();
                    return Content("1");
                }
                else
                {
                    // Si no encuentra el usuario, retorna un mensaje de error.
                    return Content("Revisa el usuario y/o password :("); 
                }
            }
        }
    }
}