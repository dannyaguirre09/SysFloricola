using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class ConciliacionDAL
	{
		public List<spSelect_conciliaciones_Result> Obtener_Conciliaciones()
		{
			using (BDFloricolaContext db = new BDFloricolaContext())
				return db.spSelect_conciliaciones().ToList();
		}

		public List<spSelect_preenvios_conciliacion_Result> Lista_Preenvios()
		{
			using (BDFloricolaContext db = new BDFloricolaContext())
				return db.spSelect_preenvios_conciliacion().ToList();
		}

		public bool Crear_Conciliaciones(List<CONCILIACION> listaConciliaciones)
		{
			bool respuesta = false;
			DateTime Fecha = DateTime.Now;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						foreach (var item in listaConciliaciones)
						{
							item.CNCESTADO = true;
							item.CNCFECHA = Fecha;
							context.CONCILIACION.Add(item);
							context.SaveChanges();
						}
						transaction.Commit();
						respuesta = true;
					}
					catch (Exception ex)
					{
						transaction.Rollback();
					}
				}
			}
			return respuesta;
		}

		public CONCILIACION Buscar_Conciliacion(int id)
		{
			using (BDFloricolaContext db = new BDFloricolaContext())
				return db.CONCILIACION.Where(x => x.CNCCODIGOI == id).FirstOrDefault();
		}

		public bool Editar_Conciliacion(CONCILIACION obj)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				db.Entry(obj).State = EntityState.Modified;
				db.SaveChanges();
				respuesta = true;
			}
			return respuesta;
		}

		public bool Eliminar_Conciliacion(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{

				int res = db.spDelete_Conciliacion(id);
				if(res>0)
					respuesta = true;
			}

			return respuesta;
		}
	}
}