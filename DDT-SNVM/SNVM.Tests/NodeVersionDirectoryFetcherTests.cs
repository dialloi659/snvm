using DDT_Node_Tool.Contracts;
using DDT_Node_Tool.Contracts.Implementations;
using DDT_Node_Tool.Models;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Tests
{
    public class NodeVersionDirectoryFetcherTests
    {
        private readonly Mock<IFileSystemService> _fileSystemServiceMock;
        private readonly INodeVersionDirectoryFetcher _fetcher;
        private readonly NodeVersionOptions _options;

        private const string PathToVersions = "D:\\path\\to\\versions";
        public NodeVersionDirectoryFetcherTests()
        {
            _options = new NodeVersionOptions
            {
                VersionsDirectory = PathToVersions,
                VersionNamePrefix = "node-v",
                VersionDirectoryNameRegex = "^node-v\\d+\\.\\d+\\.\\d+-win-(x86|x64)$"
            };

            _fileSystemServiceMock = new Mock<IFileSystemService>();

            _fetcher = new NodeVersionDirectoryFetcher(
                Options.Create(_options),
                _fileSystemServiceMock.Object
            );
        }

        [Fact]
        public void GetNodeVersionDirectories_ShouldThrowException_WhenDirectoryDoesNotExist()
        {
            // Arrange
            _fileSystemServiceMock.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(false);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _fetcher.GetNodeVersionDirectories());
        }

        [Fact]
        public void GetNodeVersionDirectories_ShouldReturnEmptyDictionary_WhenNoDirectoriesMatch()
        {
            // Arrange
            _fileSystemServiceMock.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(true);
            _fileSystemServiceMock.Setup(fs => fs.GetDirectories(It.IsAny<string>())).Returns(new[] { $"{PathToVersions}\\invalid-dir" });

            // Act
            var result = _fetcher.GetNodeVersionDirectories();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetNodeVersionDirectories_ShouldReturnCorrectDictionary_WhenDirectoriesMatch()
        {
            // Arrange
            _fileSystemServiceMock.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(true);
            _fileSystemServiceMock.Setup(fs => fs.GetDirectories(It.IsAny<string>())).Returns(new[]
            {
                $"{PathToVersions}\\node-v18.9.0-win-x86",
                $"{PathToVersions}\\node-v16.2.5-win-x64"
            });

            // Act
            var result = _fetcher.GetNodeVersionDirectories();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("node-v18.9.0-win-x86", result.Keys);
            Assert.Contains($"{PathToVersions}\\node-v18.9.0-win-x86", result.Values);
            Assert.Contains("node-v16.2.5-win-x64", result.Keys);
            Assert.Contains($"{PathToVersions}\\node-v16.2.5-win-x64", result.Values);
        }

        [Fact]
        public void GetNodeVersionDirectories_ShouldReturnDictionary_WhenOnlySomeDirectoriesMatch()
        {
            // Arrange
            _fileSystemServiceMock.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(true);
            _fileSystemServiceMock.Setup(fs => fs.GetDirectories(It.IsAny<string>())).Returns(new[]
            {
                $"{PathToVersions}\\node-v18.9.0-win-x86",
                $"{PathToVersions}\\invalid-dir",
                $"{PathToVersions}\\node-v16.2.5-win-x64"
            });

            // Act
            var result = _fetcher.GetNodeVersionDirectories();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("node-v18.9.0-win-x86", result.Keys);
            Assert.Contains($"{PathToVersions}\\node-v18.9.0-win-x86", result.Values);
            Assert.Contains("node-v16.2.5-win-x64", result.Keys);
            Assert.Contains($"{PathToVersions}\\node-v16.2.5-win-x64", result.Values);
        }

        [Fact]
        public void GetNodeVersionDirectories_ShouldHandleEmptyDirectory()
        {
            // Arrange
            _fileSystemServiceMock.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(true);
            _fileSystemServiceMock.Setup(fs => fs.GetDirectories(It.IsAny<string>())).Returns(Array.Empty<string>());

            // Act
            var result = _fetcher.GetNodeVersionDirectories();

            // Assert
            Assert.Empty(result);
        }
    }

}
