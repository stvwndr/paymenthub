using PaymentHub.Getnet.Infra.Dtos;

namespace PaymentHub.Getnet.Infra.Services.Interfaces;

public interface IGetnetService
{
    Task<byte[]> GetPixQrCode(PixRequestDto requestDto);
}
