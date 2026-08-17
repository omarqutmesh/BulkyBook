using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Business.Services.IServices
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Items, int TotalCount, int FilteredCount)> GetProductsForDataTableAsync(
    int skip, int pageSize, string? searchValue, string? sortColumn, string? sortDirection);
        Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, bool includeCategory = false);
        Task<Product?> GetProductByIdAsync(int id, bool includeCategory = false);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory = false);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);        
    }
}