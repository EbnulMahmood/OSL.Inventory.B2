﻿using OSL.Inventory.B2.Entity.BaseEntity;
using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;

namespace OSL.Inventory.B2.Entity
{
    public class User : BaseEntity<long>, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
