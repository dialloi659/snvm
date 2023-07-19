using DDT_Node_Tool.Contracts.Implementations;
using DDT_Node_Tool.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Tests
{
    public class VersionSelectorTests
    {
        private readonly Mock<IConsoleService> _consoleServiceMock;
        private readonly VersionSelector _versionSelector;

        public VersionSelectorTests()
        {
            _consoleServiceMock = new Mock<IConsoleService>();
            _versionSelector = new VersionSelector(_consoleServiceMock.Object);
        }

        [Fact]
        public void SelectVersion_ShouldReturnCorrectVersion_WhenCalled()
        {
            // Arrange
            string selectedVersionToCheck = "node-v18.12.2-win-x86";
            var versions = new List<string> { "node-v8.2.1-win-x86", selectedVersionToCheck , "node-v10.1.0-win-x64" };
            _consoleServiceMock.Setup(cs => cs.PromptSelection(versions, It.IsAny<string>())).Returns(selectedVersionToCheck);

            // Act
            var selectedVersion = _versionSelector.SelectVersion(versions);

            // Assert
            Assert.Equal(selectedVersionToCheck, selectedVersion);
            _consoleServiceMock.Verify(cs => cs.PromptSelection(versions, "Select available node version below:"), Times.Once);
        }

        [Fact]
        public void SelectVersion_ShouldThrowException_WhenVersionsIsEmpty()
        {
            // Arrange
            var versions = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _versionSelector.SelectVersion(versions));
        }

        [Fact]
        public void SelectVersion_ShouldThrowException_WhenVersionsIsNull()
        {
            // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => _versionSelector.SelectVersion(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void SelectVersion_ShouldReturnFirstVersion_WhenOnlyOneVersionIsProvided()
        {
            // Arrange
            string selectedVersionToCheck = "node-v18.12.2-win-x86";
            var versions = new List<string> { selectedVersionToCheck };
            _consoleServiceMock.Setup(cs => cs.PromptSelection(versions, It.IsAny<string>())).Returns(selectedVersionToCheck);

            // Act
            var selectedVersion = _versionSelector.SelectVersion(versions);

            // Assert
            Assert.Equal(selectedVersionToCheck, selectedVersion);
            _consoleServiceMock.Verify(cs => cs.PromptSelection(versions, "Select available node version below:"), Times.Once);
        }
    }

}
