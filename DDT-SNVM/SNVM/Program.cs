using DDT_Node_Tool;
using DDT_Node_Tool.Contracts.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

try
{
    Startup.CreateBuilder(args)
        .Services.GetRequiredService<Runner>()
        .Run();
}
catch (Exception ex)
{
    AnsiConsole.MarkupLineInterpolated($"[red]Oups ! \n{ex.Message}[/]");
}
finally
{
    AnsiConsole.MarkupLine("[cyan]Click any key to exit.[/]");
    Console.ReadKey(true);
}

