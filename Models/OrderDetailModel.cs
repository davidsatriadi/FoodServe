using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServe.Models
{
    public class OrderDetailModel
    {
        public string OrderId { get; set; }
        public string DetailId { get; set; }
        public string FoodId { get; set; }
        public decimal Qty { get; set; }

        public FoodModel FoodModel { get; set; }
        public FoodOrderModel OrderModel { get; set; }
    }
}
