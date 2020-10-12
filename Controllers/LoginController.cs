using SysFloricola.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using SysFloricola.Util;

namespace SysFloricola.Controllers
{
    public class LoginController : Controller
    {
		private Encriptacion encriptar = new Encriptacion();
		private LoginDAL objDal = new LoginDAL();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(string usuario, string password)
		{
			try
			{
				string passwordCodificado = encriptar.Encriptar(password); 
				Usuario user = objDal.Obtener_Usuario(usuario, passwordCodificado);
				if(!string.IsNullOrEmpty(user.Username))
				{
					Session["Nombre"] = user.ApellidoP +" " +user.Nombres;
					Session["Usuario"] = user.Username;
					Session["CodigoUsuario"] = user.CodigoUsuario;
					if (Convert.ToBoolean(user.Estado))
						return RedirectToAction("Index", "Home");
					else
						return RedirectToAction("CambiarPassword", "Home");
				}
				else
					Request.Flash("danger", "Usuario o Contraseña Incorrectos");
			}
			catch (Exception ex)
			{
				Request.Flash("danger", ex.Message);
			}
			return View();
		}

		public ActionResult Logout()
		{
			Session.Contents.Remove("Usuario");
			Session.Contents.Remove("Nombre");
			return RedirectToAction("Index", "Login");
		}
		

    }
}