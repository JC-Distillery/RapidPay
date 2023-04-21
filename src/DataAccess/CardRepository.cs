using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess
{
    public class CardRepository : ICardRepository
    {
        public async Task<bool> CreateCard(Card card)
        {
            var result = false;
            if (card != null)
            {
                try
                {
                    using (var context = new DataContext())
                    {
                        await context.Card.AddAsync(card);
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

        public async Task<Card> GetCard(string cardNumber)
        {
            var card = new Card();
            if (!string.IsNullOrEmpty(cardNumber))
            {
                using (var context = new DataContext()) 
                {
                    card =  context.Card.FirstOrDefault (x => x.CardNumber.Equals(cardNumber));
                }
            }


            return card;
        }
    }
}
