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
	public class PaisesController : Controller
    {
		private PaisesDAL objPaisesDAL = new PaisesDAL();
		// GET: Paise
		public ActionResult Index()
		{
			var paises = PaisesDAL.Obtener_Paises();
			return View(paises);
		}

		public ActionResult CrearPais(int paiCodigoI = 0)
		{
			PAICES objPais = new PAICES();
			if (paiCodigoI != 0)
			{
				objPais = objPaisesDAL.Buscar_Pais(paiCodigoI);
			}
			return View(objPais);
		}

		[HttpPost]
		public ActionResult CrearPais(PAICES objpais)
		{
			try
			{
				bool respuesta;
				string mensaje = "País creado correctamente";
				if (objpais.PAICODIGOI == 0)
					respuesta = objPaisesDAL.Crear_Pais(objpais, true);
				else
				{
					respuesta = objPaisesDAL.Crear_Pais(objpais, false);
					mensaje = "País editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Paises");
		}

		public ActionResult Confirmacion(int paiCodigoI)
		{
			ViewBag.paiCodigoI = paiCodigoI;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarPais(int paiCodigoI)
		{
			try
			{
				bool respuesta = objPaisesDAL.Eliminar_Pais(paiCodigoI);
				if (respuesta)
					Request.Flash("success", "Pais eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Paises");
		}
	}
}