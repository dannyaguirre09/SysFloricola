using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class EmpresasController : Controller
    {
		private EmpresasDAL objDAL = new EmpresasDAL();
		// GET: Empresas
		public ActionResult Index()
		{
			var obj = EmpresasDAL.Obtener_Empresas();
			return View(obj);
		}

		public ActionResult CrearEmpresa(int id = 0)
		{
			EMPRESAS obj = new EMPRESAS();
			if (id != 0)
				obj = objDAL.Buscar_Empresa(id);
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearEmpresa(EMPRESAS obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Empresa creada correctamente";
				if (obj.EMPCODIGOI == 0)
					respuesta = objDAL.Crear_Empresa(obj, true);
				else
				{
					respuesta = objDAL.Crear_Empresa(obj, false);
					mensaje = "Empresa editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Empresas");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarEmpresa(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Empresas(id);
				if (respuesta)
					Request.Flash("success", "Empresa eliminada correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Empresas");
		}
	}
}