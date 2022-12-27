using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, Core.Entities.User>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<Core.Entities.User>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.id);
        
        if (user is null)
        {
            return Result.Failure<Core.Entities.User>(new Error(
                "User.NotFound",
                $"The user with id {request.id} was not found"));
        }

        return user;
    }
}