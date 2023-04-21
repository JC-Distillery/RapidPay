using DataAccess.Models;


namespace DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<bool> CreateCard(Card cards);
        Task<Card> GetCard(string cardNumber);
    }
}
