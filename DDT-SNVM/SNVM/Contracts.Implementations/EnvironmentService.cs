using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Contracts.Implementations;

public class EnvironmentService : IEnvironmentService
{
    public string? GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
    {
        return Environment.GetEnvironmentVariable(variable, target);
    }

    public void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
    {
        ArgumentException.ThrowIfNullOrEmpty(variable);
        ArgumentException.ThrowIfNullOrEmpty(value);

        Environment.SetEnvironmentVariable(variable, value, target);
    }
}


