using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace CoffeeSlotMachine.Persistence
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CoinRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToDepot(int coinValue) =>
             _dbContext.Coins.Select(c => c.Amount + coinValue);
        

        public IEnumerable<Coin> GetAllCoinsSorted() =>
            _dbContext.Coins
            .OrderByDescending(c => c.CoinValue)
            .ToArray();

        
        
    }
}
