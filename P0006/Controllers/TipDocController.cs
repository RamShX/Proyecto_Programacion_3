using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using P0006.Models;
using P0006.Models.ViewModels.TipoDoc;
using P0006.Models.ViewModels;

namespace P0006.Controllers
{
    public class TipDocController : Controller
    {
        // GET: TipDoc
        public ActionResult TipDocQuery()
        {
            List<TipDocQuerryViewModels> list = null;

            using (var db = new DBMVCEntities())
            {
                list = (from t in db.TIPDOCs
                        where t.ESTATUS == 1
                        orderby t.DESCRIPCION
                        // seleccionamos los campos que queremos mostrar en la vista
                        // mapeamos el modelo que se llama TipDocQuerryViewModels a la vista
                        select new TipDocQuerryViewModels
                        {
                            ID = t.ID,
                            TIPDOC = t.TIPDOC1,
                            ORIGEN = t.ORIGEN,
                            DESCRIPCION = t.DESCRIPCION
                        }).ToList();
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult DeleteTipDoc(int id)
        {
            bool eliminado = false;
            using (var db = new DBMVCEntities())
            {
                // Buscamos el registro por su ID
                var tipDoc = db.TIPDOCs.FirstOrDefault(t => t.ID == id);

                if (tipDoc != null)
                {
                    // Marcamos el registro como eliminado
                    tipDoc.ESTATUS = 3;
                    db.Entry(tipDoc).State = EntityState.Modified;
                    db.SaveChanges(); // Guardamos los cambios en la base de datos
                    eliminado = true;
                }
            }
            return Json(eliminado ? 1:0); // Redirigimos a la acción TipDocQuery después de eliminar
        }


        [HttpGet]
        public ActionResult AddTipDoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTipDoc(AddTipDocViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Si el modelo no es válido, retornamos la vista con el modelo actual para mostrar los mensajes de error

            }

            using (var db = new DBMVCEntities())
            {
                // Creamos una nueva instancia de TIPDOC
                var tipDoc = new TIPDOC
                {

                    TIPDOC1 = model.TIPDOC,
                    ORIGEN = model.ORIGEN,
                    DESCRIPCION = model.DESCRIPCION,
                    CTADEBITO = model.CtaDebito,
                    CTACREDITO = model.CtaCredito,
                    ESTATUS = 1 // Asignamos un estado activo

                };

                // Agregamos el nuevo registro a la base de datos
                db.TIPDOCs.Add(tipDoc);
                db.SaveChanges(); // Guardamos los cambios en la base de datos
            }
            return Redirect(Url.Content("~/TipDoc/TipDocQuery"));
        }

        [HttpGet]
        public ActionResult EditTipDoc(int id)
        {
            var model = new EditTipDocViewModels();

            using (var db = new DBMVCEntities())
            {
                // Buscamos el registro por su ID
                var tipDoc = db.TIPDOCs.FirstOrDefault(t => t.ID == id);
                if (tipDoc != null)
                {
                    // Asignamos los valores del registro al modelo
                    model.Id = tipDoc.ID;
                    model.TIPDOC = tipDoc.TIPDOC1;
                    model.ORIGEN = tipDoc.ORIGEN;
                    model.DESCRIPCION = tipDoc.DESCRIPCION;
                    model.CtaDebito = tipDoc.CTADEBITO;
                    model.CtaCredito = tipDoc.CTACREDITO;
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult EditTipDoc(EditTipDocViewModels model)
        {
            using (var db = new DBMVCEntities())
            {
                var TipDocEdit = db.TIPDOCs.FirstOrDefault(u => u.ID == model.Id);


                // Asigna los valores del modelo al usuario
                TipDocEdit.TIPDOC1 = model.TIPDOC;
                TipDocEdit.ORIGEN = model.ORIGEN;
                TipDocEdit.DESCRIPCION = model.DESCRIPCION;
                TipDocEdit.CTADEBITO = model.CtaDebito;
                TipDocEdit.CTACREDITO = model.CtaCredito;
                TipDocEdit.ESTATUS = 1; // Asignamos un estado activo

                db.Entry(TipDocEdit).State = EntityState.Modified; // Marca el usuario como modificado
                db.SaveChanges(); // Guarda los cambios en la base de datos


            }

            return Redirect(Url.Content("~/User/Querry"));
        }

    }
}