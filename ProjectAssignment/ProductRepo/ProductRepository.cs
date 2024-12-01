using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectAssignment.Data;
using ProjectAssignment.Models;
using System;

namespace ProjectAssignment.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<ProductRepository> _logger;


        // Constructor Dependency Injection for EcommerceDbContext,logger
        public ProductRepository(EcommerceDbContext context,ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get products by category name
        public async Task<IEnumerable<Products>> GetProductsByCategoryNameAsync(string categoryName)
        {
            
            try
            {
                return await (from product in _context.Product
                              join category in _context.Category
                              on product.CategoryId equals category.Id
                              where category.Name == categoryName
                              select product).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the products by category {categoryName}", categoryName);
                throw;
            }
            
        }

        public async Task<IEnumerable<Products>> SearchByNameAsync(string name)
        {
            try
            {
                return await _context.Product
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for products with name {Name}.", name);
                throw;
            }
        }

        

        
    }
}
