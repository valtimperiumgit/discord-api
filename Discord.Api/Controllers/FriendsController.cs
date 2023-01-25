using Discord.Api.Abstracts;
using Discord.Api.Dtos.User;
using Discord.Api.Requests.Friends;
using Discord.Application.FriendRequest.Commands;
using Discord.Application.FriendRequest.Commands.AcceptFriendRequest;
using Discord.Application.FriendRequest.Commands.CreateFriendsRequest;
using Discord.Application.FriendRequest.Commands.DeleteFriendRequest;
using Discord.Application.FriendRequest.Queries.GetAllUserFriendRequests;
using Discord.Application.FriendRequest.Queries.GetFriendRequest;
using Discord.Application.Hubs;
using Discord.Application.User.Commands.DeleteFriend;
using Discord.Application.User.Queries.GetUserByNameAndTag;
using Discord.Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Discord.Api.Controllers;

[Authorize]
[Route("api/friends/")]
public class FriendsController : ApiController
{
    private readonly ISender _sender;
    private readonly IFriendsHub _friendsHub;
    private readonly IHubContext<FriendsHub> _friendsHubContext;
    public FriendsController(ISender sender, IFriendsHub friendsHub, IHubContext<FriendsHub> friendsHubContext) : base(sender)
    {
        _sender = sender;
        _friendsHub = friendsHub;
        _friendsHubContext = friendsHubContext;
    }
    
    [HttpPost("requests/create")]
    public async Task<IActionResult> CreateFriendRequest
    (   [FromBody]
        CreateFriendRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);

        var getUserByNameAndTagQuery = new GetUserByNameAndTagQuery(request.Name, request.Tag);
        var getUserByNameAndTagResult = await _sender.Send(getUserByNameAndTagQuery);

        if (getUserByNameAndTagResult.IsFailure)
        {
            return HandleFailure(getUserByNameAndTagResult);
        }

        var getFriendRequestQuery = new GetFriendRequestQuery(getUserByNameAndTagResult.Value.Id, userId);
        var getFriendRequestResult = await _sender.Send(getFriendRequestQuery, cancellationToken);
        
        if (getFriendRequestResult.IsSuccess)
        {
            var acceptFriendRequestCommand = new AcceptFriendRequestCommand(userId, getFriendRequestResult.Value.Id);
            var acceptFriendRequestResult = await _sender.Send(acceptFriendRequestCommand, cancellationToken);

            return Ok();
        }

        var createFriendRequestCommand = new CreateFriendRequestCommand(userId, getUserByNameAndTagResult.Value.Id);

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
    
    [HttpGet("test")]
    public async Task<IActionResult> Test
    (
        CancellationToken cancellationToken)
    {
        await _friendsHubContext.Clients.All.SendAsync("AddFriend", "test");
        return Ok();
    }
}