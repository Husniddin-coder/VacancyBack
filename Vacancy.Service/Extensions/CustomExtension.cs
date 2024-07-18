using System.Security.Cryptography;
using System.Text;

namespace Vacancy.Service.Extensions;

public static class CustomExtension
{
    public static string GetHash(this string value)
    {
        if (value == null) return string.Empty;

        using SHA256 hmacSHA256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(value);
        byte[] hashedBytes = hmacSHA256.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashedBytes)
        {
            sb.Append(b.ToString());
        }
        return sb.ToString();
    }
}
