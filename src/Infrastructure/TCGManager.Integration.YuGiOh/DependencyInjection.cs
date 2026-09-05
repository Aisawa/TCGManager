using Microsoft.Extensions.DependencyInjection;
using Refit;
using TCGManager.Domain.Interfaces;
using TCGManager.Integration.YuGiOh.Clients;
using TCGManager.Integration.YuGiOh.Services;

namespace TCGManager.Integration.YuGiOh;

public static class DependencyInjection
{
    public static IServiceCollection AddYuGiOhIntegration(
        this IServiceCollection services)
    {
        services.AddRefitClient<IYuGiOhClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://db.ygoprodeck.com");
                c.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.Delay = TimeSpan.FromSeconds(2);
            });

        services.AddScoped<IGameSyncService, YuGiOhSyncService>();

        return services;
    }
}