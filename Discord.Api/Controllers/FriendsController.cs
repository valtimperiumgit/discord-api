using Discord.Api.Abstracts;
using Discord.Api.Dtos.User;
using Discord.Application.FriendRequest.Commands;
using Discord.Application.FriendRequest.Commands.AcceptFriendRequest;
using Discord.Application.FriendRequest.Commands.CreateFriendsRequest;
using Discord.Application.FriendRequest.Commands.DeleteFriendRequest;
using Discord.Application.FriendRequest.Queries.GetAllUserFriendRequests;
using Discord.Application.FriendRequest.Queries.GetFriendRequest;
using Discord.Application.User.Commands.DeleteFriend;
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
    
    [HttpGet("requests")]
    public async Task<IActionResult> GetFriendRequests
    (
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var getAllUserFriendRequestsQuery = new GetAllUserFriendRequestsQuery(userId);

        var getAllUserFriendRequestsResult = await _sender.Send(getAllUserFriendRequestsQuery, cancellationToken);
        
        return getAllUserFriendRequestsResult.IsFailure ?
            HandleFailure(getAllUserFriendRequestsResult) :
            Ok(getAllUserFriendRequestsResult.Value);
    }
    
    [HttpDelete("requests/delete")]
    public async Task<IActionResult> DeleteFriendRequests
    (
        [FromQuery] string requestId,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var deleteFriendRequestCommand = new DeleteFriendRequestCommand(userId, requestId);

        var deleteFriendRequestResult = await _sender.Send(deleteFriendRequestCommand, cancellationToken);
        
        return deleteFriendRequestResult.IsFailure ?
            HandleFailure(deleteFriendRequestResult) :
            Ok();
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFriend
    (
        [FromQuery] string friendId,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var deleteFriendCommand = new DeleteFriendCommand(userId, friendId);

        var deleteFriendResult = await _sender.Send(deleteFriendCommand, cancellationToken);
        
        return deleteFriendResult.IsFailure ?
            HandleFailure(deleteFriendResult) :
            Ok();
    }
}