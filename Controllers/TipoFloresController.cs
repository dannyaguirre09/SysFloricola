using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class TipoFloresController : Controller
    {
		private TipoFloresDAL objDAL = new TipoFloresDAL();
		// GET: Tipos_Flores
		public ActionResult Index()
		{
			var obj = TipoFloresDAL.Obtener_Tipos_Flores();
			return View(obj);
		}

		public ActionResult CrearTipoFlores(int id = 0)
		{
			TIPOS_FLORES obj = new TIPOS_FLORES();
			if (id != 0)
				obj = objDAL.Buscar_Tipo_flores(id);
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearTipoFlores(TIPOS_FLORES obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Tipo de flor creado correctamente";
				if (obj.TPFCODIGOI == 0)
					respuesta = objDAL.Crear_Tipos_flores(obj, true);
				else
				{
					respuesta = objDAL.Crear_Tipos_flores(obj, false);
					mensaje = "Tipo de flor editada correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "TipoFlores");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarTipoFlor(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Tipo_Flores(id);
				if (respuesta)
					Request.Flash("success", "Tipo de flor eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "TipoFlores");
		}
	}
}