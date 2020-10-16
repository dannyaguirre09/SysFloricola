using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class MenuController : Controller
    {
		private MenuDAL objDal = new MenuDAL();

		// GET: Menu
		public  List<spSelect_Menu_Acceso_Result> ObtenerListaMenu(int codigoUsuario)
		{
			List<spSelect_Menu_Acceso_Result> lista = new List<spSelect_Menu_Acceso_Result>();
			lista = objDal.Obtener_Lista_Menu(codigoUsuario);
			return lista;
		}

		public List<spSelect_SubMenu_Result> ObtenerSubMenu(int codigoMenu)
		{
			List<spSelect_SubMenu_Result> lista = new List<spSelect_SubMenu_Result>();
			lista = objDal.Obtener_SubMenu(codigoMenu);
			return lista;
		}

	}
}