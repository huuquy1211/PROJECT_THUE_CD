using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THUE_CD.Models;
using THUE_CD.ViewModel;

namespace THUE_CD.Controllers
{
    public class TypeDisksController : Controller
    {
        ThueDiaDB db = new ThueDiaDB();
        
        // GET: TypeDisks
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetTypeList()
        {
            List<ModelTypeDisk> value = db.Types.Select(x => new ModelTypeDisk
            {
                Id_Type = x.Id_TypeDisk,
                TypeName = x.NameType
            }).ToList();
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTypeByTitle(int Id_Title )
        {
            TypeDisk vc = db.Titles.Where(x => x.Id_Title == Id_Title).First().TypeDisk;
            string value = string.Empty;

            value = JsonConvert.SerializeObject(vc.Id_TypeDisk, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}