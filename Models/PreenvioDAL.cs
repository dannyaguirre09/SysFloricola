using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
	}
}