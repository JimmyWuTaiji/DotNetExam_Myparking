using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MyParking.BL;

namespace MyParking.Test.Controllers
{
    public class UsedCarsController : Controller
    {
        UsedCarsBL usedCarsBL = new UsedCarsBL();
        // GET: UsedCars
        public ActionResult Index()
        {
            return View();  
        }

        public string GetFirstSettingName(string brandName)
        {
            return usedCarsBL.GetFirstSteetingName(brandName);
        }

    }
}