using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class ControlFlorNacionalController : Controller
    {
		private ControlFlorNacionalDAL objDAL = new ControlFlorNacionalDAL();

		// GET: ControlFlorNacional
		public ActionResult Index()
        {
			List<PROCESO_CLASIFICACION> lista = objDAL.Lista_Proceso_Clasificacion();
			return View(lista);
		}

		public ActionResult CrearControlFlorNacional(int proceso = 0)
		{
			DateTime fecha = DateTime.Now;
			try
			{
				ViewBag.CAUCODIGOI = objDAL.Lista_causas(0);
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

		public JsonResult listatipoCausa(int id)
		{
			List<TIPO_CAUSAS> listaTipoCausa = new List<TIPO_CAUSAS>();
			List<TIPO_CAUSAS> lista = new List<TIPO_CAUSAS>();
			string mensaje = "Correcto";
			bool estado = true;
			try
			{
				listaTipoCausa = objDAL.Lista_tipo_causa(id);
				foreach (var item in listaTipoCausa)
				{
					TIPO_CAUSAS obj = new TIPO_CAUSAS();
					obj.TPCCODIGOI = item.TPCCODIGOI;
					obj.TPCDESCRIPCION = item.TPCDESCRIPCION;
					lista.Add(obj);
				}
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
				estado = false;
			}

			return Json(new { lista = lista, mensaje = mensaje, estado = estado }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult listaVariedades(int CAUCODIGOI, string fecha)
		{
			List<VARIEDADES> lista = new List<VARIEDADES>();
			List<VARIEDADES> l = new List<VARIEDADES>();
			List<CONTROLES_FLOR_NACIONAL> listaDetalle = new List<CONTROLES_FLOR_NACIONAL>();
			string mensaje = "Correcto";
			bool estado = true;
			try
			{
				listaDetalle = objDAL.listaControlFlor(Convert.ToDateTime(fecha), CAUCODIGOI);
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

		[HttpPost]
		public JsonResult CrearControlFlorNaciona(List<CONTROLES_FLOR_NACIONAL> listaDetalle, string fecha, string observacion)
		{
			int respuesta = 0;
			try
			{
				if (listaDetalle.Count > 0)
				{
					if (listaDetalle[0].CFNCODIGOI == 0)
						respuesta = objDAL.Crear_Control_Flor_Nacional(listaDetalle, Convert.ToDateTime(fecha), observacion);
					else
						respuesta = objDAL.Editar_Control_Flor_Nacional(listaDetalle, Convert.ToDateTime(fecha));
				}
			}
			catch (Exception)
			{
				respuesta = 0;
			}

			return Json(new { res = respuesta }, JsonRequestBehavior.AllowGet);
		}


	}
}