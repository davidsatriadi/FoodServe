using System;
using System.Collections.Generic;

namespace FoodServeAPI.Models
{
    public partial class FsFood
    {
        public FsFood()
        {
            FsOrderDetail = new HashSet<FsOrderDetail>();
        }

        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public DateTime ActiveDate { get; set; }
        public string IsActive { get; set; }

        public ICollection<FsOrderDetail> FsOrderDetail { get; set; }
    }
}
