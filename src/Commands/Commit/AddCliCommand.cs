using Spectre.Console.Cli;

namespace dottr.Commands.Commit;

internal class AddCliCommand : AsyncCommand<AddSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, AddSettings settings)
    {
        return Task.FromResult(0);
    }
}