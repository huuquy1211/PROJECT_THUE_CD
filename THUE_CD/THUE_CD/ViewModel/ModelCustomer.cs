using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THUE_CD.ViewCustomer
{
    public class ModelCustomer
    {
        public int Id_Customer { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Fine { get; set; }

    }
}