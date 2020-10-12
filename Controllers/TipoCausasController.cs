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
	public class TipoCausasController : Controller
    {
		private TipoCausasDAL objDAL = new TipoCausasDAL();
		// GET: TipoCausas
		public ActionResult Index()
		{
			var obj = TipoCausasDAL.Obtener_Tipo_Causas();
			return View(obj);
		}

		public ActionResult CrearTipoCausa(int id = 0)
		{
			TIPO_CAUSAS obj = new TIPO_CAUSAS();
			if (id == 0)
			{
				ViewBag.CAUCODIGOI = objDAL.Lista_Causas(0);
			}
			else
			{
				obj = objDAL.Buscar_Tipo_Causa(id);
				ViewBag.CAUCODIGOI = objDAL.Lista_Causas(obj.CAUCODIGOI);
			}
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearTipoCausa(TIPO_CAUSAS obj)
		{
			try
			{
				int respuesta;
				string mensaje = "Tipo causa creada correctamente";
				if (obj.TPCCODIGOI == 0)
					respuesta = objDAL.Crear_Tipo_Causa(obj, true);
				else
				{
					respuesta = objDAL.Crear_Tipo_Causa(obj, false);
					mensaje = "Tipo causa editado correctamente";
				}
				if (respuesta == 1)
					Request.Flash("success", mensaje);
				else if(respuesta ==2)
					Request.Flash("warning", "No es posible modificar la causa ya que tiene registros asociados");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "TipoCausas");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarTipoCausa(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Tipo_Causa(id);
				if (respuesta)
					Request.Flash("success", "Tipo Causa eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "TipoCausas");
		}
	}
}