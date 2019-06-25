using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THUE_CD.Models;
using THUE_CD.ViewCustomer;

namespace THUE_CD.Controllers
{
    
    public class ReportController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        // GET: Report
        public ActionResult IndexReportCustomer()
        {
            return View();
        }

        public ActionResult IndexReportTitle()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCustomerList()
        {
            List<ModelCustomer> CusList = db.Customers.Select(x =>
               new ModelCustomer
               {
                   Id_Customer = x.Id_Customer,
                   Address = x.Address,
                   Fine = x.Fine,
                   FullName = x.FullName,
                   Phone = x.Phone
               }
                ).ToList();
            return Json(CusList, JsonRequestBehavior.AllowGet);
        }

    }
}