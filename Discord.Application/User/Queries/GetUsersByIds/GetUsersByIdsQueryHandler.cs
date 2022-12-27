using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Queries.GetUsersByIds;

public class GetUsersByIdsQueryHandler
    : IQueryHandler<GetUsersByIdsQuery, List<Core.Entities.User>>
{
    
    private readonly IUserRepository _userRepository;
    
    public GetUsersByIdsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<List<Core.Entities.User>>> Handle(
        GetUsersByIdsQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersByIds(request.ids);

        return users;
    }
}