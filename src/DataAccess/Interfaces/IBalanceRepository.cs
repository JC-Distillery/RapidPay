using DataAccess.Models;


namespace DataAccess.Interfaces
{
    public interface IBalanceRepository
    {
        Task<Balance> GetBalanceById(Guid cardId);
        Task<List<Balance>> GetAllBalance(Guid cardId);

        Task<bool> AddBalance(Balance balance);
    }
}
