using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THUE_CD.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult IndexReportCustomer()
        {
            return View();
        }

        public ActionResult IndexReportTitle()
        {
            return View();
        }
    }
}