using System.Security.Cryptography;
using System.Text;

namespace DrugIndication.Domain.Helpers
{
    public static class HashHelper
    {
        public static string Hash(string content)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(content));
            return Convert.ToHexString(bytes);
        }
    }
}
