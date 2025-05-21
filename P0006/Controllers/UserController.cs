
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

using P0006.Models;
using P0006.Models.ViewModels;

namespace P0006.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Query()
        {
            // Creamos una lista de QueryViewModels e inicializamos en null
            List<QueryViewModels> list = null;

            // Abrimos la conexión a la base de datos
            using (var db = new DBMVCEntities())
            {
                list = (from u in db.USERS
                        where u.idEstatus == 1
                        orderby u.Email

                        // seleccionamos los campos que queremos mostrar en la vista
                        // mapeamos el modelo que se llama QueryViewModels a la vista
                        select new QueryViewModels
                        {
                            Id = u.ID,
                            _Email = u.Email,
                            _Edad = u.Edad
                        }).ToList();
            }
            return View(list); //Enviamos la lista a la vista llamada Query
        }
    }
}