using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysFloricola.Models
{
	public class ReporteDAL
	{
		public List<BloqueVariedades_Reporte> Reporte_Bloque_Variedades(int blqCodigo)
		{
			List<BloqueVariedades_Reporte> lista = new List<BloqueVariedades_Reporte>();

			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_Bloque_variedades_Result> items = db.spSelect_Bloque_variedades(blqCodigo).ToList();
				foreach (var item in items)
				{
					BloqueVariedades_Reporte obj = new BloqueVariedades_Reporte();
					obj.BLQCODIGOI = item.BLQCODIGOI;
					obj.VRDCODIGOI = item.VRDCODIGOI;
					obj.BLQIDENTIFIC = item.BLQIDENTIFIC;
					obj.VRDNOMBREC = item.VRDNOMBREC;
					obj.TOTAL = item.Total;
					lista.Add(obj);
				}
			}
			return lista;
		}

		public List<BloqueCama_Reporte> Reporte_Bloque_numero_camas(int blqCodigo)
		{
			List<BloqueCama_Reporte> lista = new List<BloqueCama_Reporte>();

			using (BDFloricolaContext db = new BDFloricolaContext())
			{
				List<spSelect_Bloque_numeroCama_Result> items = db.spSelect_Bloque_numeroCama(blqCodigo).ToList();
				foreach (var item in items)
				{
					BloqueCama_Reporte obj = new BloqueCama_Reporte();
					obj.BlvNumeroCama = item.BLVNUMEROCAMA;
					obj.TOTAL = item.Total;
					lista.Add(obj);
				}
			}
			return lista;
		}
	}

	public class BloqueVariedades_Reporte
	{
		public int BLQCODIGOI { get; set; }
		public int VRDCODIGOI { get; set; }
		public int BLQIDENTIFIC { get; set; }
		public string VRDNOMBREC { get; set; }
		public int? TOTAL { get; set; }
	}

	public class BloqueCama_Reporte
	{
		public int BlvNumeroCama { get; set; }
		public int? TOTAL { get; set; }
	}

}