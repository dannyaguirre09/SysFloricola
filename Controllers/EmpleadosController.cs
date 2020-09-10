using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SysFloricola.Controllers
{
    public class EmpleadosController : Controller
    {
		private EmpleadosDAL objEmpleadoDAL = new EmpleadosDAL();
        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = EmpleadosDAL.Obtener_Empleados();
            return View(empleados);
        }

		public ActionResult CrearEmpleado(int eplCodigoI = 0)
		{
			EMPLEADOS objEmpleado = new EMPLEADOS();
			if(eplCodigoI == 0)
			{
				ViewBag.EMPCODIGOI = objEmpleadoDAL.Lista_Empresas(0);
			}
			else
			{
				objEmpleado = objEmpleadoDAL.Buscar_Empleado(eplCodigoI);
				ViewBag.EMPCODIGOI = objEmpleadoDAL.Lista_Empresas(objEmpleado.EMPCODIGOI);
			}
			return View(objEmpleado);
		}

		[HttpPost]
		public ActionResult CrearEmpleado(EMPLEADOS objEmpleado)
		{
			try
			{
				bool respuesta;
				string mensaje = "Empleado creado correctamente";
				if (objEmpleado.EPLCODIGOI == 0 )
					respuesta = objEmpleadoDAL.Crear_Empleado(objEmpleado, true);
				else
				{
					respuesta = objEmpleadoDAL.Crear_Empleado(objEmpleado, false);
					mensaje = "Empleado editado correctamente"; 
				}
				if (respuesta)
					Request.Flash("success", mensaje);
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Empleados");
		}

		public ActionResult Confirmacion(int eplCodigoI)
		{
			ViewBag.eplCodigoI = eplCodigoI;
			return View();
		}

		[HttpPost]
		public ActionResult EliminarEmpleado(int eplCodigoI)
		{
			try
			{
				bool respuesta = objEmpleadoDAL.Eliminar_Empleado(eplCodigoI);
				if (respuesta)
					Request.Flash("success", "Empleado eliminado correctamente");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "Empleados");
		}

	}
}