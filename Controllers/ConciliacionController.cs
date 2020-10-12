using SysFloricola.Filter;
using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
	[SecurityFilter]
	public class ConciliacionController : Controller
    {
		private ConciliacionDAL objDal = new ConciliacionDAL();

        // GET: Conciliacion
        public ActionResult Index(int id=0)
        {
			if (id == 1)
				Request.Flash("success", "Registros Creados Correctamente");
			else if (id == 2)
				Request.Flash("danger", "No ha sido posible crear el preenvío");

			return View(objDal.Obtener_Conciliaciones());
        }

		public ActionResult ModalCrearConciliacion()
		{
			return View(objDal.Lista_Preenvios());
		}

		[HttpPost]
		public JsonResult CrearConciliacion(List<CONCILIACION> listaConciliaciones)
		{
			bool estado = false;
			try
			{
				estado = objDal.Crear_Conciliaciones(listaConciliaciones);
			}
			catch (Exception)
			{
				estado = false;
			}
			return Json(new { estado = estado }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult EditarConciliacion(int id)
		{
			return View(objDal.Buscar_Conciliacion(id));
		}

		[HttpPost]
		public ActionResult EditarConciliacion(CONCILIACION obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Conciliación editada correctamente";

				respuesta = objDal.Editar_Conciliacion(obj);
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Conciliacion");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.Id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarConciliacion(int id)
		{
			try
			{
				bool respuesta = objDal.Eliminar_Conciliacion(id);
				if (respuesta)
					Request.Flash("success", "Conciliación eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Conciliacion");
		}

	}
}