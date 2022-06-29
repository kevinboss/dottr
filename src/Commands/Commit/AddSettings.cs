using Spectre.Console.Cli;

namespace dottr.Commands.Commit;

public class AddSettings : CommandSettings
{
    [CommandArgument(0, "<FilePath>")]
    public string? FilePath { get; set; }
}