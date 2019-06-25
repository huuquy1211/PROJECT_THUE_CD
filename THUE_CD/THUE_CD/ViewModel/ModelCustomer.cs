using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using THUE_CD.Models;

namespace THUE_CD.ViewCustomer
{
    public class ModelCustomer
    {
        public int Id_Customer { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Fine { get; set; }
        public virtual ICollection<Order> orderList { get; set; }
        public virtual ICollection<Reservation> ReservationList { get; set; }
        public ModelCustomer()
        {
            this.ReservationList = new List<Reservation>();
            this.orderList = new List<Order>();
        }
    }
}