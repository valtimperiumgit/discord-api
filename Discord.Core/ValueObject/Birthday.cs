using Discord.Core.Errors;
using Discord.Core.Shared;

namespace Discord.Core.ValueObject;

public sealed class Birthday : Primitives.ValueObject
{
    private Birthday(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public int Year { get; }

    public int Month { get; }
    
    public int Day { get; }
    
    public static Result<Birthday> Create(int year, int month, int day)
    {
        if (year > (DateTime.Today.Year - 4) || year < (DateTime.Today.Year - 200))
        {
            return Result.Failure<Birthday>(DomainErrors.Birthday.InvalidYear);
        }

        if (month is < 0 or > 12)
        {
            return Result.Failure<Birthday>(DomainErrors.Birthday.InvalidMonth);
        }
        
        if (day is < 0 or > 31)
        {
            return Result.Failure<Birthday>(DomainErrors.Birthday.InvalidDay);
        }

        return new Birthday(year, month, day);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Year;
    }
}