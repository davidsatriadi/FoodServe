using System;
using System.Collections.Generic;

namespace FoodServeAPI.Models
{
    public partial class FsUser
    {
        public FsUser()
        {
            FsFoodOrder = new HashSet<FsFoodOrder>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime? DoB { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string IsActive { get; set; }

        public FsRole RoleNavigation { get; set; }
        public ICollection<FsFoodOrder> FsFoodOrder { get; set; }
    }
}
