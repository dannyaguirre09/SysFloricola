using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class PreenvioController : Controller
    {
		private PreenvioDAL objDAL = new PreenvioDAL();

        // GET: Preenvio
        public ActionResult Index()
        {
			List<PREENVIOS> lista = objDAL.Lista_Preenvios();
            return View(lista);
        }

		public ActionResult Preenvio()
		{
			ViewBag.Fecha = DateTime.Now.ToShortDateString();
			return View();
		}

		public ActionResult ModalCliente()
		{
			List<CLIENTES> listaClientes = ClientesDAL.Obtener_Clientes();
			return View(listaClientes);
		}
    }
}