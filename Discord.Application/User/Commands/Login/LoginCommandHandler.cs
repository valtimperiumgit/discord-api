using Discord.Application.Abstractions;
using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;
using Discord.Core.ValueObject;

namespace Discord.Application.User.Commands.Login;

internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUserRepository userRepository, 
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Result<Email> email = Email.Create(request.Email);
        if (email.IsFailure)
        {
            return Result.Failure<string>(email.Error);
        }
        
        Result<Password> password = Password.Create(request.Password);
        if (password.IsFailure)
        {
            return Result.Failure<string>(password.Error);
        }
        
        Core.Entities.User? user = await _userRepository.GetUserByEmail(email.Value.Value);

        if (user is null || 
            _passwordHasher.Hash(password.Value.Value) 
            != user.Password.Value
            )
        {
            return Result.Failure<string>(DomainErrors.User.InvalidCredentials);
        }
        
        string token = _jwtProvider.Generate(user);

        return token;
    }
}