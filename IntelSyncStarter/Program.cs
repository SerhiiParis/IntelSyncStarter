using IntelSyncStarter;
using IntelSyncStarter.Services;
using IntelSyncStarter.Services.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceProvider = new ServiceCollection()
    .AddLogging(builder => 
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    })
    .AddSingleton<ISyncValidator, SimpleTokenValidator>()
    .AddSingleton<IBatchSyncProcessor, BatchSyncProcessor>()
    .BuildServiceProvider();

await serviceProvider
    .GetRequiredService<IBatchSyncProcessor>()
    .ProcessAllAsync(Consts.Jobs);

Console.ReadKey();
