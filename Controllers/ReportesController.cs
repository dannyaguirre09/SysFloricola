using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Controllers
{
    public class ReportesController : Controller
    {
		ReporteDAL objDal = new ReporteDAL();
        // GET: Reportes
        public ActionResult BloquesVariedades()
        {
			BloqueVariedadesDAL obj = new BloqueVariedadesDAL();
			ViewBag.listaBloques =  obj.Lista_Bloques(0);
            return View();
        }

		public ActionResult ObtenerTotalVariedades (int blqCodigoI)
		{
			return Json(objDal.Reporte_Bloque_Variedades(blqCodigoI), JsonRequestBehavior.AllowGet);
		}

		public ActionResult ObtenerTotalCamas(int blqCodigoI)
		{
			return Json(objDal.Reporte_Bloque_numero_camas(blqCodigoI), JsonRequestBehavior.AllowGet);
		}
	}
}