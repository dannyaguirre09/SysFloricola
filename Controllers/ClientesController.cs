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
	public class ClientesController : Controller
    {
		private ClientesDAL objDAL = new ClientesDAL();
		// GET: Clientes
		public ActionResult Index()
		{
			var obj = ClientesDAL.Obtener_Clientes();
			return View(obj);
		}

		public ActionResult CrearCliente(int id = 0)
		{
			CLIENTES obj = new CLIENTES();
			if (id == 0)
			{
				ViewBag.ESTCODIGOI = objDAL.Lista_Estado(0);
			}
			else
			{
				obj = objDAL.Buscar_Cliente(id);
				ViewBag.ESTCODIGOI = objDAL.Lista_Estado(obj.CLNCODIGOI);
			}
			return View(obj);
		}

		[HttpPost]
		public ActionResult CrearCliente(CLIENTES obj)
		{
			try
			{
				bool respuesta;
				string mensaje = "Cliente creado correctamente";
				if (obj.CLNCODIGOI == 0)
					respuesta = objDAL.Crear_Cliente(obj, true);
				else
				{
					respuesta = objDAL.Crear_Cliente(obj, false);
					mensaje = "Cliente editado correctamente";
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Clientes");
		}

		public ActionResult Confirmacion(int id)
		{
			ViewBag.id = id;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarCliente(int id)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_Cliente(id);
				if (respuesta)
					Request.Flash("success", "Cliente eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Clientes");
		}
	}
}