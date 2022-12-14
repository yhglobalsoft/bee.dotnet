using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var redisConnection = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

builder.Services.AddHangfire(
    config =>
    {
        config.UseRedisStorage(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Hangfire")), new RedisStorageOptions()
        {
            Db = 11,
        });
    });
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBeeBasicApm();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.Run();