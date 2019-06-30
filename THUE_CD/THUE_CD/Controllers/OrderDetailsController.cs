using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using THUE_CD.Models;
using THUE_CD.ViewModel;
namespace THUE_CD.Controllers
{
    public class OrderDetailsController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        // GET: OrderDetails
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddOrder(string lst,string date1, string date2, string CusID)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            List<int> Lst = json_serializer.Deserialize<List<int>>(lst);


            Order or = new Order();
            //or.DateRent = DateTime.ParseExact(date1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            var s = String.Format("dd/mm/yyyy", date1);
            //or.DateRent = DateTime.Parse(date1);
            or.Id_Customer = int.Parse(CusID);
            db.Orders.Add(or);

            foreach (int i in Lst)
            {
                Item x = db.Items.Where(t => t.Id_Item == i).FirstOrDefault();
                if (x != null)
                {
                    OrderDetail ord = new OrderDetail();
                    ord.Id_Item = x.Id_Item;
                    ord.Id_Order = or.Id_Order;
                    ord.DateDue = DateTime.ParseExact(date2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    ord.Status = "";
                    ord.RentFee = x.Titles.TypeDisk.RentPrice;
                    or.TotalRent += ord.RentFee;
                    db.OrderDetails.Add(ord);
                }
            }
            db.SaveChanges();


            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}