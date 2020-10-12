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
	public class FincasController : Controller
    {
		private FincasDAL objFincasDAL = new FincasDAL();
		// GET: Empleados
		public ActionResult Index()
		{
			var fincas = FincasDAL.Obtener_Fincas();
			return View(fincas);
		}

		public ActionResult CrearFinca(int fncCodigoI = 0)
		{
			FINCAS objFincas = new FINCAS();
			if (fncCodigoI == 0)
			{
				ViewBag.EMPCODIGOI = objFincasDAL.Lista_Empresas(0);
			}
			else
			{
				objFincas = objFincasDAL.Buscar_Finca(fncCodigoI);
				ViewBag.EMPCODIGOI = objFincasDAL.Lista_Empresas(objFincas.EMPCODIGOI);
			}
			return View(objFincas);
		}

		[HttpPost]
		public ActionResult CrearFinca(FINCAS objFincas)
		{
			try
			{
				bool respuesta;
				string mensaje = "Finca creada correctamente";
				if (objFincas.FNCCODIGOI == 0)
					respuesta = objFincasDAL.Crear_Finca(objFincas, true);
				else
				{
					respuesta = objFincasDAL.Crear_Finca(objFincas, false);
					mensaje = "Finca editada correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Fincas");
		}

		public ActionResult Confirmacion(int fncCodigoI)
		{
			ViewBag.fncCodigoI = fncCodigoI;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarFinca(int fncCodigoI)
		{
			try
			{
				bool respuesta = objFincasDAL.Eliminar_Finca(fncCodigoI);
				if (respuesta)
					Request.Flash("success", "FInca eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Fincas");
		}
	}
}