using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.MongoModels;

public class BirthdayMongoModel
{
    public BirthdayMongoModel(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }
    
    [BsonElement("year")]
    public int Year { get; set; }
    [BsonElement("month")]
    public int Month { get; set; }
    [BsonElement("day")]
    public int Day { get; set; }
}