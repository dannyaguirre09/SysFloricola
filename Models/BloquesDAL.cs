using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class BloquesDAL
	{
		public List<BLOQUES> Obtener_Bloques()
		{
			using (var db = new BDFloricolaContext())
				return db.BLOQUES.Include(e => e.FINCAS).ToList();
		}

		public SelectList Lista_Fincas(int fncCodigoI)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (fncCodigoI == 0)
				return new SelectList(db.FINCAS, "FNCCODIGOI", "FNCNOMBREC");
			else
				return new SelectList(db.FINCAS, "FNCCODIGOI", "FNCNOMBREC", fncCodigoI);
		}

		public bool Crear_Bloque(BLOQUES objBloque, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.BLOQUES.Add(objBloque);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(objBloque).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}

			}

			return respuesta;
		}

		public BLOQUES Buscar_Bloque(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.BLOQUES.Find(id);
		}

		public bool Eliminar_Bloque(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				BLOQUES bloque = db.BLOQUES.Find(id);
				db.BLOQUES.Remove(bloque);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}