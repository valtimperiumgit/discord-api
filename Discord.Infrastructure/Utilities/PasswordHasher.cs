using System.Security.Cryptography;
using System.Text;
using Discord.Application.Abstractions;

namespace Discord.Infrastructure.Utilities;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        MD5 md5 = MD5.Create();

        byte[] bytes = Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(bytes);

        StringBuilder builder = new StringBuilder();
        foreach (var a in hash)
        {
            builder.Append(a.ToString("X2"));
        }
        
        return builder.ToString();
    }
}