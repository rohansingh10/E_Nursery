using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_Nursery.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }
        public string NurseryName { get; set; }
        public string PlantName { get; set; }
        [RegularExpression("^[1-5]{1}$", ErrorMessage = "Please enter valid rating")]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}