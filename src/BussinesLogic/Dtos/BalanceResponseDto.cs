

namespace BussinesLogic.Dtos
{
    public class BalanceResponseDto
    {
        public Guid CardId { get; set; }
        public string CardNumber { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Balance { get; set; }
        public List<BalanceDetail> BalanceDetails { get; set; }

    }

    public class BalanceDetail 
    {
        public decimal Amount { get; set; }
        public decimal Feed { get; set; }
        public decimal AmountWithFeed { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime DateTimeOperation { get; set; }
        public string Type { get; set; }
    }
}
