using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class CausasController : Controller
    {
		private CausasDAL objDAL = new CausasDAL();
		// GET: Causas
		public ActionResult Index()
		{
			var obj = CausasDAL.Obtener_Causas();
			return View(obj);
		}

		public ActionResult CrearCausa(int id = 0)
		{
			CAUSAS obj = new CAUSAS();
			if (id != 0)
				obj = objDAL.Buscar_Causas(id);
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearCausa(CAUSAS obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Causa creada correctamente";
				if (obj.CAUCODIGOI == 0)
					respuesta = objDAL.Crear_Causas(obj, true);
				else
				{
					respuesta = objDAL.Crear_Causas(obj, false);
					mensaje = "Causa editada correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Causas");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarCausa(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Causa(id);
				if (respuesta)
					Request.Flash("success", "Causa eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Causas");
		}
	}
}