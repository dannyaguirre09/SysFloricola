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
	public class VariedadesController : Controller
    {
		private VariedadesDAL objDAL = new VariedadesDAL();
		// GET: variedades
		public ActionResult Index()
		{
			var obj = VariedadesDAL.Obtener_Variedades();
			return View(obj);
		}

		public ActionResult CrearVariedad(int id = 0)
		{
			VARIEDADES obj = new VARIEDADES();
			if (id != 0)
				obj = objDAL.Buscar_Variedad(id);
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearVariedad(VARIEDADES obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Variedad creada correctamente";
				if (obj.VRDCODIGOI == 0)
					respuesta = objDAL.Crear_Variedad(obj, true);
				else
				{
					respuesta = objDAL.Crear_Variedad(obj, false);
					mensaje = "Variedad editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Variedades");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarVariedad(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Variedad(id);
				if (respuesta)
					Request.Flash("success", "Variedad eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Variedades");
		}
	}
}