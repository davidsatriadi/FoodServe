using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServe.Models
{
    public class FoodOrderModel
    {
        public FoodOrderModel()
        {
            OrderDetailModel = new HashSet<OrderDetailModel>();
        }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string IsClose { get; set; }
        public decimal? QtyTotal { get; set; }
        public decimal? PriceTotal { get; set; }

        public UserModel UserModel { get; set; }
        public ICollection<OrderDetailModel> OrderDetailModel { get; set; }
    }
}

