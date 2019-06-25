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

    public class TitlesController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        // GET: Titles
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetTitlesList()
        {
            List<ModelTitle> value = db.Titles.Select(x => new ModelTitle
            {
                Id_Title = x.Id_Title,
                TitleName = x.Name
            }).ToList();
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTitleById(int Id_Title)
        {
            Title model = db.Titles.Where(x => x.Id_Title == Id_Title).SingleOrDefault();
            ModelTitle v = new ModelTitle
            {
                Id_Title = model.Id_Title,
                TitleName = model.Name,
                Id_Type = model.Id_TypeDisk,
                TypeName = model.TypeDisk.NameType,
                Count = model.CountOfItem
            };
            string value = string.Empty;

            value = JsonConvert.SerializeObject(v, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTitleList()
        {
            List<ModelTitle> TitleList = db.Titles.Select(model =>
               new ModelTitle
               {
                   Id_Title = model.Id_Title,
                   TitleName = model.Name,
                   Id_Type = model.Id_TypeDisk,
                   TypeName = model.TypeDisk.NameType,
                   Count = model.CountOfItem
               }).ToList();
            return Json(TitleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTitleInDatabase(int Id_Title, string Name_Title, int Type)
        {
            var result = false;
            try
            {
                Title item = db.Titles.SingleOrDefault(x => x.Id_Title == Id_Title);
                if (item != null)
                {
                    item.Name = Name_Title;
                    item.Id_TypeDisk = Type;
                    db.SaveChanges();
                }
                else
                {
                    Title t = new Title();
                    t.Name = Name_Title;
                    t.Id_TypeDisk = Type;
                    t.CountOfItem = 0;
                    db.Titles.Add(t);
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
        //[HttpPost]
        //public JsonResult DeleteTitleRecord(int Id_Title)
        //{
        //    bool result = false;
        //    Title TT = db.Titles.SingleOrDefault(x => x.Id_Title == Id_Title);
        //  List<OrderDetail> Ord = db.OrderDetails.Where(x => x.Id_Title == Id_Title).ToList();


        //    foreach (var item in Ord)
        //    {
        //        db.OrderDetails.Remove(item);
        //    }
        //    //thiếu phần xóa ở reservation nữa
        //    db.Items.Remove(It);
        //    db.SaveChanges();
        //    result = true;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult UpdateTitle(int id)
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