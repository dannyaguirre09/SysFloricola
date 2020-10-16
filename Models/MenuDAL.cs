using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class MenuDAL
	{
		public List<spSelect_Menu_Acceso_Result> Obtener_Lista_Menu(int codigoUsuario)
		{
			List<spSelect_Menu_Acceso_Result> lista = new List<spSelect_Menu_Acceso_Result>();
			using (BDFloricolaContext db = new  BDFloricolaContext())
				lista = db.spSelect_Menu_Acceso(codigoUsuario).ToList();
			return lista;
		}

		public List<spSelect_SubMenu_Result> Obtener_SubMenu(int codigoMenu)
		{
			List<spSelect_SubMenu_Result> lista = new List<spSelect_SubMenu_Result>();
			using (BDFloricolaContext db = new BDFloricolaContext())
				lista = db.spSelect_SubMenu(codigoMenu).ToList();
			return lista;
		}

		public bool Posee_Permisos(int codigoUsuario, string controlador)
		{
			bool respuesta = false;

			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				var lista = db.spSelect_permisos_controlador(codigoUsuario).ToList();
				foreach (var item in lista)
				{
					if (item.Trim().ToString().Equals(controlador))
					{
						respuesta = true;
						break;
					}
						
				}
			}
			return respuesta;
		}
	}
}