using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THUE_CD.Models
{
    public class Account
    {
        [Key]
        public int Id_Account { get; set; }
        public string AccountName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }

    }
}