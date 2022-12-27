using Discord.Core.Entities;
using Discord.Core.Repositories;
using Discord.Core.ValueObject;
using MongoDB.Driver;
using Discord.Persistence.MongoModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Discord.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<UserMongoModel> _usersCollection;

    public UserRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _usersCollection = _mongoClient
            .GetDatabase("Discord")
            .GetCollection<UserMongoModel>("Users");
    }
    
    public Task<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = (await _usersCollection
            .FindAsync(user => user.Email == email)).FirstOrDefaultAsync();
        
        if (user.Result is null)
        {
            return null;
        }
        return user.Result.ToDomainEntity();
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = (await _usersCollection
            .FindAsync(user => user.Id == id)).FirstOrDefaultAsync();
        
        if (user.Result is null)
        {
            return null;
        }
        
        return user.Result.ToDomainEntity();
    }

    public async Task<User?> GetUserByNameAndTag(string name, string tag)
    {
        var user = (await _usersCollection
            .FindAsync(user => user.Name == name && user.Tag == tag)).FirstOrDefaultAsync();

        return (user.Result is null) ? null : user.Result.ToDomainEntity();
    }

    public async Task CreateUser(string email, string name, string password,
        Birthday birthday, bool isAcceptNewsletters)
    {
        BirthdayMongoModel birthdayModel = new BirthdayMongoModel
            (birthday.Year, birthday.Month, birthday.Day);
        
        UserMongoModel userModel = new UserMongoModel(
            ObjectId.GenerateNewId().ToString(), 
            email, 
            password, 
            name, 
            "2222", 
            null, 
            DateTime.Now,
            birthdayModel,
            isAcceptNewsletters);
        
        await _usersCollection.InsertOneAsync(userModel);
    }

    public async Task<List<User>> GetUsersByIds(List<string> ids)
    {
        List<UserMongoModel> users = (await _usersCollection
            .FindAsync(user => ids.Contains(user.Id))).ToList();
        
        return users.Select(user => user.ToDomainEntity()).ToList();
    }
}