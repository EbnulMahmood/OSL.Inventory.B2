using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Inventory.B2.Entity
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Status { get; set; } = 1; // 1 = Active, -1 = Inactive, 404 = Delete
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }
}