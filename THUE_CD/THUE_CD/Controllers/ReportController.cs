using Newtonsoft.Json;
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

        //Load dữ liệu của khách hàng
        public JsonResult GetCustomerList()
        {
            List<ModelCustomer> CusList = db.Customers.Where(x=>x.orderList.Count > 0).Select(x =>
               new ModelCustomer
               {
                   Id_Customer = x.Id_Customer,
                   Address = x.Address,
                   Fine = x.Fine,
                   FullName = x.FullName,
                   Phone = x.Phone,
                   CountCDBorrow = x.orderList.Sum(y=>y.orderDetailList.Where(z=>z.Status == "CHƯA TRẢ").Count())
               }
                ).ToList();
            return Json(CusList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //Load danh sach cac CD
        public JsonResult GetCustomerById(int Id_Customer)
        {

            var value = db.OrderDetails.Where(x => x.Orders.Id_Customer == Id_Customer).Select(x => new
            {
                Name = x.Items.Titles.Name,
                DateReturn = x.DateReturn,
                DateDue = x.DateDue,
                Status = x.Status
            }).ToList();


            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}