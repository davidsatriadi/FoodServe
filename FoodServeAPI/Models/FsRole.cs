using System;
using System.Collections.Generic;

namespace FoodServeAPI.Models
{
    public partial class FsRole
    {
        public FsRole()
        {
            FsUser = new HashSet<FsUser>();
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string IsActive { get; set; }
         
        public ICollection<FsUser> FsUser { get; set; }
    }
}
