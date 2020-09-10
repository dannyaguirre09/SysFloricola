using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class PaisesDAL
	{
		public static List<PAICES> Obtener_Paises()
		{
			using (var db = new BDFloricolaContext())
				return db.PAICES.ToList();
		}

		public bool Crear_Pais(PAICES objPais, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.PAICES.Add(objPais);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(objPais).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}

			}

			return respuesta;
		}

		public PAICES Buscar_Pais(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.PAICES.Find(id);
		}

		public bool Eliminar_Pais(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				PAICES pais = db.PAICES.Find(id);
				db.PAICES.Remove(pais);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}