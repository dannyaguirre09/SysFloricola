using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class TipoFloresDAL
	{
		public static List<TIPOS_FLORES> Obtener_Tipos_Flores()
		{
			using (var db = new BDFloricolaContext())
				return db.TIPOS_FLORES.ToList();
		}

		public bool Crear_Tipos_flores(TIPOS_FLORES obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.TIPOS_FLORES.Add(obj);
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

		public TIPOS_FLORES Buscar_Tipo_flores(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.TIPOS_FLORES.Find(id);
		}

		public bool Eliminar_Tipo_Flores(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				TIPOS_FLORES obj = db.TIPOS_FLORES.Find(id);
				db.TIPOS_FLORES.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}