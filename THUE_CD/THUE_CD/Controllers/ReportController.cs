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
            List<ModelCustomer> CusList = db.Customers.Where(x => x.orderList.Count > 0).Select(x =>
                 new ModelCustomer
                 {
                     Id_Customer = x.Id_Customer,
                     Address = x.Address,
                     Fine = x.Fine,
                     FullName = x.FullName,
                     Phone = x.Phone,
                     //CountCDBorrow = x.orderList.Sum(y=>y.orderDetailList.Where(z=>z.Status == "CHƯA TRẢ").Count())
                     CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Count),
                     TotalRent = x.orderList.Sum(y => y.TotalRent)
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
                DateRent = x.Orders.DateRent,
                DateDue = x.DateDue,
                DateReturn = x.DateReturn,
                RentFee = x.RentFee,
                LateFee = x.LateFee,
                Status = x.Status
            }).ToList();

            return Json(value, JsonRequestBehavior.AllowGet);
        }
        //Tim khách hàng
        [HttpGet]
        public JsonResult GetSearchingData(string SearchBy)
        {
            List<ModelCustomer> CusList = new List<ModelCustomer>();
            if (SearchBy == "0")
            {
                try
                {
                        CusList = db.Customers.Where(x => x.orderList.Count > 0).Select(x =>
                     new ModelCustomer
                     {
                         Id_Customer = x.Id_Customer,
                         Address = x.Address,
                         Fine = x.Fine,
                         FullName = x.FullName,
                         Phone = x.Phone,
                         //CountCDBorrow = x.orderList.Sum(y=>y.orderDetailList.Where(z=>z.Status == "CHƯA TRẢ").Count())
                         CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Count),
                         TotalRent = x.orderList.Sum(y => y.TotalRent)
                     }
                    ).ToList();
                        return Json(CusList, JsonRequestBehavior.AllowGet);

                    //int Id = Convert.ToInt32(SearchBy);
                    //StuList = db.Customers.Where(x => x.Id_Customer == Id).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} Is Not A ID ", SearchBy);
                }
                return Json(CusList, JsonRequestBehavior.AllowGet);
            }
            else if(SearchBy == "1")
            {
                CusList = db.Customers.Where(x => x.orderList.Sum(y => y.orderDetailList.Where(z => z.DateDue < z.DateReturn).Count()) > 0).Select(x =>
                     new ModelCustomer
                     {
                         Id_Customer = x.Id_Customer,
                         Address = x.Address,
                         Fine = x.Fine,
                         FullName = x.FullName,
                         Phone = x.Phone,
                         //CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Where(z => z.LateFee > 0).Count()),
                         CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Count),
                         TotalRent = x.orderList.Sum(y => y.TotalRent)
                     }
                    ).ToList();
                return Json(CusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CusList = db.Customers.Where(x => x.orderList.Sum(y => y.orderDetailList.Where(z => z.RentFee > 0).Count()) > 0).Select(x =>
                     new ModelCustomer
                     {
                         Id_Customer = x.Id_Customer,
                         Address = x.Address,
                         Fine = x.Fine,
                         FullName = x.FullName,
                         Phone = x.Phone,
                         //CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Where(z => z.LateFee > 0).Count()),
                         CountCDBorrow = x.orderList.Sum(y => y.orderDetailList.Count),
                         TotalRent = x.orderList.Sum(y => y.TotalRent)
                     }
                    ).ToList();
                return Json(CusList, JsonRequestBehavior.AllowGet);
                //CusList = db.Customers.Where(x => x.FullName.StartsWith(SearchBy)).ToList();
                //return Json(CusList, JsonRequestBehavior.AllowGet);
            }

        }
    }
}