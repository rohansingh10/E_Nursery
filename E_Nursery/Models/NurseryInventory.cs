using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nursery.Models
{
    public class NurseryInventory
    {
        [Key]
        public int InventoryID { get; set; }
        public string PlantName { get; set; }
        public string Description { get; set; }
        public string variety { get; set; }
        public string origin { get; set; }
        public string season { get; set; }
        public int discount { get; set; }
        public int stock { get; set; }
        [ForeignKey("NurseryAccount")]
        public int NurseryID { get; set; }
        public NurseryAccount NurseryAccount { get; set; }
    }
}