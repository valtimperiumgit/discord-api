using MediatR;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                Discord.Infrastructure.AssemblyReference.Assembly,
                Discord.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.AddMediatR(Discord.Application.AssemblyReference.Assembly);

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp 
    => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();