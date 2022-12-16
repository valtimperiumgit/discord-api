using Discord.Core.Entities;

namespace Discord.Core.Repositories;

public interface IUserRepository
{
    public Task<User> GetAllUsers();
    public Task<User> GetUserByEmail();
    public Task<User> CreateUser();
}