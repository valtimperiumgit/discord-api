using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Commands.CreateFriendsRequest;

public class CreateFriendRequestCommandHandler
   : ICommandHandler<CreateFriendRequestCommand, Core.Entities.FriendRequest>
{
   private readonly IFriendsRepository _friendsRepository;
   private readonly IUserRepository _userRepository;
    
   public CreateFriendRequestCommandHandler(
      IFriendsRepository friendsRepository,
      IUserRepository userRepository)
   {
      _friendsRepository = friendsRepository;
      _userRepository = userRepository;
   }
   
   public async Task<Result<Core.Entities.FriendRequest>> Handle(
      CreateFriendRequestCommand request,
      CancellationToken cancellationToken)
   {
      if (request.requestingId == request.receivingId)
      {
         return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.SameIds);
      }
      
      var friendRequest = await _friendsRepository
         .GetFriendRequest(request.requestingId, request.receivingId);

      if (friendRequest is not null)
      {
         return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.RequestAlreadyExist);
      }

      var receivingUser = await _userRepository.GetUserById(request.receivingId);

      if (receivingUser is null)
      {
         return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.User.UserNotFound);
      }
      
      if (receivingUser.Friends.Contains(request.receivingId))
      {
         return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.UsersAlreadyFriends);
      }

      await _friendsRepository
         .CreateFriendRequest(request.requestingId, request.receivingId);

      var createdFriendRequest = await _friendsRepository
         .GetFriendRequest(request.requestingId, request.receivingId);

      return createdFriendRequest;
   }
}