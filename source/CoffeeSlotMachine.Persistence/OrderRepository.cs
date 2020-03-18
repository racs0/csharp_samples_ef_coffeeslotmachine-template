﻿using System;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSlotMachine.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrder(Order order) =>
            _dbContext.Orders.Add(order);

        public IEnumerable<Order> GetAllWithProduct()
        {
            return _dbContext.Orders.ToArray();
        }

    }
}
