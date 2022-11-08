﻿using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;
using OSL.Inventory.B2.Entity.BaseEntity;

namespace OSL.Inventory.B2.Entity
{
    public class Customer : BaseEntity<long>, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
