using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Contracts.Implementations
{
    public class ConsoleService : IConsoleService
    {
        public string PromptSelection(IEnumerable<string> choices, string title)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(title)
                    .AddChoices(choices)
            );
        }
    }
}
