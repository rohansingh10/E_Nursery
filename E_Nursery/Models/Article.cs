using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_Nursery.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }
        public string PlantName { get; set; }
        public string Articles { get; set; }
    }
}