using System.Text;
using System.Security.Cryptography;

namespace PaymentHub.Core.Helpers;

public static class CryptoHelper
{
    public static string Crypt(string message, string key)
    {
        string ret = string.Empty;
        if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(key))
        {
            try
            {
                var des = TripleDES.Create("TripleDES");
                var hashMD5 = MD5.Create();
                des!.Key = hashMD5.ComputeHash(Encoding.ASCII.GetBytes(key));
                des.Mode = System.Security.Cryptography.CipherMode.ECB;
                var transform = des.CreateEncryptor();
                var bytes = Encoding.ASCII.GetBytes(message);
                ret = Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch
            {
                ret = string.Empty;
            }
        }
        return ret;
    }

    public static string Decrypt(string encryptedMessage, string key)
    {
        var ret = string.Empty;

        if (!string.IsNullOrEmpty(encryptedMessage) && !string.IsNullOrEmpty(key))
        {
            try
            {
                var des = TripleDES.Create("TripleDES");
                var hashMD5 = MD5.Create();
                des!.Key = hashMD5.ComputeHash(Encoding.ASCII.GetBytes(key));
                des.Mode = System.Security.Cryptography.CipherMode.ECB;
                var transform = des.CreateDecryptor();
                var inputBuffer = Convert.FromBase64String(encryptedMessage);
                ret = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                ret = string.Empty;
            }
        }

        return ret;
    }
}
