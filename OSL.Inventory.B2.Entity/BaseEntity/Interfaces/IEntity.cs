using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Entity.BaseEntity.Interfaces
{
    public interface IEntity
    {
        object Id { get; set; }
        int Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object ModifiedBy { get; set; }
    }
}
