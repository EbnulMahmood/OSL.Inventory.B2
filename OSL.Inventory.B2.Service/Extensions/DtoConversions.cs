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

        public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new CategoryDto
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

        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDto
                    {
                        Name = product.Name,
                        StatusHtml = $"<span class='text text-{ConditionClassStatus((StatusDto)product.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)product.Status)}</span>",
                        ActionLinkHtml = ActionLinks("Product", product.Id),
                    }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Limited = product.Limited,
                InStock = product.InStock,
                PricePerUnit = product.PricePerUnit,
                BasicUnit = product.BasicUnit,
                Status = (DTOs.Enums.StatusDto)product.Status,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt,
                CreatedBy = product.CreatedBy,
                ModifiedBy = product.ModifiedBy,
            };
        }

        public static Product ConvertToEntity(this ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                Limited = productDto.Limited,
                InStock = productDto.InStock,
                PricePerUnit = productDto.PricePerUnit,
                BasicUnit = productDto.BasicUnit,
                Status = (Entity.Enums.Status)productDto.Status,
                // CategoryId = productDto.CategoryId,
                CreatedAt = productDto.CreatedAt,
                ModifiedAt = productDto.ModifiedAt,
                CreatedBy = productDto.CreatedBy,
                ModifiedBy = productDto.ModifiedBy,
            };
        }
    }
}
