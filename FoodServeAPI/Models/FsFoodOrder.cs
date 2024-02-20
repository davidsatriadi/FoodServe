using System;
using System.Collections.Generic;

namespace FoodServeAPI.Models
{
    public partial class FsFoodOrder
    {
        public FsFoodOrder()
        {
            FsOrderDetail = new HashSet<FsOrderDetail>();
        }

        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string IsClose { get; set; }
        public decimal? QtyTotal { get; set; }
        public decimal? PriceTotal { get; set; }

        public FsUser User { get; set; }
        public ICollection<FsOrderDetail> FsOrderDetail { get; set; }
    }
}
