using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class DetalleFlorClasificadaDAL
	{
		public List<PROCESO_CLASIFICACION> Lista_Proceso_Clasificacion ()
		{
			List<PROCESO_CLASIFICACION> lista = new List<PROCESO_CLASIFICACION>();

			using (BDFloricolaContext db= new BDFloricolaContext())
				lista = db.PROCESO_CLASIFICACION.Where(x => x.PCRIDENTIFICADOR.Equals(1) && x.PRCESTADO == true ).OrderByDescending(x=> x.PRCFECHA).ToList();
			return lista;
		}

		public DateTime retornarFecha (int prcCodigoi)
		{
			DateTime fecha = DateTime.Now;
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				PROCESO_CLASIFICACION res = db.PROCESO_CLASIFICACION.Where(x => x.PRCCODIGOI == prcCodigoi && x.PCRIDENTIFICADOR == 1).FirstOrDefault();
				fecha = res.PRCFECHA;
			}
			return fecha;
		}

		public SelectList Lista_Tipo_flor(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.TIPOS_FLORES, "TPFCODIGOI", "TPFDESCRIPCION");
			else
				return new SelectList(db.TIPOS_FLORES, "TPFCODIGOI", "TPFDESCRIPCION", id);
		}

		public List<FlorTallo> Lista_tallos(int id)
		{
			List<FlorTallo> lista = new List<FlorTallo>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_flor_numerotallos_Result> tallos = db.spSelect_flor_numerotallos(id).ToList();
				foreach (var item in tallos)
				{
					FlorTallo obj = new FlorTallo();
					obj.TPFDESCRIPCION = item.TPFDESCRIPCION;
					obj.UNDDESCRIPCION = item.UNDDESCRIPCION;
					obj.UNDCODIGOI = item.UNDCODIGOI;
					lista.Add(obj);
				}
			}
			return lista;
		}

		public List<FlorTallo> Lista_unidades(int id, int idUnidad)
		{
			List<FlorTallo> lista = new List<FlorTallo>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_tallos_unidad_tam_Result> tallos = db.spSelect_tallos_unidad_tam(id, idUnidad).ToList();
				foreach (var item in tallos)
				{
					FlorTallo obj = new FlorTallo();
					obj.TPFDESCRIPCION = item.TPFDESCRIPCION;
					obj.UNDDESCRIPCION = item.UNDDESCRIPCION;
					obj.TMTDESCRIPC = item.TMTDESCRIPC;
					obj.UNDCODIGOI = item.UNDCODIGOI;
					obj.TMTCODIGOI = item.TMTCODIGOI;
					lista.Add(obj);
				}
			}
			return lista;
		}

		public List<VARIEDADES> listaVariedades()
		{
			BDFloricolaContext db = new BDFloricolaContext();
			return db.VARIEDADES.OrderBy(x => x.VRDCODIGOI).ToList();
		}

		public List<Detalle> listaDetalle (int TPFCODIGOI, int UNDCODIGOI, DateTime fecha)
		{
			List<Detalle> lista = new List<Detalle>();

			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spVerificar_detalle_flor_Result> detalle = db.spVerificar_detalle_flor(TPFCODIGOI, UNDCODIGOI, fecha).ToList();
				foreach (var item in detalle)
				{
					Detalle obj = new Detalle();
					obj.DTECODIGOI = item.DTECODIGOI;
					obj.TMTCODIGOI = item.TMTCODIGOI;
					obj.UNDCODIGOI = item.UNDCODIGOI;
					obj.PRCCODIGOI = item.PRCCODIGOI;
					obj.VRDCODIGOI = item.VRDCODIGOI;
					obj.DTECANTIDAD = item.DTECANTIDAD;
					lista.Add(obj);
				}
			}
			return lista;
		}

		public int Crear_Detalle_Flor_Clasificada(List<Detalle> lista, string observacion, DateTime fecha, int idTipoFlor, int idUnidad)
		{
			int respuesta = 0;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						var verificacion = context.spVerificar_proceso_diario(fecha, idTipoFlor, idUnidad).ToList();
						if (verificacion.Count <= 0)
						{
							var comprobar = context.spVerificar_proceso(fecha, 1).ToList();
							if(comprobar.Count <= 0)
							{
								PROCESO_CLASIFICACION obj = new PROCESO_CLASIFICACION();
								obj.PRCESTADO = true;
								obj.PCRIDENTIFICADOR = 1;
								obj.PRCFECHA = DateTime.Now;
								obj.PRCOBSERVACION = observacion;
								context.PROCESO_CLASIFICACION.Add(obj);
								context.SaveChanges();
							}
							
							foreach (var item in lista)
							{
								int res = context.spInsert_detalle_flor_clasificada(item.TMTCODIGOI, item.UNDCODIGOI, item.TPFCODIGOI, item.VRDCODIGOI, item.DTECANTIDAD, fecha);
							}
							transaction.Commit();
							respuesta = 1;
						}
						else
							respuesta = 2;
					}
					catch (Exception)
					{
						transaction.Rollback();
					}
				}
			}
			return respuesta;
		}

		public int Editar_Detalle_Flor_Clasificada(List<Detalle> lista, DateTime fecha)
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
							int res = context.spUpdate_detalle_flor_clasificada(item.DTECODIGOI, item.TMTCODIGOI, item.UNDCODIGOI, item.TPFCODIGOI, item.VRDCODIGOI, item.DTECANTIDAD, fecha);
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

		public PROCESO_CLASIFICACION Buscar_Proceso_Clasificacion(int id)
		{
			PROCESO_CLASIFICACION obj = new PROCESO_CLASIFICACION();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				obj = db.PROCESO_CLASIFICACION.Where(x => x.PRCCODIGOI == id).FirstOrDefault();
			}

			return obj;
		}

		public bool Eliminar_ProcesoDiario(PROCESO_CLASIFICACION obj)
		{
			bool respuesta = false;
			using (var db = new BDFloricolaContext())
			{
				obj.PRCESTADO = false;
				db.Entry(obj).State = EntityState.Modified;
				db.SaveChanges();
				respuesta = true;
			}

			return respuesta;
		}

	}

	public class FlorTallo
	{
		public int UNDCODIGOI { get; set; }
		public string TPFDESCRIPCION { get; set; }
		public string UNDDESCRIPCION { get; set; }
		public string TMTDESCRIPC { get; set; }
		public int TMTCODIGOI { get; set; }
	}

	public class Detalle
	{
		public int DTECODIGOI { get; set; }
		public int VRDCODIGOI { get; set; }
		public int TMTCODIGOI { get; set; }
		public int DTECANTIDAD { get; set; }
		public int UNDCODIGOI { get; set; }
		public int PRCCODIGOI { get; set; }
		public int TPFCODIGOI { get; set; }
	}
}