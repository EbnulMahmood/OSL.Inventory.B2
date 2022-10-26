using OSL.Inventory.B2.Entity.BaseEntity;
using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Inventory.B2.Entity
{
    public class Category : BaseEntity<long>, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}