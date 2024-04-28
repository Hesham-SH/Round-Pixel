using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddRateLimiterService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

//Creating Service Of DB Context
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var context = service.GetRequiredService<ApplicationDatabaseContext>();

try
{
    //Applying Pending Migrations From Middleware (When Running API)
    await context.Database.MigrateAsync();
    
    //Seeding Data For Test
}
catch (Exception ex)
{
    var middlewareLogger = service.GetRequiredService<ILogger<Program>>();
    middlewareLogger.LogError(ex, "An Error Occured During Migration !");
}

app.Run();
