using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THUE_CD.Models;

namespace THUE_CD.Controllers
{
    public class CustomerController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        // GET: Customer
        public ActionResult IndexCustomer()
        {
            var Cus = db.Customers.ToList();
            return View(Cus);
        }

       
    }
}