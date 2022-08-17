using System.Diagnostics;
using Bee.Apm.Hangfire;
using Bee.Hangfire.Diagnostics;
using Bee.Hangfire.Filter;
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

var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Hangfire"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddHangfireServer();
builder.Services.AddBeeApmHangfire();
builder.Services.AddHangfire(
    config =>
    {
        config.UseRedisStorage(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Hangfire")), new RedisStorageOptions()
        {
            Db = 13,
        });

        //config.UseFilter(new BeeHangfireDiagnosticClientFilter(provider));
        config.UseFilter(new BeeHangfireDiagnosticServerFilter());
    });



var app = builder.Build();
app.UseBeeBasicApm();
app.UseBeeRedisApm();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.Run();