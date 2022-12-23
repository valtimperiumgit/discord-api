using Discord.Api.Abstracts;
using Discord.Application.Chat.Queries.GetUserChats;
using Discord.Core.Entities;
using Discord.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Discord.Api.Controllers;

[Route("api/user/")]
public class ChatController : ApiController
{
    private readonly ISender _sender;

    public ChatController(ISender sender) 
        : base(sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("chats")]
    public async Task<IActionResult> GetUserChats(
        CancellationToken cancellationToken)
    {
        var chatsQuery = new GetUserChatsQuery(
            HttpContext.User.Claims
                .Where(claim => claim.Properties
                    .FirstOrDefault().Value == JwtRegisteredClaimNames.Sub)
                .Select(claim => claim.Value)
                .FirstOrDefault()
            );
        
        Result<List<Chat>> chatsResult = await Sender.Send(chatsQuery, cancellationToken);
        
        if (chatsResult.IsFailure)
        {
            return HandleFailure(chatsResult);
        }
        
        return Ok(chatsResult.Value);
    }
}