using DDT_Node_Tool.Contracts.Implementations;
using DDT_Node_Tool.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool.Tests
{
    public class EnvironmentServiceTests
    {
        private readonly IEnvironmentService _service;
        private readonly string _variable;
        private readonly string _value;

        public EnvironmentServiceTests()
        {
            _service = new EnvironmentService();
            _variable = "TEST_VARIABLE";
            _value = "TEST_VALUE";
        }

        [Fact]
        public void GetEnvironmentVariable_ShouldReturnNull_WhenVariableDoesNotExist()
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, null, EnvironmentVariableTarget.User);

            // Act
            var result = _service.GetEnvironmentVariable(_variable, EnvironmentVariableTarget.User);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetEnvironmentVariable_ShouldReturnValue_WhenVariableExists()
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, _value, EnvironmentVariableTarget.User);

            // Act
            var result = _service.GetEnvironmentVariable(_variable, EnvironmentVariableTarget.User);

            // Assert
            Assert.Equal(_value, result);
        }

        [Fact]
        public void SetEnvironmentVariable_ShouldChangeVariableValue()
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, null, EnvironmentVariableTarget.User);

            // Act
            _service.SetEnvironmentVariable(_variable, _value, EnvironmentVariableTarget.User);

            // Assert
            var result = Environment.GetEnvironmentVariable(_variable, EnvironmentVariableTarget.User);
            Assert.Equal(_value, result);
        }



        [Fact]
        public void SetEnvironmentVariable_ShouldThrowArgumentException_WhenValueIsNull()
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, _value, EnvironmentVariableTarget.User);

            // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => _service.SetEnvironmentVariable(_variable, null, EnvironmentVariableTarget.User));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void SetEnvironmentVariable_ShouldThrowArgumentException_WhenValueIsEmpty()
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, _value, EnvironmentVariableTarget.User);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.SetEnvironmentVariable(_variable, string.Empty, EnvironmentVariableTarget.User));
        }

        [Fact]
        public void SetEnvironmentVariable_ShouldThrowArgumentException_WhenVariableIsNull()
        {
            // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => _service.SetEnvironmentVariable(null, _value, EnvironmentVariableTarget.User));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void SetEnvironmentVariable_ShouldThrowArgumentException_WhenVariableIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.SetEnvironmentVariable(string.Empty, _value, EnvironmentVariableTarget.User));
        }

        [Theory]
        [InlineData(EnvironmentVariableTarget.Process)]
        [InlineData(EnvironmentVariableTarget.User)]
        public void GetEnvironmentVariable_ShouldReturnValue_ForDifferentTargets(EnvironmentVariableTarget target)
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, _value, target);

            // Act
            var result = _service.GetEnvironmentVariable(_variable, target);

            // Assert
            Assert.Equal(_value, result);
        }

        [Theory]
        [InlineData(EnvironmentVariableTarget.Process)]
        [InlineData(EnvironmentVariableTarget.User)]
        public void SetEnvironmentVariable_ShouldChangeVariableValue_ForDifferentTargets(EnvironmentVariableTarget target)
        {
            // Arrange
            Environment.SetEnvironmentVariable(_variable, null, target);

            // Act
            _service.SetEnvironmentVariable(_variable, _value, target);

            // Assert
            var result = Environment.GetEnvironmentVariable(_variable, target);
            Assert.Equal(_value, result);
        }


    }

}
