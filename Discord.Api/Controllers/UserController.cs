using Discord.Api.Abstracts;
using Discord.Api.Dtos.User;
using Discord.Application.User.Queries.GetCurrentUsers;
using Discord.Application.User.Queries.GetUserById;
using Discord.Application.User.Queries.GetUserByNameAndTag;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Discord.Api.Controllers;

[Authorize]
[Route("api/user/")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    public UserController(ISender sender) 
        : base(sender)
    {
        _sender = sender;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetUser
    (
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var getUserByIdQuery = new GetUserByIdQuery(userId);

        var getUserByIdQueryResult = await _sender.Send(getUserByIdQuery, cancellationToken);
        
        if (getUserByIdQueryResult.IsFailure)
        {
            return HandleFailure(getUserByIdQueryResult);
        }
        
        return Ok(new UserDto(getUserByIdQueryResult.Value));
    }
    
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUsers
    (
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var getCurrentUsers = new GetCurrentUsersQuery(userId);

        var getCurrentUsersResult = await _sender.Send(getCurrentUsers, cancellationToken);
        
        if (getCurrentUsersResult.IsFailure)
        {
            return HandleFailure(getCurrentUsersResult);
        }
        
        return Ok(getCurrentUsersResult.Value.Select(user => new UserDto(user)));
    }
    
    [HttpGet("find")]
    public async Task<IActionResult> GetUserByNameAndTag
    (   [FromQuery] 
        string name,
        string tag,
        CancellationToken cancellationToken)
    {
        var getUserByNameAndTagQuery = new GetUserByNameAndTagQuery(name, tag);

        var getUserByNameAndTagResult = await _sender.Send(getUserByNameAndTagQuery, cancellationToken);
        
        if (getUserByNameAndTagResult.IsFailure)
        {
            return HandleFailure(getUserByNameAndTagResult);
        }
        
        return Ok(new UserDto(getUserByNameAndTagResult.Value));
    }
}