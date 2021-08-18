using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_Nursery.Models
{
    public class AdminAccount
    {
        [Key]
        public int AdminID { get; set; }
        [Required(ErrorMessage = "Login ID is required")]
        public string LoginID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}