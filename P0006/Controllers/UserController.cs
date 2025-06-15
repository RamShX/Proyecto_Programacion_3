
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;

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

        [HttpGet]
        public ActionResult Edit(int Id) 
        {
            // Creamos una instancia del modelo EditUserViewModels
            EditUserViewModels model = new EditUserViewModels();


            // Abrimos la conexión a la base de datos
            using (var db = new DBMVCEntities())
            {
                // Buscamos el usuario por su ID
                var user = db.USERS.FirstOrDefault(u => u.ID == Id);

                // Si el usuario existe, asignamos sus valores al modelo
                if (user != null)
                {
                    // Asignamos los valores del usuario al modelo
                    model.Id = user.ID;
                    model.Nombre = user.Nombre;
                    model.Email = user.Email;
                    model.Password = user.Password;
                    model.Edad = user.Edad;
                }
            }
            return View(model); // Retorna la vista Edit con el modelo EditUserViewModels lleno con los datos del usuario seleccionado
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModels model) 
        {
            using (var db = new DBMVCEntities())
            {
                var user = db.USERS.FirstOrDefault(u => u.ID == model.Id);


                // Asigna los valores del modelo al usuario
                user.Nombre = model.Nombre;
                user.Email = model.Email;
                if (model.Password != null || model.Password != "")  // Verifica si la contraseña no está vacía y null
                {
                    user.Password = model.Password;
                }
                user.Edad = model.Edad;

                db.Entry(user).State = EntityState.Modified; // Marca el usuario como modificado
                db.SaveChanges(); // Guarda los cambios en la base de datos


            }

            return Redirect(Url.Content("~/User/Query"));
        }

        [HttpPost]
        public ActionResult Delete(int id) 
        { 
            bool eliminado = false; // Variable para verificar si el usuario fue eliminado

            using (var db = new DBMVCEntities())
            {
                // Busca el usuario por su ID y verifica que su estatus sea activo
                var user = db.USERS.FirstOrDefault(u => u.ID == id);

                // Si el usuario existe, lo "eliminamos"
                if (user != null)
                {
                    user.idEstatus = 3; // Cambiamos el estatus a Borrado (3)
                    db.Entry(user).State = EntityState.Modified; // Marca el usuario como modificado
                    db.SaveChanges(); // Guarda los cambios en la base de datos
                    eliminado = true; // Cambiamos la variable a verdadero porque el usuario fue "eliminado"
                }

                return Json(eliminado ? 1 : 0);
            }
        }
    }
}