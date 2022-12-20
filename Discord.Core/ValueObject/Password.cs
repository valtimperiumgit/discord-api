using Discord.Core.Errors;
using Discord.Core.Shared;

namespace Discord.Core.ValueObject;

public class Password : Primitives.ValueObject
{
    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result.Failure<Password>(DomainErrors.Password.Empty);
        }

        if (password.Length < 6)
        {
            return Result.Failure<Password>(DomainErrors.Password.InvalidPassword);
        }

        return new Password(password);
    }
    
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}