using lista_asistencias_merino_espinoza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lista_asistencias_merino_espinoza.Controllers
{
    public class AsistenciasController : Controller
    {
        lista_asistenciasEntitie db = new lista_asistenciasEntitie();
        public ActionResult Index()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst = (from d in db.reuniones
                   select new SelectListItem
                   {
                       Value = d.id_reunion.ToString(),
                       Text = d.nombre
                   }).ToList();

            return View(lst);
        }

        public ActionResult listar(int id = 0)
        {
                List<usuarios> lst;
                lst = (from a in db.asistencias
                       join u in db.usuarios
                       on a.usuario equals u.id_usuario
                       where a.reunion == id
                       orderby u.nombres
                       select u).ToList();

                return View(lst);
        }

        public ActionResult listarUsuarios()
        {
            List<usuarios> lst;
            lst = (from a in db.usuarios
                   select a).ToList();
            
            return View(lst);
        }

        public JsonResult registrar(string id)
        {
            var arreglo = id.Split('/');
            var usuario = Int32.Parse(arreglo[0]);
            var reunion = Int32.Parse(arreglo[1]);
            if (usuario > 0 && reunion > 0)
            {
                List<asistencias> lst;
                lst = (from a in db.asistencias
                       where a.reunion == reunion
                       && a.usuario == usuario
                       select a).ToList();
                if (lst.Count >= 1)
                {
                    return Json(new { result = false, message = "Usuario ya registrado en la reunion!" }, JsonRequestBehavior.AllowGet);
                } else
                {
                    var datos = new asistencias();
                    datos.reunion = reunion;
                    datos.usuario = usuario;

                    db.asistencias.Add(datos);
                    db.SaveChanges();

                    return Json(new { result = true, message = "Usuario registrado correctamente!" }, JsonRequestBehavior.AllowGet);
                }
            } else
            {
                return Json(new { result = false, message = "Error al registra al usuario!" }, JsonRequestBehavior.AllowGet);
            }
        }
        

        public class ElementJsonIntKey
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public JsonResult datos(int id = 0)
        {
            var de = db.reuniones.Where(m => m.id_reunion == id).FirstOrDefault();
            return Json(de, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult datosUsuario(int id)
        {
            var de = db.usuarios.Where(m => m.id_usuario == id).FirstOrDefault();
            return Json(de, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminar(string id)
        {
            var arreglo = id.Split('/');
            var usuario = Int32.Parse(arreglo[0]);
            var reunion = Int32.Parse(arreglo[1]);
            asistencias d = db.asistencias.Where(m => m.usuario == usuario && m.reunion == reunion).FirstOrDefault<asistencias>();
            db.asistencias.Remove(d);
            db.SaveChanges();

            return Json(new { result = true, message = "Datos eliminados correctamente!" }, JsonRequestBehavior.AllowGet);
        }
    }
}