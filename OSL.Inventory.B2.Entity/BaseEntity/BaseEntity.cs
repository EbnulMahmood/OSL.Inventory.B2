using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Entity.BaseEntity
{
    public abstract class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; }
        public object CreatedBy { get; set; }
        public object ModifiedBy { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        object IEntity.CreatedBy
        {
            get { return CreatedBy; }
            set { }
        }

        object IEntity.ModifiedBy
        {
            get { return ModifiedBy; }
            set { }
        }
    }
}
