using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public CatalogController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<Product> GetAsync([FromRoute] string id) => await _repository.GetAsync(id);

        [HttpGet("category/{category}")]
        public async Task<IEnumerable<Product>> GetAllByCategoryAsync([FromRoute] string category) => await _repository.GetAllByCategoryAsync(category);

        [HttpPost]
        public async Task<bool> PostAsync([FromBody] Product product) => await _repository.CreateAsync(product);

        [HttpPut]
        public async Task<bool> PutAsync([FromBody] Product product) => await _repository.UpdateAsync(product);

        [HttpDelete]
        public async Task<bool> DeleteAsync([FromRoute] string id) => await _repository.DeleteAsync(id);
    }
}
