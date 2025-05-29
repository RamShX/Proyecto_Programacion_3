
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

        
        // GET: User/Add
        [HttpGet]
        public ActionResult Add() 
        { 
            return View(); // Retorna la vista Add sin ningún modelo
        }

        // POST: User/Add
        [HttpPost]
        public ActionResult Add(AddUserViewModels model) 
        {
            // Verifica si el modelo es inválido (campos vacíos, formatos incorrectos, etc.)
            // Si no es válido, se retorna la vista con el modelo actual para mostrar los mensajes de error al usuario
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            using (var db = new DBMVCEntities())
            {
                // Creamos un nuevo usuario
                USER user = new USER
                {
                    Nombre = model.Nombre,
                    Email = model.Email,
                    Password = model.Password,
                    Edad = model.Edad,
                    idEstatus = 1 // Asignamos el estatus activo
                };

                // Agregamos el usuario a la base de datos
                db.USERS.Add(user);
                // Guardamos los cambios en la base de datos
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/User/Query"));// Redirige a la acción Query después de agregar el usuario exitosamente


        }

    }
}