using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [HttpPost]
        public ActionResult UpdateCustomer(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Customers.Where(x => x.Id_Customer == id).ToList();

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

    }
}