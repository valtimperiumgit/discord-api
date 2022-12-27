using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Queries.GetUserByNameAndTag;

public class GetUserByNameAndTagQueryHandler
    : IQueryHandler<GetUserByNameAndTagQuery, Core.Entities.User>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByNameAndTagQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<Core.Entities.User>> Handle(
        GetUserByNameAndTagQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByNameAndTag(request.name, request.tag);
        
        if (user is null)
        {
            return Result.Failure<Core.Entities.User>(new Error(
                "User.NotFound",
                $"The user with name {request.name} and tag {request.tag} was not found"));
        }

        return user;
    }
}