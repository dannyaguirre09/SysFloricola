using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class BloqueVariedadesDAL
	{
		public static List<BLOQUES_VARIEDADES> Obtener_Bloques_Variedades()
		{
			using (var db = new BDFloricolaContext())
				return db.BLOQUES_VARIEDADES.Include(e => e.BLOQUES).Include(e => e.VARIEDADES).ToList();
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

		public SelectList Lista_Camas(int id, int idBloque)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(Numero_Camas(idBloque), "Value", "Text");
			else
				return new SelectList(Numero_Camas(idBloque), "Value", "Text", id);
		}

		public List<NumCamas> Numero_Camas(int idBloque)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			BLOQUES resp = db.BLOQUES.Where(x => x.BLQCODIGOI.Equals(idBloque)).FirstOrDefault();

			List<NumCamas> lista = new List<NumCamas>();
			for (int i = 0; i < resp.BLQNUMCAMAS; i++)
			{
				NumCamas obj = new NumCamas();
				obj.Value = i + 1;
				obj.Text = (i + 1).ToString();
				lista.Add(obj);
			}
			return lista;
		}

		public bool Crear_Bloque_Variedades(BLOQUES_VARIEDADES obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					obj.BLVFECHA = DateTime.Now;
					db.BLOQUES_VARIEDADES.Add(obj);
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

		public BLOQUES_VARIEDADES Buscar_Bloque_Variedad(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.BLOQUES_VARIEDADES.Find(id);
		}

		public bool Eliminar_Bloque_variedad(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				BLOQUES_VARIEDADES bloque = db.BLOQUES_VARIEDADES.Find(id);
				db.BLOQUES_VARIEDADES.Remove(bloque);
				db.SaveChanges();
				respuesta = true;
			}
			return respuesta;
		}

	}

	public class NumCamas
	{
		public int Value { get; set; }
		public string Text { get; set; }
	}


}