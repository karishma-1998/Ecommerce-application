using ProjectAssignment.Models;

namespace ProjectAssignment.ProductRepo
{
    public interface IProductRepository
    {


        // Get products by categoryname
        Task<IEnumerable<Products>> GetProductsByCategoryNameAsync(string categoryName);

        // Search products by name
        Task<IEnumerable<Products>> SearchByNameAsync(string name);
    }
}
