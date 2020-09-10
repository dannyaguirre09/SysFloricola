using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class FincasDAL
	{
		public static List<FINCAS> Obtener_Fincas()
		{
			using (var db = new BDFloricolaContext())
				return db.FINCAS.Include(e => e.EMPRESAS).ToList();
		}

		public SelectList Lista_Empresas(int EmpCodigoI)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (EmpCodigoI == 0)
				return new SelectList(db.EMPRESAS, "EMPCODIGOI", "EMPNOMBREC");
			else
				return new SelectList(db.EMPRESAS, "EMPCODIGOI", "EMPNOMBREC", EmpCodigoI);
		}

		public bool Crear_Finca(FINCAS objFinca, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.FINCAS.Add(objFinca);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(objFinca).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}

			}

			return respuesta;
		}

		public FINCAS Buscar_Finca(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.FINCAS.Find(id);
		}

		public bool Eliminar_Finca(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				FINCAS finca = db.FINCAS.Find(id);
				db.FINCAS.Remove(finca);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}