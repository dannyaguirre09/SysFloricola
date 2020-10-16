using SysFloricola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysFloricola.Filter
{
	public class SecurityFilter: ActionFilterAttribute, IActionFilter
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			try
			{
				base.OnActionExecuting(filterContext);
				string controller = filterContext.RouteData.GetRequiredString("controller");
				string action = filterContext.RouteData.GetRequiredString("action");
				string nombreUsuario = (string)HttpContext.Current.Session["Usuario"];
				
				if (string.IsNullOrEmpty(nombreUsuario))
				{
					filterContext.HttpContext.Response.Redirect("/login");
				}
				else
				{
					int codigoUsuario = (int)HttpContext.Current.Session["CodigoUsuario"];
					if (!new MenuDAL().Posee_Permisos(codigoUsuario, controller))
					{
						filterContext.HttpContext.Response.Redirect("/");
					}
				}

			}
			catch (Exception)
			{
				filterContext.Result = new RedirectResult("/Error");
			}
		}
	}
}