using Application.Helpers;
using Application.Interfaces;
using Application.Items;
using Application.Services;
using Domain.Interceptors;
using Microsoft.EntityFrameworkCore;
using Persistence;
using StackExchange.Redis;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Creating Service of Logger
        services.AddLogging();
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<SoftDelete>>();

        //Main Database (SQL Server) Connection
        services.AddDbContext<ApplicationDatabaseContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("MainDatabaseConnection")).
            AddInterceptors(new SoftDelete(logger));
        });

        services.AddCors(opt => 
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:5273");
            });
        });

        //Redis
        services.AddSingleton<ConnectionMultiplexer>(options =>
        {
        var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
        return ConnectionMultiplexer.Connect(config);
        });


        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ListAll.Handler).Assembly));

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICachingService, CachingService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.Configure<BasicCurrency>(configuration.GetSection("BasicCurrency"));
        services.Configure<Discount>(configuration.GetSection("Discount"));
        services.Configure<RedisConfig>(configuration.GetSection("RedisConfig"));

        return services;
    }
}
