using System.ComponentModel.DataAnnotations;


namespace BussinesLogic.Dtos
{
    public class CardRequestDto
    {
        [Required(ErrorMessage = "Please enter a valid card number"), MaxLength(15)]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a valid Amount")]
        public decimal CreditAmount { get; set; }
    }
}
