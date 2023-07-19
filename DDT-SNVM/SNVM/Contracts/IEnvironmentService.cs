using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Contracts;

public interface IEnvironmentService
{
    string? GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
    void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
}

