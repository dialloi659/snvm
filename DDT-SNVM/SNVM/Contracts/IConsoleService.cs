using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Contracts
{
    public interface IConsoleService
    {
        string PromptSelection(IEnumerable<string> choices, string title);
    }


}
