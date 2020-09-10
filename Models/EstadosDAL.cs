using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class EstadosDAL
	{
		public static List<ESTADOS> Obtener_Estados()
		{
			using (var db = new BDFloricolaContext())
				return db.ESTADOS.Include(e => e.PAICES).ToList();
		}

		public SelectList Lista_Paises(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.PAICES, "PAICODIGOI", "PAINOMBRE");
			else
				return new SelectList(db.PAICES, "PAICODIGOI", "PAINOMBRE", id);
		}

		public bool Crear_Estadp(ESTADOS objEstado, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.ESTADOS.Add(objEstado);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(objEstado).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}

			}

			return respuesta;
		}

		public ESTADOS Buscar_Estado(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.ESTADOS.Find(id);
		}

		public bool Eliminar_Estado(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				ESTADOS estado = db.ESTADOS.Find(id);
				db.ESTADOS.Remove(estado);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}