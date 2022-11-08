﻿using OSL.Inventory.B2.Service.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = string.Empty;
        public bool Limited { get; set; }
        [Display(Name = "In Stock")]
        public int InStock { get; set; }
        [Display(Name = "Price Per Unit")]
        public decimal PricePerUnit { get; set; }
        [Display(Name = "Basic Unit")]
        public BasicUnitDto BasicUnit { get; set; }
        [Display(Name ="Category")]
        public long CategoryId { get; set; }
    }
}
