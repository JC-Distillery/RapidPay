using BussinesLogic.Dtos;
using BussinesLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System.Net;

namespace BussinesLogic
{
    public class CardManagementBL : ICardManagementBL
    {
        private readonly ICardRepository _cardRepository;
        private readonly IBalanceRepository _balanceRepository;

        public CardManagementBL(ICardRepository cardRepository, IBalanceRepository balanceRepository)
        {
            _cardRepository = cardRepository; 
            _balanceRepository = balanceRepository;
        }
        public async Task<CommonResponseDto<bool>> CreateCard(CardRequestDto requestDto)
        {
            var result = new CommonResponseDto<bool>();
            if (string.IsNullOrEmpty(requestDto.CardNumber) || requestDto.CreditAmount <= 0)
            {
                result.Message = "Insert a valid cardNumber or valid Amount";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            else 
            {
                var card = new Card()
                {
                    Id = Guid.NewGuid(),
                    CardNumber = requestDto.CardNumber,
                    CreationDatetime = DateTime.Now,
                    CreditAmount = requestDto.CreditAmount
                };

                var dataResponse = await _cardRepository.CreateCard(card);
                result.Response = dataResponse;

                if (dataResponse)
                {
                    var balance = new Balance()
                    {
                        Id = Guid.NewGuid(),
                        CardId = card.Id,
                        Amount = 0,
                        CurrentBalance = card.CreditAmount,
                        DateTimeOperation = DateTime.Now,
                        Type = "I"
                    };
                    await _balanceRepository.AddBalance(balance);

                    result.Message = string.Empty;
                    result.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    result.Message = "Something went wrong, try later!";
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                }
            }
            

            return result;
        }

        public async Task<CommonResponseDto<bool>> PayCard(PaymentRequestDto payment)
        {
            var result = new CommonResponseDto<bool>();
            if (payment.Type.Equals("I") || payment.Type.Equals("E"))
            {
                var cardInfo = await GetCardByNumber(payment.CardNumber);
                if (cardInfo!= null && !string.IsNullOrEmpty(cardInfo.CardNumber))
                {
                    var balance = new Balance();
                    var currentBalance = await GetAvailableBalance(cardInfo.Id);
                    balance.Id = Guid.NewGuid();
                    balance.CardId = cardInfo.Id;
                    balance.Amount = Math.Round(payment.PaymentAmout, 2);
                    balance.DateTimeOperation = DateTime.Now;
                    if (payment.Type.Equals("E"))
                    {

                        if (currentBalance >= payment.PaymentAmout)
                        {
                            var feed = FeedValue.Instance;
                            balance.Feed = feed.RandomDecimal;
                            balance.AmountWithFeed = Math.Round( balance.Amount * (1 + balance.Feed),2);
                            balance.CurrentBalance = Math.Round(currentBalance - balance.AmountWithFeed, 2);                           
                            balance.Type = payment.Type;
                        }
                        else
                        {
                            result.Response = false;
                            result.Message = "Insufficient funds!";
                            result.HttpStatusCode = HttpStatusCode.BadRequest;
                        }
                    }
                    else
                    {

                        balance.Feed = 0;
                        balance.AmountWithFeed = 0;
                        balance.CurrentBalance = Math.Round(currentBalance + balance.Amount, 2);                       
                        balance.Type = payment.Type;
                    }

                   
                    var dataResponse = await _balanceRepository.AddBalance(balance);
                    result.Response = dataResponse;

                    if (dataResponse)
                    {
                        result.Message = string.Empty;
                        result.HttpStatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        result.Message = "Something went wrong, try later!";
                        result.HttpStatusCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    result.Response = false;
                    result.Message = "Card number doesn't exist!";
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                }
            }
            else 
            {
                result.Response = false;
                result.Message = "Invalid Type, must be: I for income or E for expenses";
                result.HttpStatusCode = HttpStatusCode.BadRequest;               
               
            }

            return result;
        }

        public async Task<CommonResponseDto<BalanceResponseDto>> GetCardBalance(string cardNumber)
        {
            var result = new CommonResponseDto<BalanceResponseDto>();

            var cardInfo = await GetCardByNumber(cardNumber);
            if (cardInfo != null)
            {
                var balanceResponse = new BalanceResponseDto();
                balanceResponse.CardId = cardInfo.Id;
                balanceResponse.CardNumber = cardInfo.CardNumber;
                balanceResponse.CreditAmount = cardInfo.CreditAmount;
                balanceResponse.Balance = await GetAvailableBalance(cardInfo.Id);
                balanceResponse.BalanceDetails = await GetBalanceDetail(cardInfo.Id);

                result.Response = balanceResponse;
                result.Message = String.Empty;
                result.HttpStatusCode = HttpStatusCode.OK;
            }
            else 
            {
                result.Response = null;
                result.Message = "Card number doesn't exist!";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
            }

            return result;
        }


        #region Private methods
        private async Task<Card> GetCardByNumber(string cardNumber) 
        {
            Card response = null;

            response = await _cardRepository.GetCard(cardNumber);           

            return response;
        }

        private async Task<decimal> GetAvailableBalance(Guid cardId)
        {
            decimal result = 0;
            var balanceInfo = await _balanceRepository.GetBalanceById(cardId);
            if (balanceInfo != null)
            {
                result = balanceInfo.CurrentBalance;
            }

            return result;
        }

        private async Task<List<BalanceDetail>> GetBalanceDetail(Guid cardId)
        {
            var result = new List<BalanceDetail>();           

            var details = await _balanceRepository.GetAllBalance(cardId);
            if (details.Any())
            {
                result = details.Select(s => new BalanceDetail() 
                {
                    Amount = s.Amount,
                    Feed =  s.Feed,
                    AmountWithFeed = s.AmountWithFeed,
                    CurrentBalance = s.CurrentBalance,
                    DateTimeOperation = s.DateTimeOperation,
                    Type = s.Type
                }).ToList();
            }

            return result;
        }
        #endregion

    }
}
