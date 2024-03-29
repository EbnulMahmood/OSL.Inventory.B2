﻿using Humanizer;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System.Linq;

namespace OSL.Inventory.B2.Service.Extensions
{
    public static class DtoConversions
    {
        // category
        private static CategoryDto NewCategoryDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = (DTOs.Enums.StatusDto)category.Status,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt,
                CreatedBy = category.CreatedBy,
                ModifiedBy = category.ModifiedBy,
            };
        }

        private static Category NewCategory(CategoryDto categoryDto)
        {
            return new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Status = (Entity.Enums.Status)categoryDto.Status,
                CreatedAt = categoryDto.CreatedAt,
                ModifiedAt = categoryDto.ModifiedAt,
                CreatedBy = categoryDto.CreatedBy,
                ModifiedBy = categoryDto.ModifiedBy,
            };
        }

        private static string ConditionClassStatus(StatusDto statusDto)
        {
            return statusDto == StatusDto.Active ? "success" : "warning";
        }

        private static string ConditionTextStatus(StatusDto statusDto)
        {
            return statusDto == StatusDto.Active ? "Active" : "Inactive";
        }

        private static string ActionLinks(string actionName, long id)
        {
            return $"<div class='btn-group' role='group'>" +
                        $"<a href='{actionName}/Edit/{id}' class='btn btn-primary mx-1'><i class='bi bi-pencil-square'></i>Edit</a>" +
                        $"<button type='button' data-bs-target='#delete{actionName}' data-bs-toggle='ajax-modal'" +
                            $"class='btn btn-danger mx-1 btn-{actionName.ToLower()}-delete' data-{actionName.ToLower()}-id='{id}'>" +
                            $"<i class='bi bi-trash-fill'></i>Delete</button>" +
                        $"<a href='{actionName}/Details/{id}' class='btn btn-info mx-1'><i class='bi bi-ticket-detailed-fill'>" +
                            $"</i>Details</a>" +
                    $"</div>";
        }

        public static IEnumerable<CategoryDatatableViewDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new CategoryDatatableViewDto
                    {
                        Name = category.Name,
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)category.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)category.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Category", category.Id),
                    }).ToList();
        }

        public static CategoryDto ConvertToDto(this Category category)
        {
            return NewCategoryDto(category);
        }

        public static Category ConvertToEntity(this CategoryDto categoryDto)
        {
            return NewCategory(categoryDto);
        }

        // product


        public static IEnumerable<ProductDatatableViewDto> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDatatableViewDto
                    {
                        Name = product.Name,
                        InStockString = $"{product.InStock} {product.BasicUnit}",
                        PricePerUnitString = product.PricePerUnit.ToString(),
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)product.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)product.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Product", product.Id),
                    }).ToList();
        } 

        public static ProductDto ConvertToDto(this Product product)
        {
            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Limited = product.Limited,
                InStock = product.InStock,
                PricePerUnit = product.PricePerUnit,
                BasicUnit = (BasicUnitDto)product.BasicUnit,
                CategoryId = product.CategoryId,
            };
        }

        public static Product ConvertToEntity(this ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                Limited = productDto.Limited,
                InStock = productDto.InStock,
                PricePerUnit = productDto.PricePerUnit,
                BasicUnit = (Entity.Enums.BasicUnit)productDto.BasicUnit,
                CategoryId = productDto.CategoryId,
            };
        }

        // purchase
        public static IEnumerable<PurchaseDatatableViewDto> ConvertToDto(this IEnumerable<Purchase> purchases)
        {
            return (from purchase in purchases
                    select new PurchaseDatatableViewDto
                    {
                        PurchaseCode = purchase.PurchaseCode,
                        PurchaseAmount = purchase.PurchaseAmount,
                        PurchaseDate = purchase.PurchaseDate.ToUniversalTime().Humanize(),
                        PurchaseAmountPaid = purchase.PurchaseAmountPaid?.ToString() ?? "<span class='text-warning'>Unpaid</span>",
                        AmountPaidTime = purchase.AmountPaidTime?.ToUniversalTime().Humanize() ?? "<span class='text-warning'>Unpaid</span>",
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)purchase.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)purchase.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Purchase", purchase.Id),
                    }).ToList();
        }
        
        // sale
        public static IEnumerable<SaleDatatableViewDto> ConvertToDto(this IEnumerable<Sale> sales)
        {
            return (from sale in sales
                    select new SaleDatatableViewDto
                    {
                        SaleCode = sale.SaleCode,
                        SaleAmount = sale.SaleAmount,
                        SaleDate = sale.SaleDate.ToUniversalTime().Humanize(),
                        SaleAmountPaid = sale.SaleAmountPaid?.ToString() ?? "<span class='text-warning'>Unpaid</span>",
                        AmountPaidTime = sale.AmountPaidTime?.ToUniversalTime().Humanize() ?? "<span class='text-warning'>Unpaid</span>",
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)sale.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)sale.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Sale", sale.Id),
                    }).ToList();
        }

        // customer
        public static IEnumerable<CustomerDatatableViewDto> ConvertToDto(this IEnumerable<Customer> customers)
        {
            return (from customer in customers
                    select new CustomerDatatableViewDto
                    {
                        Name = $"{customer.FirstName} {customer.LastName}",
                        EmailAddress = customer.EmailAddress,
                        PhoneNumber = customer.PhoneNumber,
                        Address = $"{customer.City}, {customer.State}, {customer.ZipCode}, {customer.Country}",
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)customer.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)customer.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Customer", customer.Id),
                    }).ToList();
        }

        // supplier
        public static IEnumerable<SupplierDatatableViewDto> ConvertToDto(this IEnumerable<Supplier> suppliers)
        {
            return (from supplier in suppliers
                    select new SupplierDatatableViewDto
                    {
                        Name = $"{supplier.FirstName} {supplier.LastName}",
                        EmailAddress = supplier.EmailAddress,
                        PhoneNumber = supplier.PhoneNumber,
                        Address = $"{supplier.City}, {supplier.State}, {supplier.ZipCode}, {supplier.Country}",
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)supplier.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)supplier.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Supplier", supplier.Id),
                    }).ToList();
        }
    }
}
