using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THUE_CD.Models;
using Newtonsoft.Json;
using THUE_CD.ViewCustomer;

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

        //Nut save
        [HttpPost]
        public JsonResult SaveCustomerInDatabase(ModelCustomer model)
        {
            var result = false;
            try
            {
                if (model.Id_Customer > 0)
                {
                    Customer Cus = db.Customers.SingleOrDefault(x => x.Id_Customer == model.Id_Customer);
                    Cus.FullName = model.FullName;
                    Cus.Address = model.Address;
                    Cus.Phone = model.Phone;
                    Cus.Fine = model.Fine;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Customer Cus = new Customer();
                    Cus.FullName = model.FullName;
                    Cus.Address = model.Address;
                    Cus.Phone = model.Phone;
                    Cus.Fine = model.Fine;


                    db.Customers.Add(Cus);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch
            {
                throw new HttpException(404, "Add false");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetCustomerById(int Id_Customer)
        {
            Customer model = db.Customers.Where(x => x.Id_Customer == Id_Customer).SingleOrDefault();
            string value = string.Empty;

            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult DeleteCustomerRecord(int Id_Customer)
        {
            bool result = false;
            Customer Cus = db.Customers.SingleOrDefault(x => x.Id_Customer == Id_Customer);

            if (Cus != null)
            {
                Order Or = db.Orders.SingleOrDefault(x => x.Id_Customer == Id_Customer);
                if(Or != null)
                {
                    OrderDetail OrD = db.OrderDetails.SingleOrDefault(x => x.Id_Order == Or.Id_Order);
                    if (OrD != null)
                    {
                        db.OrderDetails.Remove(OrD);
                        db.Orders.Remove(Or);
                        db.Customers.Remove(Cus);
                        db.SaveChanges();
                        result = true;
                    }
                    
                }
                db.Customers.Remove(Cus);
                db.SaveChanges();
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}