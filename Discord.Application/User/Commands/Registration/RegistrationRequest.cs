namespace Discord.Application.User.Commands.Registration;

public sealed record RegistrationRequest(
    string Email,
    string Password,
    string Name,
    int Year,
    int Month,
    int Day,
    bool IsAcceptNewsletters
    );
