using Discord.Core.Shared;
using MediatR;

namespace Discord.Application.Abstractions.Messaging;

public interface IQuery<TResponce> : IRequest<Result<TResponce>>
{
    
}