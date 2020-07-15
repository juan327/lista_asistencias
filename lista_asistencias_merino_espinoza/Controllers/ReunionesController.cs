using lista_asistencias_merino_espinoza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lista_asistencias_merino_espinoza.Controllers
{
    public class ReunionesController : Controller
    {
        lista_asistenciasEntitie db = new lista_asistenciasEntitie();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listar()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<reuniones> lt = db.reuniones.OrderBy(u => u.nombre).ToList<reuniones>();
            return Json(new { data = lt }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult modificar(reuniones ob)
        {
            if (ob.id_reunion > 0)
            {
                reuniones usu = db.reuniones.Where(m => m.id_reunion == ob.id_reunion).FirstOrDefault<reuniones>();
                usu.id_reunion = ob.id_reunion;
                usu.nombre = ob.nombre;
                usu.descripcion = ob.descripcion;
                usu.fecha = ob.fecha;

                db.SaveChanges();
                return Json(new { result = true, message = "Datos editados correctamente!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                reuniones usu = new reuniones();
                usu.nombre = ob.nombre;
                usu.descripcion = ob.descripcion;
                usu.fecha = ob.fecha;

                db.reuniones.Add(ob);
                db.SaveChanges();
                return Json(new { result = true, message = "Datos registrados correctamente!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult editar(int id)
        {
            var de = db.reuniones.Where(m => m.id_reunion == id).FirstOrDefault();
            return Json(de, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminar(int id)
        {
            reuniones d = db.reuniones.Where(m => m.id_reunion == id).FirstOrDefault<reuniones>();
            db.reuniones.Remove(d);
            db.SaveChanges();

            return Json(new { result = true, message = "Datos eliminados correctamente!" }, JsonRequestBehavior.AllowGet);
        }
    }
}