﻿using System.Web;
using System.Web.Mvc;

namespace OSL.Inventory.B2.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
