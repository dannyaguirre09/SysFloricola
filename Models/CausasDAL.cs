using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class CausasDAL
	{
		public static List<CAUSAS> Obtener_Causas()
		{
			using (var db = new BDFloricolaContext())
				return db.CAUSAS.ToList();
		}		

		public bool Crear_Causas(CAUSAS obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.CAUSAS.Add(obj);
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

		public CAUSAS Buscar_Causas(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.CAUSAS.Find(id);
		}

		public bool Eliminar_Causa(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				CAUSAS obj = db.CAUSAS.Find(id);
				db.CAUSAS.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}