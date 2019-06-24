using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}