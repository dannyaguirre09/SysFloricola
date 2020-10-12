using SysFloricola.Filter;
using SysFloricola.Models;
using SysFloricola.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
	[SecurityFilter]
	public class HomeController : Controller
    {
		private Encriptacion encriptar = new Encriptacion();
		private LoginDAL objDal = new LoginDAL();
		
		public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult CambiarPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CambiarPassword(string nuevoPassword)
		{
			bool respuesta = false;
			try
			{
				int codigoUsuario = Convert.ToInt32(Session["CodigoUsuario"]);
				string passCodificado = encriptar.Encriptar(nuevoPassword);
				respuesta = objDal.ActualizarContraseña(passCodificado, codigoUsuario);
				if (respuesta)
				{
					Request.Flash("success", "Contraseña Actualizada Correctamente");
					return RedirectToAction("Index", "Login");
				}else
					Request.Flash("danger", "No ha sido posible actualizar la contraseña");
			}
			catch (Exception exc)
			{
				Request.Flash("danger", "Ha ocurrido un error: " + exc.Message);
			}

			return View();
		}
	}
}