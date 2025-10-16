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
    public class DeleteItemApiMethodDefinitionCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiMethodDefinitionRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemApiMethodDefinitionCommand.DeleteItemApiMethodDefinitionHandler>> _loggerMock;
        public DeleteItemApiMethodDefinitionCommandTests()
        {
            _repositoryMock = new Mock<IApiMethodDefinitionRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemApiMethodDefinitionCommand.DeleteItemApiMethodDefinitionHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiMethodDefinitionDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiMethodDefinitionDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Key)).ReturnsAsync(new RepoResponse<ApiMethodDefinitionDto>());
            var handler = new DeleteItemApiMethodDefinitionCommand.DeleteItemApiMethodDefinitionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemApiMethodDefinitionCommand(fakeDto.Key);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ApiMethodDefinitionDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiMethodDefinitionDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiMethodDefinitionDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Key)).ReturnsAsync(new RepoResponse<ApiMethodDefinitionDto>());
            var handler = new DeleteItemApiMethodDefinitionCommand.DeleteItemApiMethodDefinitionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemApiMethodDefinitionCommand(fakeDto.Key);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}