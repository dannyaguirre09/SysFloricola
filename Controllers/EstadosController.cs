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
	public class EstadosController : Controller
    {
		private EstadosDAL objDAL = new EstadosDAL();
		// GET: Estados
		public ActionResult Index()
		{
			var estados = EstadosDAL.Obtener_Estados();
			return View(estados);
		}

		public ActionResult CrearEstado(int id = 0)
		{
			ESTADOS obj = new ESTADOS();
			if (id == 0)
			{
				ViewBag.PAICODIGOI = objDAL.Lista_Paises(0);
			}
			else
			{
				obj = objDAL.Buscar_Estado(id);
				ViewBag.PAICODIGOI = objDAL.Lista_Paises(obj.PAICODIGOI);
			}
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearEstado(ESTADOS obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Estado creado correctamente";
				if (obj.ESTCODIGOI == 0)
					respuesta = objDAL.Crear_Estadp(obj, true);
				else
				{
					respuesta = objDAL.Crear_Estadp(obj, false);
					mensaje = "Estado editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Estados");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarEstado(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Estado(id);
				if (respuesta)
					Request.Flash("success", "Estado eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Estados");
		}
	}
}