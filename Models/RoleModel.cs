using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServe.Models
{
    public class RoleModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string IsActive { get; set; }
    }
}
