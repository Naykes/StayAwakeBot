using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StayAwakeBot;

Host.CreateDefaultBuilder()
    .UseWindowsService()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<BotService>();
        services.AddSingleton<IUserInterfaceService, UserInterfaceService>();
    })
    .Build()
    .Run();