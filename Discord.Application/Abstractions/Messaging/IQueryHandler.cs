using Discord.Core.Shared;
using MediatR;

namespace Discord.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponce>
    : IRequestHandler<TQuery, Result<TResponce>>
    where TQuery : IQuery<TResponce>
{
    
}