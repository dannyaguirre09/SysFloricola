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
	public class PreenvioController : Controller
    {
		private PreenvioDAL objDAL = new PreenvioDAL();

        // GET: Preenvio
        public ActionResult Index(int id= 0)
        {
			if(id == 1)
				Request.Flash("success", "Preenvío Creado Correctamente");
			else if(id == 2)
					Request.Flash("danger", "No ha sido posible crear el preenvío");
			else if (id == 3)
				Request.Flash("success", "Preenvío Editado Correctamente");

			List<PREENVIOS> lista = objDAL.Lista_Preenvios();
            return View(lista);
        }

		public ActionResult Preenvio()
		{
			ViewBag.Fecha = DateTime.Now.ToShortDateString();
			return View();
		}

		[HttpPost]
		public JsonResult Preenvio(PREENVIOS preenvio, List<DetalleItems> detalleItems)
		{
			bool estado = false;
			int respuesta = objDAL.Crear_Preenvio(preenvio, detalleItems);
			if (respuesta == 1)
				estado = true;
			return Json(new { estado = estado, respuesta = respuesta }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult EditarPreenvio(int preCodigoI)
		{
			List<Preenvio> lista = objDAL.Lista_Preenvio_Id(preCodigoI);
			return View(lista);
		}

		[HttpPost]
		public JsonResult EditarPreenvio(PREENVIOS preenvio, List<DetalleItems> detalleItems)
		{
			bool estado = false;
			int respuesta =  objDAL.Editar_Preenvio(preenvio, detalleItems);
			if (respuesta == 1)
				estado = true;
			return Json(new { estado = estado, respuesta = respuesta }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ListaDetalleItems(int preCodigoI)
		{
			bool estado = false;
			string mensaje = "OK";
			List<spSelect_listado_items_Result> lista = new List<spSelect_listado_items_Result>();
			try
			{
				lista = objDAL.Lista_Detalle_Items(preCodigoI);
				estado = true;
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
			}
			return Json(new { estado = estado, mensaje = mensaje, lista = lista }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ModalCliente()
		{
			List<CLIENTES> listaClientes = ClientesDAL.Obtener_Clientes();
			return View(listaClientes);
		}

		public ActionResult ModalItems()
		{
			BloqueVariedadesDAL objBloquesDAL = new BloqueVariedadesDAL();
			DetalleFlorClasificadaDAL objDetalleDAL = new DetalleFlorClasificadaDAL();
			ViewBag.VRDCODIGOI = objBloquesDAL.Lista_Variedades(0);
			ViewBag.TPFCODIGOI = objDetalleDAL.Lista_Tipo_flor(0);
			ViewBag.TMTCODIGOI = objDAL.Lista_Tamanio(0);
			return View();
		}

		public JsonResult ObtenerItems(int TPFCODIGOI, int VRDCODIGOI, int TMTCODIGOI, int UNDCODIGOI)
		{
			
			List<Items> lista = new List<Items>();
			string mensaje = "OK";
			bool estado = true;
			try
			{
				Items items = new Items();
				items.TPFCODIGOI = TPFCODIGOI;
				items.VRDCODIGOI = VRDCODIGOI;
				items.TMTCODIGOI = TMTCODIGOI;
				items.UNDCODIGOI = UNDCODIGOI;
				lista = objDAL.Lista_items(items);
			}
			catch (Exception ex)
			{
				mensaje = ex.Message;
				estado = false;
			}

			return Json(new { estado = estado, mensaje = mensaje, data = lista }, JsonRequestBehavior.AllowGet);
		}

	}
}