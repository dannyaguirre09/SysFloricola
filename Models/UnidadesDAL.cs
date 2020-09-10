using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class UnidadesDAL
	{
		public static List<UNIDADES> Obtener_Unidades()
		{
			using (var db = new BDFloricolaContext())
				return db.UNIDADES.ToList();
		}

		public bool Crear_Unidades(UNIDADES obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.UNIDADES.Add(obj);
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

		public UNIDADES Buscar_Unidades(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.UNIDADES.Find(id);
		}

		public bool Eliminar_Unidades(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				UNIDADES obj = db.UNIDADES.Find(id);
				db.UNIDADES.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}