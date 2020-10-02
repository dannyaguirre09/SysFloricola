using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Models
{
	public class PreenvioDAL
	{
		public List<PREENVIOS> Lista_Preenvios()
		{
			List<PREENVIOS> lista = new List<PREENVIOS>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				lista = db.PREENVIOS.OrderByDescending(x => x.PREFECHA).ToList();
			}
			return lista;
		}

		public SelectList Lista_Tamanio(int id)
		{
			BDFloricolaContext db = new BDFloricolaContext();
			if (id == 0)
				return new SelectList(db.TAMANO_TALLOS, "TMTCODIGOI", "TMTDESCRIPC");
			else
				return new SelectList(db.TAMANO_TALLOS, "TMTCODIGOI", "TMTDESCRIPC", id);
		}

		public List<Items> Lista_items(Items model)
		{
			List<Items> lista = new List<Items>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_Items_Preenvio_Result> items = db.spSelect_Items_Preenvio(model.TPFCODIGOI, model.VRDCODIGOI, model.TMTCODIGOI, model.UNDCODIGOI).ToList();
				foreach (var item in items)
				{
					Items obj = new Items();
					if (item.DTECANTIDAD > 0)
					{
						obj.DTECODIGOI = item.DTECODIGOI;
						obj.TMTDESCRIPC = item.TMTDESCRIPC;
						obj.UNDDESCRIPCION = item.UNDDESCRIPCION;
						obj.TPFDESCRIPCION = item.TPFDESCRIPCION;
						obj.PRCCODIGOI = item.PRCCODIGOI;
						obj.VRDNOMBREC = item.VRDNOMBREC;
						obj.DTEFECHAINGRESO = item.DTEFECHAINGRESO.ToShortDateString();
						obj.DTECANTIDAD = item.DTECANTIDAD;
						lista.Add(obj);
					}
					
				}
			}
			return lista;
		}

		public int Crear_Preenvio(PREENVIOS preenvio, List<DetalleItems> listaDetalle)
		{
			int respuesta = 0;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						context.PREENVIOS.Add(preenvio);
						context.SaveChanges();

						foreach (var item in listaDetalle)
						{
							int res = context.spInsert_Detalle_preenvio(item.DTECODIGOI, item.cantidadIngresada);
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

		public int Editar_Preenvio(PREENVIOS preenvio, List<DetalleItems> listaDetalle)
		{
			int respuesta = 0;
			using (var context = new BDFloricolaContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						int res = context.spUpdate_Preenvio(preenvio.PRECODIGOI, preenvio.CLNCODIGOI, preenvio.PREFECHA, Convert.ToInt32(preenvio.PRENUMERO) ,preenvio.PREAWB, preenvio.PREHAWB, preenvio.PRENUMPIEZAS, preenvio.PREIDCAJAS );
						List<DETALLES_PREENVIOS> listaItemsPreenvio = context.DETALLES_PREENVIOS.Where(x => x.PRECODIGOI == preenvio.PRECODIGOI).ToList();

						foreach (var item in listaItemsPreenvio)
						{
							int respuestaActualizar = context.spActualizar_Stock(item.DTECODIGOI, item.DTPCODIGOI, item.DTPCANTIDAD);
						}
						foreach (var item in listaDetalle)
						{
							int respuestaInsertarNuevo = context.spInsert_Editado_Detalle_preenvio(item.DTECODIGOI, item.cantidadIngresada, preenvio.PRECODIGOI);
						}
						transaction.Commit();
						respuesta = 1;
					}
					catch (Exception ex)
					{
						transaction.Rollback();
					}
				}
			}
			return respuesta;
		}

		public List<Preenvio> Lista_Preenvio_Id(int preCodigoI)
		{
			List<Preenvio> lista = new List<Preenvio>();
			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_preenvio_id_Result> preenvios = db.spSelect_preenvio_id(preCodigoI).ToList();
				foreach (var item in preenvios)
				{
					Preenvio obj = new Preenvio();
					obj.PRECODIGOI = item.PRECODIGOI;
					obj.PRENUMERO = Convert.ToInt32(item.PRENUMERO);
					obj.PREAWB = item.PREAWB.TrimEnd();
					obj.PREHAWB = item.PREHAWB.TrimEnd();
					obj.PRENUMPIEZAS = item.PRENUMPIEZAS;
					obj.PREIDCAJAS = item.PREIDCAJAS.TrimEnd();
					obj.CLNCODIGOI = item.CLNCODIGOI;
					obj.CLNRUC = item.CLNRUC;
					obj.CLNRAZONSOCIAL = item.CLNRAZONSOCIAL;
					obj.CLNTELEFONO = item.CLNTELEFONO;
					obj.PREFECHA = item.PREFECHA;
					lista.Add(obj);

				}
			}
			return lista;
		}

		public List<spSelect_listado_items_Result> Lista_Detalle_Items(int preCodigoI)
		{
			List<spSelect_listado_items_Result> lista = new List<spSelect_listado_items_Result>();			
			using (BDFloricolaContext db = new BDFloricolaContext())
				lista = db.spSelect_listado_items(preCodigoI).ToList();
			return lista;
		}

	}

	/////Modelos
	
	public class Items
	{
		public int DTECODIGOI { get; set; }
		public string TMTDESCRIPC { get; set; }
		public string UNDDESCRIPCION { get; set; }
		public string TPFDESCRIPCION { get; set; }
		public int PRCCODIGOI { get; set; }
		public string VRDNOMBREC { get; set; }
		public string DTEFECHAINGRESO { get; set; }
		public int DTECANTIDAD { get; set; }
		public int TPFCODIGOI { get; set; }
		public int VRDCODIGOI { get; set; }
		public int UNDCODIGOI { get; set; }
		public int TMTCODIGOI { get; set; }

	}

	public class DetalleItems
	{
		public int DTECODIGOI { get; set; }
		public int cantidadIngresada { get; set; }
		public string variedad { get; set; }
		public string stem { get; set; }
		public string length { get; set; }
	}

	public class Preenvio
	{
		public int PRECODIGOI { get; set; }
		public decimal PRENUMERO { get; set; }
		public string PREAWB { get; set; }
		public string PREHAWB { get; set; }
		public int PRENUMPIEZAS { get; set; }
		public string PREIDCAJAS { get; set; }
		public int DTPCODIGOI { get; set; }
		public int DTECODIGOI { get; set; }
		public int DTPCANTIDAD { get; set; }
		public int CLNCODIGOI { get; set; }
		public string CLNRUC { get; set; }
		public string CLNRAZONSOCIAL { get; set; }
		public string CLNTELEFONO { get; set; }
		public DateTime PREFECHA { get; set; }

	}



}