using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.ApiPermissionGroup
{
    public class DeleteItemApiPermissionGroupCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiPermissionGroupRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemApiPermissionGroupCommand.DeleteItemApiPermissionGroupHandler>> _loggerMock;
        public DeleteItemApiPermissionGroupCommandTests()
        {
            _repositoryMock = new Mock<IApiPermissionGroupRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemApiPermissionGroupCommand.DeleteItemApiPermissionGroupHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.PermissionGroupName, fakeDto.ApiMethodDefinitionKey)).ReturnsAsync(new RepoResponse<ApiPermissionGroupDto>());
            var handler = new DeleteItemApiPermissionGroupCommand.DeleteItemApiPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemApiPermissionGroupCommand(fakeDto.PermissionGroupName, fakeDto.ApiMethodDefinitionKey);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.PermissionGroupName, fakeDto.ApiMethodDefinitionKey)).ReturnsAsync(new RepoResponse<ApiPermissionGroupDto>());
            var handler = new DeleteItemApiPermissionGroupCommand.DeleteItemApiPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemApiPermissionGroupCommand(fakeDto.PermissionGroupName, fakeDto.ApiMethodDefinitionKey);
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