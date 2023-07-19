using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Contracts.Implementations
{
    public class FileSystemService : IFileSystemService
    {
        public bool DirectoryExists(string path) => Directory.Exists(path);

        public string[] GetDirectories(string path) => Directory.GetDirectories(path);
    }
}
