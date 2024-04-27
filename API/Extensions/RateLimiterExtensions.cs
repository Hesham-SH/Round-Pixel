using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Extensions;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
    {
        services.AddRateLimiter(options => 
        {
            options.AddFixedWindowLimiter("FixedWindowPolicy", opt => 
            {
                opt.Window = TimeSpan.FromSeconds(5);
                opt.PermitLimit = 5;
                opt.QueueLimit = 10;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });
        return services;
    }
}
