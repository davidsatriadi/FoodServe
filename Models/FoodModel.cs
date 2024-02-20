using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServe.Models
{
    [Table("FS_Food")]
    public class FoodModel
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public DateTime ActiveDate { get; set; }
        public string IsActive { get; set; }

        public ICollection<OrderDetailModel> OrderDetailModel { get; set; }
    }
}
