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
            List<Customer> Cus = db.Customers.ToList();
            return View();

            //var Cus = db.Customers.ToList();
            //return View(Cus);
        }

        public JsonResult GetCustomerList()
        {
            List<Customer> CusList = db.Customers.Where(x => x.Fine == 0).Select(x => new Customer
            {
                Id_Customer = x.Id_Customer,
                FullName = x.FullName,
                Address = x.Address,
                Phone = x.Phone,
                Fine = x.Fine
            }).ToList();
            return Json(CusList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCustomer()
        {
            var Cus = db.Customers.OrderBy(a => a.Id_Customer).ToList();
            return Json(new { data = Cus }, JsonRequestBehavior.AllowGet);
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