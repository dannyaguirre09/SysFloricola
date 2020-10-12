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
	public class BloqueVariedadesController : Controller
    {
		private BloqueVariedadesDAL objBloquesDAL = new BloqueVariedadesDAL();
		// GET: BloquesVariedades
		public ActionResult Index()
		{
			var bloques = BloqueVariedadesDAL.Obtener_Bloques_Variedades();
			return View(bloques);
		}

		public ActionResult CrearBloqueVariedad(int id = 0)
		{
			BLOQUES_VARIEDADES objBloques = new BLOQUES_VARIEDADES();
			if (id == 0)
			{
				ViewBag.BLQCODIGOI = objBloquesDAL.Lista_Bloques(0);
				ViewBag.VRDCODIGOI = objBloquesDAL.Lista_Variedades(0);
				ViewBag.identificador = 0;
			}
			else
			{
				objBloques = objBloquesDAL.Buscar_Bloque_Variedad(id);
				ViewBag.BLQCODIGOI = objBloquesDAL.Lista_Bloques(objBloques.BLQCODIGOI);
				ViewBag.VRDCODIGOI = objBloquesDAL.Lista_Variedades(objBloques.VRDCODIGOI);
				ViewBag.identificador = objBloques.BLVNUMEROCAMA;
			}
			return View(objBloques);
		}

		[HttpPost]
		public ActionResult CrearBloqueVariedad(BLOQUES_VARIEDADES objBloques)
		{
			try
			{
				bool respuesta;
				string mensaje = "Registro creado correctamente";
				if (objBloques.BLVCODIGOI == 0)
					respuesta = objBloquesDAL.Crear_Bloque_Variedades(objBloques, true);
				else
				{
					respuesta = objBloquesDAL.Crear_Bloque_Variedades(objBloques, false);
					mensaje = "Registro editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "BloqueVariedades");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarBloqueVariedad(int id)
		{
			try
			{
				bool respuesta = objBloquesDAL.Eliminar_Bloque_variedad(id);
				if (respuesta)
					Request.Flash("success", "Registro eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "BloqueVariedades");
		}

		public JsonResult NumeroCamas(int codigoBloque)
		{
			List<NumCamas> lista = objBloquesDAL.Numero_Camas(codigoBloque);
			return Json(lista, JsonRequestBehavior.AllowGet);
		}
	}
}