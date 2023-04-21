using BussinesLogic;
using BussinesLogic.Dtos;
using BussinesLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        private readonly ICardManagementBL _cardManagementBL;
        public CardManagementController(ICardManagementBL cardManagementBL)
        {
            _cardManagementBL = cardManagementBL;
        }

        [HttpPost]
        [Route("RegisterCard")]
        public async Task<IActionResult> CreateCard([FromBody]CardRequestDto requestDto) 
        {
            if (requestDto == null)
            {
                return BadRequest();
            }
            else 
            {
                return Ok(await _cardManagementBL.CreateCard(requestDto));
            }
        }

        [HttpPost]
        [Route("Payment")]
        public async Task<IActionResult> PayCard([FromBody] PaymentRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _cardManagementBL.PayCard(requestDto));
            }
        }

        [HttpGet]
        [Route("Balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _cardManagementBL.GetCardBalance(cardNumber));
            }
        }

       
    }
}
