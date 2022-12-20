using Discord.Application.Abstractions;
using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;
using Discord.Core.ValueObject;

namespace Discord.Application.User.Commands.Registration;

public class RegistrationCommandHandler :
    ICommandHandler<RegistrationCommand, Core.Entities.User>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegistrationCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<Result<Core.Entities.User>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        Result<Email> email = Email.Create(request.request.Email);
        if (email.IsFailure)
        {
            return Result.Failure<Core.Entities.User>(email.Error);
        }

        var user = await _userRepository.GetUserByEmail(email.Value.Value);
        if (user is not null)
        {
            return Result.Failure<Core.Entities.User>(DomainErrors.User.EmailAlreadyExist);
        }
        
        Result<Password> password = Password.Create(request.request.Password);
        if (password.IsFailure)
        {
            return Result.Failure<Core.Entities.User>(password.Error);
        }
        
        Result<Birthday> birthday = Birthday.Create(
            request.request.Year,
            request.request.Month,
            request.request.Day);
        
        if (birthday.IsFailure)
        {
            return Result.Failure<Core.Entities.User>(birthday.Error);
        }

        await _userRepository.CreateUser(
            request.request.Email,
            request.request.Name,
            _passwordHasher.Hash(request.request.Password),
            birthday.Value,
            request.request.IsAcceptNewsletters);

        var newUser = await _userRepository.GetUserByEmail(email.Value.Value);

        return newUser;
    }
}