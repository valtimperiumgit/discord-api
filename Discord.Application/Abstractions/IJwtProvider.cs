namespace Discord.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(Core.Entities.User user);
}
