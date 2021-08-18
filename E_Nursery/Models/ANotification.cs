using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_Nursery.Models
{
    public class ANotification
    {
        [Key]
        public int NotiID { get; set; }
        public string details { get; set; }
    }
}