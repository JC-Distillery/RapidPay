

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Balance
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public decimal Amount { get; set; }
        public decimal Feed { get; set; }
        public decimal AmountWithFeed { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime DateTimeOperation { get; set; }
        public string Type { get; set; }
    }
}
