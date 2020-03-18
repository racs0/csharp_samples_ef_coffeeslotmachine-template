using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSlotMachine.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool FindProduct(Product product) =>
            _dbContext.Products.Any(p => p.Name == product.Name);
       

        public IEnumerable<Product> GetAllProducts() =>
            _dbContext.Products
            .OrderBy(p => p.Name)
            .ToArray();
        

    }
}
