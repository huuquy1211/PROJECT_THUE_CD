using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using THUE_CD.Models;
namespace THUE_CD.ViewModel
{
    public class ModelItem
    {
        ThueDiaDB db = new ThueDiaDB();
        public int Id_Item { get; set; }
        public int Id_Title { get; set; }
        public int Id_TypeDisk { get; set; }
        public string Status { get; set; }
        public string TitleName { get; set; }
        public string TypeName { get; set; }

        //public ModelItem(Item item)
        //{
        //    this.Id_Item = item.Id_Item;
        //    this.Id_Title = item.Id_Title;
        //    this.TitleName = item.Titles.Name;
        //    this.Id_TypeDisk = item.Titles.Id_TypeDisk;
        //    this.TypeName = item.Titles.TypeDisk.NameType;
        //}
    }
}