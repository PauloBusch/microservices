using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(string id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllByCategoryAsync(string category);

        Task<bool> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
