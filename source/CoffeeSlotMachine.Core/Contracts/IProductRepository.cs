﻿using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        bool FindProduct(Product product);


    }
}