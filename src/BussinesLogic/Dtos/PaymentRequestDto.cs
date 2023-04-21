
using System.ComponentModel.DataAnnotations;


namespace BussinesLogic.Dtos
{
    public class PaymentRequestDto
    {
        [Required(ErrorMessage = "Please enter a valid card number"), MaxLength(16)]
        public string CardNumber { get; set; }
        public decimal PaymentAmout { get; set; }

        [Required(ErrorMessage = "Please enter a valid type (I)Income, (E)Expense"), MaxLength(1)]
        public string Type { get; set; }
    }
}
