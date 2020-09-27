using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class ControlFlorNacionalDAL
	{
	
		public List<PROCESO_CLASIFICACION> Lista_Proceso_Clasificacion()
		{
			List<PROCESO_CLASIFICACION> lista = new List<PROCESO_CLASIFICACION>();

			using (BDFloricolaContext db = new BDFloricolaContext())
				lista = db.PROCESO_CLASIFICACION.Where(x => x.PCRIDENTIFICADOR.Equals(2) && x.PRCESTADO == true).OrderByDescending(x => x.PRCFECHA).ToList();
			return lista;
		}

		public DateTime retornarFecha(int prcCodigoi)
		{
			DateTime fecha = DateTime.Now;
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				PROCESO_CLASIFICACION res = db.PROCESO_CLASIFICACION.Where(x => x.PRCCODIGOI == prcCodigoi && x.PCRIDENTIFICADOR == 2).FirstOrDefault();
				fecha = res.PRCFECHA;
			}
			return fecha;
		}

		public SelectList Lista_causas(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.CAUSAS, "CAUCODIGOI", "CAUDESCRIPCION");
			else
				return new SelectList(db.CAUSAS, "CAUCODIGOI", "CAUDESCRIPCION", id);
		}

		public List<TIPO_CAUSAS> Lista_tipo_causa(int id)
		{
			List<TIPO_CAUSAS> lista = new List<TIPO_CAUSAS>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				lista = db.TIPO_CAUSAS.Where(x => x.CAUCODIGOI == id).OrderBy(x => x.CAUCODIGOI).ToList();
			}
			return lista;
		}

		public List<VARIEDADES> listaVariedades()
		{
			BDFloricolaContext db = new BDFloricolaContext();
			return db.VARIEDADES.OrderBy(x => x.VRDCODIGOI).ToList();
		}

		public List<CONTROLES_FLOR_NACIONAL> listaControlFlor(DateTime fecha, int CAUCODIGOI)
		{
			List<CONTROLES_FLOR_NACIONAL> lista = new List<CONTROLES_FLOR_NACIONAL>();

			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spVerificar_control_flor_nacional_Result> detalle = db.spVerificar_control_flor_nacional(fecha, CAUCODIGOI).ToList();
				foreach (var item in detalle)
				{
					CONTROLES_FLOR_NACIONAL obj = new CONTROLES_FLOR_NACIONAL();
					obj.CFNCODIGOI = item.CFNCODIGOI;
					obj.TPCCODIGOI = item.TPCCODIGOI;
					obj.PRCCODIGOI = item.PRCCODIGOI;
					obj.VRDCODIGOI = item.VRDCODIGOI;
					obj.CFNCANTIDAD = item.CFNCANTIDAD;
					lista.Add(obj);
				}
			}
			return lista;
		}

		public int Crear_Control_Flor_Nacional(List<CONTROLES_FLOR_NACIONAL> lista, DateTime fecha, string observacion )
		{
			int respuesta = 0;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						var comprobar = context.spVerificar_proceso(fecha, 2).ToList();
						if (comprobar.Count <= 0)
						{
							PROCESO_CLASIFICACION obj = new PROCESO_CLASIFICACION();
							obj.PRCESTADO = true;
							obj.PCRIDENTIFICADOR = 2;
							obj.PRCFECHA = fecha;
							obj.PRCOBSERVACION = observacion;
							context.PROCESO_CLASIFICACION.Add(obj);
							context.SaveChanges();
						}
						
						foreach (var item in lista)
						{
							int res = context.spInsert_control_flor_nacional(item.TPCCODIGOI, item.VRDCODIGOI, item.CFNCANTIDAD, observacion, fecha);
						}
						transaction.Commit();
						respuesta = 1;
					}
					catch (Exception)
					{
						transaction.Rollback();
					}
				}
			}
			return respuesta;
		}

		public int Editar_Control_Flor_Nacional(List<CONTROLES_FLOR_NACIONAL> lista, DateTime fecha)
		{
			int respuesta = 0;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						foreach (var item in lista)
						{
							int res = context.spUpdate_control_flor_nacional(item.CFNCODIGOI, item.TPCCODIGOI, item.VRDCODIGOI, item.CFNCANTIDAD, fecha);
						}
						transaction.Commit();
						respuesta = 1;
					}
					catch (Exception)
					{
						transaction.Rollback();
					}
				}
			}
			return respuesta;
		}


	}
}