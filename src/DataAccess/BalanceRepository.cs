using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess
{
    public class BalanceRepository : IBalanceRepository
    {
       

        public async Task<List<Balance>> GetAllBalance(Guid cardId)
        {
            var result = new List<Balance>();

            using (var context = new DataContext())
            {
                result = context.Balance
                    .OrderByDescending(o => o.DateTimeOperation)
                    .Where(b => b.CardId == cardId).ToList();

            }

            return result; 
        }

        public async Task<Balance> GetBalanceById(Guid cardId)
        {
            var balance = new Balance();
            using(var context = new DataContext()) 
            {
                balance = context.Balance
                    .OrderByDescending( o => o.DateTimeOperation)
                    .Where(b => b.CardId == cardId).FirstOrDefault();
               
            }

            return balance;
        }

        public async Task<bool> AddBalance(Balance balance)
        {
            var result = false;

            if (balance != null)
            {
                try
                {
                    using (var context = new DataContext()) 
                    {
                        await context.Balance.AddAsync(balance);
                        context.SaveChanges();
                    }
                    result = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return result;
        }
    }
}
