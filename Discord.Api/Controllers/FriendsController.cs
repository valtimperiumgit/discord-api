using Discord.Api.Abstracts;
using Discord.Api.Dtos.User;
using Discord.Application.FriendRequest.Commands;
using Discord.Application.FriendRequest.Commands.AcceptFriendRequest;
using Discord.Application.FriendRequest.Commands.CreateFriendsRequest;
using Discord.Application.FriendRequest.Queries.GetFriendRequest;
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
    
    [HttpPost("requests/create")]
    public async Task<IActionResult> CrateFriendRequest
    (   [FromQuery]
        string receivingId,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);

        var getFriendRequestQuery = new GetFriendRequestQuery(receivingId, userId);
        var getFriendRequestResult = await _sender.Send(getFriendRequestQuery, cancellationToken);
        
        if (getFriendRequestResult.IsSuccess)
        {
            var acceptFriendRequestCommand = new AcceptFriendRequestCommand(userId, receivingId);
            var acceptFriendRequestResult = await _sender.Send(acceptFriendRequestCommand, cancellationToken);

            return Ok();
        }

        var createFriendRequestCommand = new CreateFriendRequestCommand(userId, receivingId);

        var createFriendRequestResult = await _sender.Send(createFriendRequestCommand, cancellationToken);
        
        return createFriendRequestResult.IsFailure ?
            HandleFailure(createFriendRequestResult) :
            Ok(createFriendRequestResult.Value);
    }
    
    [HttpPost("requests/accept")]
    public async Task<IActionResult> AcceptFriendRequest
    (   [FromQuery]
        string requestId,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var acceptFriendRequestCommand = new AcceptFriendRequestCommand(userId, requestId);

        var createFriendRequestResult = await _sender.Send(acceptFriendRequestCommand, cancellationToken);
        
        return createFriendRequestResult.IsFailure ?
            HandleFailure(createFriendRequestResult) :
            Ok();
    }
}