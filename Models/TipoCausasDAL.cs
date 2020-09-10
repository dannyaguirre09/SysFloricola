using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class TipoCausasDAL
	{
		public static List<TIPO_CAUSAS> Obtener_Tipo_Causas()
		{
			using (var db = new BDFloricolaContext())
				return db.TIPO_CAUSAS.Include(e => e.CAUSAS).ToList();
		}

		public SelectList Lista_Causas(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.CAUSAS, "CAUCODIGOI", "CAUDESCRIPCION");
			else
				return new SelectList(db.CAUSAS, "CAUCODIGOI", "CAUDESCRIPCION", id);
		}

		public int Crear_Tipo_Causa(TIPO_CAUSAS obj, bool nuevo)
		{
			int respuesta = 0;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					obj.TPCESTADO = true;
					db.TIPO_CAUSAS.Add(obj);
					db.SaveChanges();
					respuesta = 1;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					List<CONTROLES_FLOR_NACIONAL> control = db.CONTROLES_FLOR_NACIONAL.Where(x => x.TPCCODIGOI == obj.TPCCODIGOI).ToList();
					if(control.Count>0)
					{
						TIPO_CAUSAS causa = db.TIPO_CAUSAS.AsNoTracking().Where(x => x.TPCCODIGOI == obj.TPCCODIGOI).FirstOrDefault();
						if (causa.CAUCODIGOI != obj.CAUCODIGOI)
							respuesta = 2;
						else
						{
							db.Entry(obj).State = EntityState.Modified;
							db.SaveChanges();
							respuesta = 1;
						}
					}
					else
					{
						db.Entry(obj).State = EntityState.Modified;
						db.SaveChanges();
						respuesta = 1;
					}

				}

			}

			return respuesta;
		}

		public TIPO_CAUSAS Buscar_Tipo_Causa(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.TIPO_CAUSAS.Find(id);
		}

		public bool Eliminar_Tipo_Causa(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				TIPO_CAUSAS tipo = db.TIPO_CAUSAS.Find(id);
				db.TIPO_CAUSAS.Remove(tipo);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}