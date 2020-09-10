using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class EmpresasDAL
	{
		public static List<EMPRESAS> Obtener_Empresas()
		{
			using (var db = new BDFloricolaContext())
				return db.EMPRESAS.ToList();
		}

		public bool Crear_Empresa(EMPRESAS obj, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.EMPRESAS.Add(obj);
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

		public EMPRESAS Buscar_Empresa(int id)
		{
			using (var db = new BDFloricolaContext())
				return db.EMPRESAS.Find(id);
		}

		public bool Eliminar_Empresas(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				EMPRESAS obj = db.EMPRESAS.Find(id);
				db.EMPRESAS.Remove(obj);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}
	}
}