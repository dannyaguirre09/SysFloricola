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
	public class UnidadesController : Controller
    {
		private UnidadesDAL objDAL = new UnidadesDAL();
		// GET: Unidades
		public ActionResult Index()
		{
			var unidades = UnidadesDAL.Obtener_Unidades();
			return View(unidades);
		}

		public ActionResult CrearUnidades(int id = 0)
		{
			UNIDADES obj = new UNIDADES();
			if (id != 0)
				obj = objDAL.Buscar_Unidades(id);
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearUnidades(UNIDADES obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Unidad creada correctamente";
				if (obj.UNDCODIGOI == 0)
					respuesta = objDAL.Crear_Unidades(obj, true);
				else
				{
					respuesta = objDAL.Crear_Unidades(obj, false);
					mensaje = "Unidad editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Unidades");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarUnidad(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Unidades(id);
				if (respuesta)
					Request.Flash("success", "Unidad eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Unidades");
		}
	}
}