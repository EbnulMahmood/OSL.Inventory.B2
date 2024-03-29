﻿using System.Collections.Generic;
using System;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using OSL.Inventory.B2.Service.DTOs.BaseDto;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SaleDto : BaseDto<long>, IDto
    {
        public string SaleCode { get; set; } = string.Empty;
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal? SaleAmountPaid { get; set; } = 0;
        public DateTime? AmountPaidTime { get; set; }
        public long CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public virtual ICollection<SaleDetailDto> SaleDetailsDto { get; set; }
    }
}
