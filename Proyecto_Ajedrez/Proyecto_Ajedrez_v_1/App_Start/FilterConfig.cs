﻿using System.Web;
using System.Web.Mvc;

namespace Proyecto_Ajedrez_v_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
