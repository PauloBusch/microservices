using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = await _context.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var result = await _context.Products.ReplaceOneAsync(filter, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
