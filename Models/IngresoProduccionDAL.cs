using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class IngresoProduccionDAL
	{
		public static List<INGRESO_PRODUCCION> Obtener_Ingresos_Produccion()
		{
			using (var db = new BDFloricolaContext())
				return db.INGRESO_PRODUCCION.Include(x => x.BLOQUES).Include(x => x.VARIEDADES).ToList();
		}

		public SelectList Lista_Bloques(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.BLOQUES, "BLQCODIGOI", "BLQIDENTIFIC");
			else
				return new SelectList(db.BLOQUES, "BLQCODIGOI", "BLQIDENTIFIC", id);
		}

		public SelectList Lista_Variedades(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.VARIEDADES, "VRDCODIGOI", "VRDNOMBREC");
			else
				return new SelectList(db.VARIEDADES, "VRDCODIGOI", "VRDNOMBREC", id);
		}

		public bool Crear_Ingresos_Produccion(INGRESO_PRODUCCION obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					obj.INPFECHA = DateTime.Now;
					db.INGRESO_PRODUCCION.Add(obj);
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

		public INGRESO_PRODUCCION Buscar_Ingreso_Produccion(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.INGRESO_PRODUCCION.Find(id);
		}

		public bool Eliminar_Ingreso_Produccion(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				INGRESO_PRODUCCION bloque = db.INGRESO_PRODUCCION.Find(id);
				db.INGRESO_PRODUCCION.Remove(bloque);
				db.SaveChanges();
				respuesta = true;
			}
			return respuesta;
		}
	}
}