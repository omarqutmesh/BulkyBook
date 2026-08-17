using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.Models.ViewModels;

namespace BulkyBook.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory = false)
        {
            if (includeCategory)
            {
                return await _context.Products.Include(u => u.Category).ToListAsync();
            }
            else
            {
                return await _context.Products.ToListAsync();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id, bool includeCategory = false)
        {
            if (includeCategory)
            {
                return await _context.Products.Include(u => u.Category).FirstOrDefaultAsync(u => u.Id == id);
            }
            else
            {
                return await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
            }

        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product {id} not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

public async Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, bool includeCategory = false)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 8;

        IQueryable<Product> query = _context.Products;
        if (includeCategory)
        {
            query = query.Include(u => u.Category);
        }

        query = query.OrderBy(p => p.Id);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
        public async Task<(IEnumerable<Product> Items, int TotalCount, int FilteredCount)> GetProductsForDataTableAsync(
    int skip, int pageSize, string? searchValue, string? sortColumn, string? sortDirection)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category);

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var search = searchValue.Trim().ToLower();
                query = query.Where(p =>
                    p.Title.ToLower().Contains(search) ||
                    p.Author.ToLower().Contains(search) ||
                    p.ISBN.ToLower().Contains(search));
            }

            var filteredCount = await query.CountAsync();

            bool desc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);
            query = sortColumn switch
            {
                "title" => desc ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                "isbn" => desc ? query.OrderByDescending(p => p.ISBN) : query.OrderBy(p => p.ISBN),
                "price" => desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "author" => desc ? query.OrderByDescending(p => p.Author) : query.OrderBy(p => p.Author),
                _ => query.OrderBy(p => p.Id)
            };

            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, totalCount, filteredCount);
        }

    }
}