using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class ClientesDAL
	{
		public static List<CLIENTES> Obtener_Clientes()
		{
			using (var db = new BDFloricolaContext())
				return db.CLIENTES.Include(e => e.ESTADOS).ToList();
		}

		public SelectList Lista_Estado(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.ESTADOS, "ESTCODIGOI", "ESTNOMBRE");
			else
				return new SelectList(db.ESTADOS, "ESTCODIGOI", "ESTNOMBRE", id);
		}

		public bool Crear_Cliente(CLIENTES obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.CLIENTES.Add(obj);
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

		public CLIENTES Buscar_Cliente(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.CLIENTES.Find(id);
		}

		public bool Eliminar_Cliente(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				CLIENTES obj = db.CLIENTES.Find(id);
				db.CLIENTES.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}