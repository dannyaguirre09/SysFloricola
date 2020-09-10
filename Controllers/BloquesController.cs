using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class BloquesController : Controller
    {
		private BloquesDAL objBloquesDAL = new BloquesDAL();
		// GET: Bloques
		public ActionResult Index()
		{
			var bloques = objBloquesDAL.Obtener_Bloques();
			return View(bloques);
		}

		public ActionResult CrearBloque(int blqCodigoI = 0)
		{
			BLOQUES objBloques = new BLOQUES();
			if (blqCodigoI == 0)
			{
				ViewBag.FNCCODIGOI = objBloquesDAL.Lista_Fincas(0);
			}
			else
			{
				objBloques = objBloquesDAL.Buscar_Bloque(blqCodigoI);
				ViewBag.FNCCODIGOI = objBloquesDAL.Lista_Fincas(objBloques.FNCCODIGOI);
			}
			return View(objBloques);
		}

		[HttpPost]
		public ActionResult CrearBloque(BLOQUES objBloques)
		{
			try
			{
				bool respuesta;
				string mensaje = "Bloque creado correctamente";
				if (objBloques.BLQCODIGOI == 0)
					respuesta = objBloquesDAL.Crear_Bloque(objBloques, true);
				else
				{
					respuesta = objBloquesDAL.Crear_Bloque(objBloques, false);
					mensaje = "Bloque editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Bloques");
		}

		public ActionResult Confirmacion(int blqCodigoI)
		{
			ViewBag.blqCodigoI = blqCodigoI;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarBloque(int blqCodigoI)
		{
			try
			{
				bool respuesta = objBloquesDAL.Eliminar_Bloque(blqCodigoI);
				if (respuesta)
					Request.Flash("success", "Bloque eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Bloques");
		}
	}
}

