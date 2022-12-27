using Discord.Api.Abstracts;
using Discord.Api.Dtos.User;
using Discord.Application.FriendRequest.Commands;
using Discord.Application.FriendRequest.Commands.CreateFriendsRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Discord.Api.Controllers;

[Authorize]
[Route("api/friends/")]
public class FriendsController : ApiController
{
    private readonly ISender _sender;
    public FriendsController(ISender sender) : base(sender)
    {
        _sender = sender;
    }
    
    [HttpPost("request")]
    public async Task<IActionResult> GetUserByNameAndTag
    (   [FromQuery]
        string receivingId,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var createFriendRequestCommand = new CreateFriendRequestCommand(userId, receivingId);

        var createFriendRequestResult = await _sender.Send(createFriendRequestCommand, cancellationToken);
        
        if (createFriendRequestResult.IsFailure)
        {
            return HandleFailure(createFriendRequestResult);
        }
        
        return Ok(createFriendRequestResult.Value);
    }
}