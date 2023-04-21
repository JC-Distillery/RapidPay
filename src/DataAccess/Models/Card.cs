

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime CreationDatetime { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
