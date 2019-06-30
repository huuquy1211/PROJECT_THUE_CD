using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THUE_CD.Models;
using THUE_CD.ViewModel;
namespace THUE_CD.Controllers
{
    public class ItemController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        // GET: Item
        public ActionResult Index()
        {


            return View();
        }
        public JsonResult GetItemById(int Id_Item)
        {
            Item model = db.Items.Where(x => x.Id_Item == Id_Item).SingleOrDefault();
            if (model != null)
            {
                ModelItem v = new ModelItem
                {
                    Id_Item = model.Id_Item,
                    Id_Title = model.Id_Title,
                    Id_TypeDisk = model.Titles.Id_TypeDisk,
                    TitleName = model.Titles.Name,
                    TypeName = model.Titles.TypeDisk.NameType,
                    Status = model.Status,
                    RentFee = model.Titles.TypeDisk.RentPrice,
                    LateFee = model.Titles.TypeDisk.LateFee,
                    MaxDate = model.Titles.TypeDisk.MaxDate

                };
                string value = string.Empty;

                value = JsonConvert.SerializeObject(v, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetItemList()
        {
            List<ModelItem> ItemList = db.Items.Select(x =>
               new ModelItem
               {
                   Id_Item = x.Id_Item,
                   Id_Title = x.Id_Title,
                   Id_TypeDisk = x.Titles.Id_TypeDisk,
                   TitleName = x.Titles.Name,
                   TypeName = x.Titles.TypeDisk.NameType,
                   Status = x.Status,
                   RentFee = x.Titles.TypeDisk.RentPrice,
                   LateFee = x.Titles.TypeDisk.LateFee,
                   MaxDate = x.Titles.TypeDisk.MaxDate
               }).ToList();
            return Json(ItemList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveItemInDatabase(int Id_Item, int Id_Title, string Status)
        {
            var result = false;
            try
            {
                Item item = db.Items.SingleOrDefault(x => x.Id_Item == Id_Item);
                if (item != null)
                {
                    item.Id_Title = Id_Title;
                    item.Status = Status;
                    db.SaveChanges();
                }
                else
                {
                    Item t = new Item();
                    t.Id_Title = Id_Title;
                    t.Status = Status;

                    db.Items.Add(t);
                    db.SaveChanges();
                }

                result = true;
            }
            catch
            {
                throw new HttpException(404, "Add false");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteItemRecord(int Id_Item)
        {
            bool result = false;
            Item It = db.Items.SingleOrDefault(x => x.Id_Item == Id_Item);
            List<OrderDetail> Ord = db.OrderDetails.Where(x => x.Id_Item == Id_Item).ToList();


            foreach (var item in Ord)
            {
                db.OrderDetails.Remove(item);
            }
            //thiếu phần xóa ở reservation nữa
            db.Items.Remove(It);
            db.SaveChanges();
            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateItem(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Items.Where(x => x.Id_Item == id).ToList();

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);

        }
    }
}