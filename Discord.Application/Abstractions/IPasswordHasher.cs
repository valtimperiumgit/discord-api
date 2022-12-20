namespace Discord.Application.Abstractions;

public interface IPasswordHasher
{
    public string Hash(string password);
}