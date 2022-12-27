using Discord.Api.Abstracts;
using Discord.Api.Dtos.Chat;
using Discord.Application.Chat.Queries.GetUserChats;
using Discord.Application.Message.Queries.GetCountUnreadChatsMessages;
using Discord.Application.User.Queries.GetUsersByIds;
using Discord.Core.Entities;
using Discord.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Discord.Api.Controllers;

[Authorize]
[Route("api/chats/")]
public class ChatController : ApiController
{
    private readonly ISender _sender;

    public ChatController(ISender sender) 
        : base(sender)
    {
        _sender = sender;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetUserChats(
        CancellationToken cancellationToken)
    {
        var userId = GetClaimValueByProperty(JwtRegisteredClaimNames.Sub);
        
        var chatsQuery = new GetUserChatsQuery(userId);

        Result<List<Chat>> chatsResult = await _sender.Send(chatsQuery, cancellationToken);
        
        if (chatsResult.IsFailure)
        {
            return HandleFailure(chatsResult);
        }

        var unreadMessagesQuery = new GetUnreadChatsMessagesQuery(
            chatsResult.Value.Select(chat => chat.Id).ToList(), userId);

        Result<List<Message>> unreadMessagesResult = await _sender.Send(unreadMessagesQuery, cancellationToken);

        var chats = chatsResult.Value
            .Select(chat => new ChatDto(
                chat, 
                unreadMessagesResult.Value.Count(message => message.ChatId == chat.Id)
                ))
            .ToList();

        return Ok(chats);
    }
}