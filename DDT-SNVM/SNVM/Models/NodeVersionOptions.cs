using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Models
{
    public partial class NodeVersionOptions
    {
        [Required]
        public string? VersionsDirectory { get; init; }

        [Required]
        public string? VersionNamePrefix { get; init; }

        [Required]
        public string? VersionDirectoryNameRegex { get; init; }

        public Regex GetVersionDirectoryNameRegex() => new Regex(VersionDirectoryNameRegex!, RegexOptions.Compiled);
    }
}
