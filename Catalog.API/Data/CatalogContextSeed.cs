using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertMany(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new [] {
                new Product {
                    Id = "68940aea-0cee-42fc-a87a-82fd2ef331cd",
                    Name = "Samsung 9",
                    Description = "e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido " +
                    "pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos.",
                    Image = "product-1.png",
                    Price = 850.00M,
                    Category = "Smart Phone 1"
                },
                new Product { 
                    Id = "615072e1-7323-495e-a9fe-8df5cd20b9f2",
                    Name = "Samsung 10",
                    Description = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica " +
                    "e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido " +
                    "pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos.",
                    Image = "product-2.png",
                    Price = 850.00M,
                    Category = "Smart Phone 2"
                }
            };
        }
    }
}
