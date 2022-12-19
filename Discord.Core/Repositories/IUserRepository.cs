using Discord.Core.Entities;

namespace Discord.Core.Repositories;

public interface IUserRepository
{
    public Task<User> GetAllUsers();
    public Task<User?> GetUserByEmail(string email);
    public Task CreateUser(string email, string name, string password);
}