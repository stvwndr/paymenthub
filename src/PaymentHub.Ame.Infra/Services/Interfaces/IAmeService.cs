using PaymentHub.Ame.Infra.Dtos;

namespace PaymentHub.Ame.Infra.Services.Interfaces;

public interface IAmeService
{
    Task<byte[]> GetQrCode(AmeQrCodeRequestDto request);
    Task<PayWithGiftCardResponseDto> PayWithGiftCard(PayWithGiftCardRequestDto request);
}
