using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Service.DTOs;
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

        public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Status = (DTOs.Enums.StatusDto)category.Status,
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
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Limited = product.Limited,
                        InStock = product.InStock,
                        PricePerUnit = product.PricePerUnit,
                        BasicUnit = product.BasicUnit,
                        Status = (DTOs.Enums.StatusDto)product.Status,
                        CategoryId = product.Category.Id,
                        CategoryName = product.Category.Name,
                        CreatedAt = product.CreatedAt,
                        ModifiedAt = product.ModifiedAt,
                        CreatedBy = product.CreatedBy,
                        ModifiedBy = product.ModifiedBy,
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
