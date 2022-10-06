using QRCoder;

namespace PaymentHub.Core.Factories;

public static class QrCodeFactory
{
    public static byte[] GenerateQrCode(string input)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(20);
    }
}
