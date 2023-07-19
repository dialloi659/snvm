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

namespace DDT_Node_Tool.Tests;


public class PathEnvironmentVariableServiceTests
{
    private readonly Mock<IEnvironmentService> _environmentServiceMock;
    private readonly IOptions<NodeVersionOptions> _options;
    private readonly IPathEnvironmentVariableService _service;

    //[Set]
    public PathEnvironmentVariableServiceTests()
    {
        _environmentServiceMock = new Mock<IEnvironmentService>();
        _options = Options.Create(new NodeVersionOptions { VersionNamePrefix = "TestPrefix" });

        _service = new PathEnvironmentVariableService(_options, _environmentServiceMock.Object);
    }

    [Fact]
    public void Should_ThrowArgumentNullException_When_NullSelectedNodeVersionProvided()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddToPathEnvironmentVariable(null));
    }

    [Fact]
    public void Should_ThrowArgumentNullException_When_EmptySelectedNodeVersionProvided()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.AddToPathEnvironmentVariable(string.Empty));
    }

    [Fact]
    public void Should_AddSelectedNodeVersion_When_NodeVersionDoesNotExistsInPath()
    {
        // Arrange
        string pathVariable = "Path1;Path2;Path3;";
        string selectedNodeVersion = "Path4";
        string expectedPathVariable = $"{pathVariable}{selectedNodeVersion}";

        _environmentServiceMock.Setup(es => es.GetEnvironmentVariable(It.IsAny<string>(), It.IsAny<EnvironmentVariableTarget>())).Returns(pathVariable);

        // Act
        _service.AddToPathEnvironmentVariable(selectedNodeVersion);

        // Assert
        _environmentServiceMock.Verify(es => es.SetEnvironmentVariable(It.IsAny<string>(), It.Is<string>(s => s == expectedPathVariable), It.IsAny<EnvironmentVariableTarget>()), Times.Once);
    }

    [Fact]
    public void Should_UpdateSelectedNodeVersion_When_NodeVersionExistsInPath()
    {
        // Arrange
        string pathVariable = $"Path1;TestPrefix_1;Path3";
        string selectedNodeVersion = "Path4";
        string expectedPathVariable = $"Path1;{selectedNodeVersion};Path3";

        _environmentServiceMock.Setup(es => es.GetEnvironmentVariable(It.IsAny<string>(), It.IsAny<EnvironmentVariableTarget>())).Returns(pathVariable);

        // Act
        _service.AddToPathEnvironmentVariable(selectedNodeVersion);

        // Assert
        _environmentServiceMock.Verify(es => es.SetEnvironmentVariable(It.IsAny<string>(), It.Is<string>(s => s == expectedPathVariable), It.IsAny<EnvironmentVariableTarget>()), Times.Once);
    }

    [Fact]
    public void Should_NotUpdatePathVariable_When_SameNodeVersionExistsInPath()
    {
        // Arrange
        string existingNodeVersion = "Path4";
        string pathVariable = $"Path1;{existingNodeVersion};Path3";

        _environmentServiceMock.Setup(es => es.GetEnvironmentVariable(It.IsAny<string>(), It.IsAny<EnvironmentVariableTarget>())).Returns(pathVariable);

        // Act
        _service.AddToPathEnvironmentVariable(existingNodeVersion);

        // Assert
        _environmentServiceMock.Verify(es => es.SetEnvironmentVariable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<EnvironmentVariableTarget>()), Times.Never);
    }

    [Fact]
    public void Should_ReturnNull_When_NoPathVariableExists()
    {
        // Arrange
        _environmentServiceMock.Setup(es => es.GetEnvironmentVariable(It.IsAny<string>(), It.IsAny<EnvironmentVariableTarget>())).Returns(value: null);

        // Act
        var result = _service.PathEnvironmentVariable;

        // Assert
        Assert.Null(result);
    }

}
