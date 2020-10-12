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
	public class DetalleFlorClasificadaController : Controller
    {
		private DetalleFlorClasificadaDAL objDAL = new DetalleFlorClasificadaDAL();

		// GET: DetalleFlorClasificada
		public ActionResult Index()
        {
			List<PROCESO_CLASIFICACION> lista = objDAL.Lista_Proceso_Clasificacion();
            return View(lista);
        }

		public ActionResult CrearControlDiario(int proceso = 0)
		{
			DateTime fecha = DateTime.Now;
			try
			{
				ViewBag.TPFCODIGOI = objDAL.Lista_Tipo_flor(0);
				ViewBag.idProceso = proceso;

				if (proceso != 0)
					fecha = objDAL.retornarFecha(proceso);
					
					
				ViewBag.Fecha = fecha.ToShortDateString();
			}
			catch (Exception)
			{
				ViewBag.Fecha = fecha;
			}
			return View();
		}

		[HttpPost]
		public JsonResult CrearControlDiario(List<Detalle> listaDetalle, int idTipoFlor, int idUnidad, string fecha, string observacion, bool nuevo)
		{
			int respuesta = 0;
			try
			{
				if (listaDetalle.Count > 0)
				{
					if (nuevo)
						respuesta = objDAL.Crear_Detalle_Flor_Clasificada(listaDetalle, observacion, Convert.ToDateTime(fecha), idTipoFlor, idUnidad);
					else
						respuesta = objDAL.Editar_Detalle_Flor_Clasificada(listaDetalle, Convert.ToDateTime(fecha) );
				}
			}
			catch (Exception)
			{
				respuesta = 0;
			}

			return Json(new { res = respuesta }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult listaTallos (int id)
		{
			List<FlorTallo> lista = new List<FlorTallo>();
			string mensaje = "Correcto";
			bool estado = true;
			try
			{
				lista = objDAL.Lista_tallos(id);
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
				estado = false;
			}

			return Json(new { lista = lista, mensaje = mensaje, estado = estado }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult listaUnidades(int id, int idUnidad )
		{
			List<FlorTallo> lista = new List<FlorTallo>();
			
			string mensaje = "Correcto";
			bool estado = true;
			try
			{
				
				lista = objDAL.Lista_unidades(id, idUnidad);
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
				estado = false;
			}

			return Json(new {lista = lista, mensaje = mensaje, estado = estado }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult listaVariedades(int idTipoFlor, int idUnidad, int idProceso)
		{
			List<VARIEDADES> lista = new List<VARIEDADES>();
			List<VARIEDADES> l = new List<VARIEDADES>();
			List<Detalle> listaDetalle = new List<Detalle>();
			string mensaje = "Correcto";
			DateTime fecha;
			bool estado = true;
			try
			{
				if (idProceso == 0)
					fecha = DateTime.Now;
				else
					fecha = objDAL.retornarFecha(idProceso);
				listaDetalle = objDAL.listaDetalle(idTipoFlor, idUnidad, Convert.ToDateTime(fecha));
				lista = objDAL.listaVariedades();
				foreach (var item in lista)
				{
					VARIEDADES obj = new VARIEDADES();
					obj.VRDCODIGOI = item.VRDCODIGOI;
					obj.VRDNOMBREC = item.VRDNOMBREC;
					l.Add(obj);
				}
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
				estado = false;
			}

			return Json(new { lista = l, listaDetalle = listaDetalle, mensaje = mensaje, estado = estado }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult EliminarControlDiario(int idProceso)
		{
			PROCESO_CLASIFICACION obj = objDAL.Buscar_Proceso_Clasificacion(idProceso);
			return View(obj);
		}

		[HttpPost]
		public ActionResult EliminarControlDiario(PROCESO_CLASIFICACION objProceso)
		{
			try
			{
				bool respuesta = objDAL.Eliminar_ProcesoDiario(objProceso);
				if (respuesta)
					Request.Flash("success", "Registro eliminado correctamente");
				else
					Request.Flash("danger", "No ha sido posible eliminar el registro");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return RedirectToAction("Index", "DetalleFlorClasificada"); ;
		}

	}
}