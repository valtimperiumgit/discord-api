using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Queries.GetUserByEmail;

public sealed class GetUserByEmailQueryHandler
    : IQueryHandler<GetUserByEmailQuery, Core.Entities.User>
{
    
    private readonly IUserRepository _userRepository;
    
    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Core.Entities.User>> Handle(
        GetUserByEmailQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.email);
        
        if (user is null)
        {
            return Result.Failure<Core.Entities.User>(new Error(
                "User.NotFound",
                $"The user with email {request.email} was not found"));
        }

        return user;
    }
}