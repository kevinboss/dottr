using Microsoft.Extensions.DependencyInjection;
using dottr.Commands.Commit;
using dottr.Config;
using dottr.Infrastructure;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();
registrations.AddSingleton(typeof(Config), _ => ConfigFactory.GetOrCreateConfig());

var registrar = new TypeRegistrar(registrations);

var app = new CommandApp(registrar);

app.Configure(appConfig =>
{
    appConfig.AddCommand<AddCliCommand>("add")
        .WithAlias("a");
});

return app.Run(args);