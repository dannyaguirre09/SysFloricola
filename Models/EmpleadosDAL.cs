using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SysFloricola.Models
{
    public class EmpleadosDAL
    {
        public static List<EMPLEADOS> Obtener_Empleados()
        {
			using (var db = new BDFloricolaContext())
			return db.EMPLEADOS.Include(e => e.EMPRESAS).ToList();
        }
       
		public SelectList Lista_Empresas(int EmpCcodigoI)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if(EmpCcodigoI == 0)
				return new SelectList(db.EMPRESAS, "EMPCODIGOI", "EMPNOMBREC");
			else
				return new SelectList(db.EMPRESAS, "EMPCODIGOI", "EMPNOMBREC", EmpCcodigoI);
		}

		public bool Crear_Empleado(EMPLEADOS objEmpleado, bool nuevo)
		{
			bool respuesta = false;
			if (nuevo)
			{
				using (var db = new BDFloricolaContext())
				{
					db.EMPLEADOS.Add(objEmpleado);
					db.SaveChanges();
					respuesta = true;
				}
			}
			else
			{
				using (var db = new BDFloricolaContext())
				{
					db.Entry(objEmpleado).State = EntityState.Modified;
					db.SaveChanges();
					respuesta = true;
				}
				
			}
			
			return respuesta;
		}

		public EMPLEADOS Buscar_Empleado(int id)
		{
			using (var db = new BDFloricolaContext())
			return db.EMPLEADOS.Find(id);
		}

		public bool Eliminar_Empleado(int id)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
				db.EMPLEADOS.Remove(eMPLEADOS);
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}

	}

}