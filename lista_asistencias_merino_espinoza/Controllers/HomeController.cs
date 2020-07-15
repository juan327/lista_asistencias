using lista_asistencias_merino_espinoza.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lista_asistencias_merino_espinoza.Controllers
{
    public class HomeController : Controller
    {
        lista_asistenciasEntitie db = new lista_asistenciasEntitie();
        public ActionResult Index()
        {
;            return View();
        }

        public JsonResult listar()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<usuarios> lt = db.usuarios.OrderBy(u => u.nombres).ToList<usuarios>();
            return Json(new { data = lt }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult modificar(usuarios ob)
        {
            if (ob.id_usuario > 0)
            {
                usuarios usu = db.usuarios.Where(m => m.id_usuario == ob.id_usuario).FirstOrDefault<usuarios>();
                usu.id_usuario = ob.id_usuario;
                usu.nombres = ob.nombres;
                usu.apellidos = ob.apellidos;
                usu.correo = ob.correo;
                
                db.SaveChanges();
                return Json(new { result = true, message = "Datos editados correctamente!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                usuarios usu = new usuarios();
                usu.nombres = ob.nombres;
                usu.apellidos = ob.apellidos;
                usu.correo = ob.correo;

                db.usuarios.Add(ob);
                db.SaveChanges();
                return Json(new { result = true, message = "Datos registrados correctamente!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult editar(int id)
        {
            var de = db.usuarios.Where(m => m.id_usuario == id).FirstOrDefault();
            return Json(de, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminar(int id)
        {
            usuarios d = db.usuarios.Where(m => m.id_usuario == id).FirstOrDefault<usuarios>();
            db.usuarios.Remove(d);
            db.SaveChanges();

            return Json(new { result = true, message = "Datos eliminados correctamente!" }, JsonRequestBehavior.AllowGet);
        }
    }
}