﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiMethodDefinition;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.ApiMethodDefinition
{
    public class InsertApiMethodDefinitionCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiMethodDefinitionRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertApiMethodDefinitionCommand.InsertApiMethodDefinitionHandler>> _loggerMock;
        public InsertApiMethodDefinitionCommandTests()
        {
            _repositoryMock = new Mock<IApiMethodDefinitionRepository>();
            _loggerMock = new Mock<ILogger<InsertApiMethodDefinitionCommand.InsertApiMethodDefinitionHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiMethodDefinitionDto>("tr");
            var fakeResponse = new RepoResponse<ApiMethodDefinitionDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiMethodDefinitionDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertApiMethodDefinitionCommand.InsertApiMethodDefinitionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertApiMethodDefinitionCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ApiMethodDefinitionDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiMethodDefinitionDto>("tr");
            var fakeResponse = new RepoResponse<ApiMethodDefinitionDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiMethodDefinitionDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertApiMethodDefinitionCommand.InsertApiMethodDefinitionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertApiMethodDefinitionCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.Null(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}