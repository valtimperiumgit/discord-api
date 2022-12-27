using Discord.Core.Entities;
using Discord.Core.ValueObject;

namespace Discord.Core.Repositories;

public interface IUserRepository
{
    public Task<User> GetAllUsers();
    
    //Get User
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserById(string id);
    public Task<List<User>> GetUsersByIds(List<string> ids);
    public Task<User?> GetUserByNameAndTag(string name, string tag);
    
    public Task CreateUser(string email, string name, string password,
        Birthday birthday, bool isAcceptNewsletters);
}