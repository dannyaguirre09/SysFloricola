using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class VariedadesDAL
	{
		public static List<VARIEDADES> Obtener_Variedades()
		{
			using (var db = new BDFloricolaContext())
				return db.VARIEDADES.ToList();
		}

		
		public bool Crear_Variedad(VARIEDADES obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.VARIEDADES.Add(obj);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(obj).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}

			}

			return respuesta;
		}

		public VARIEDADES Buscar_Variedad(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.VARIEDADES.Find(id);
		}

		public bool Eliminar_Variedad(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				VARIEDADES obj = db.VARIEDADES.Find(id);
				db.VARIEDADES.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}