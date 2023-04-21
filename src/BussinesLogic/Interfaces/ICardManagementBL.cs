using BussinesLogic.Dtos;


namespace BussinesLogic.Interfaces
{
    public interface ICardManagementBL
    {
        Task<CommonResponseDto<bool>> CreateCard(CardRequestDto requestDto);
        Task<CommonResponseDto<bool>> PayCard(PaymentRequestDto requestDto);
        Task<CommonResponseDto<BalanceResponseDto>> GetCardBalance(string cardNumber);
    }
}
