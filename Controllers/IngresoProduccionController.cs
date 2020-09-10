using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class IngresoProduccionController : Controller
    {
		private IngresoProduccionDAL objBloquesDAL = new IngresoProduccionDAL();
		// GET: IngresoProduccion
		public ActionResult Index()
		{
			var obj = IngresoProduccionDAL.Obtener_Ingresos_Produccion();
			return View(obj);
		}

		public ActionResult CrearIngresoProduccion(int id = 0)
		{
			INGRESO_PRODUCCION objBloques = new INGRESO_PRODUCCION();
			if (id == 0)
			{
				ViewBag.BLCCODIGOI = objBloquesDAL.Lista_Bloques(0);
				ViewBag.VRDCODIGOI = objBloquesDAL.Lista_Variedades(0);
			}
			else
			{
				objBloques = objBloquesDAL.Buscar_Ingreso_Produccion(id);
				ViewBag.BLCCODIGOI = objBloquesDAL.Lista_Bloques(objBloques.BLCCODIGOI);
				ViewBag.VRDCODIGOI = objBloquesDAL.Lista_Variedades(Convert.ToInt32(objBloques.VRDCODIGOI));
			}
			return View(objBloques);
		}

		[HttpPost]
		public ActionResult CrearIngresoProduccion(INGRESO_PRODUCCION objBloques)
		{
			try
			{
				bool respuesta;
				string mensaje = "Registro creado correctamente";
				if (objBloques.INPCODIGOI == 0)
					respuesta = objBloquesDAL.Crear_Ingresos_Produccion(objBloques, true);
				else
				{
					respuesta = objBloquesDAL.Crear_Ingresos_Produccion(objBloques, false);
					mensaje = "Registro editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "IngresoProduccion");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarIngresoProduccion(int id)
		{
			try
			{
				bool respuesta = objBloquesDAL.Eliminar_Ingreso_Produccion(id);
				if (respuesta)
					Request.Flash("success", "Registro eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "IngresoProduccion");
		}
	}
}