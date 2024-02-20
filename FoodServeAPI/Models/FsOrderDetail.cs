using System;
using System.Collections.Generic;

namespace FoodServeAPI.Models
{
    public partial class FsOrderDetail
    {
        public string OrderId { get; set; }
        public string DetailId { get; set; }
        public string FoodId { get; set; }
        public decimal Qty { get; set; }

        public FsFood Food { get; set; }
        public FsFoodOrder Order { get; set; }
    }
}
