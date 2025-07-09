using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features
{
    public class ApiPermissionGroupsDeleteItemCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiPermissionGroupsRepository> _repositoryMock;
        private readonly Mock<ILogger<ApiPermissionGroupsDeleteItemCommand.ApiPermissionGroupsDeleteItemHandler>> _loggerMock;
        public ApiPermissionGroupsDeleteItemCommandTests()
        {
            _repositoryMock = new Mock<IApiPermissionGroupsRepository>();
            _loggerMock = new Mock<ILogger<ApiPermissionGroupsDeleteItemCommand.ApiPermissionGroupsDeleteItemHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<ApiPermissionGroupsDto>());
            var handler = new ApiPermissionGroupsDeleteItemCommand.ApiPermissionGroupsDeleteItemHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ApiPermissionGroupsDeleteItemCommand(fakeDto.Id);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<ApiPermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<ApiPermissionGroupsDto>());
            var handler = new ApiPermissionGroupsDeleteItemCommand.ApiPermissionGroupsDeleteItemHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ApiPermissionGroupsDeleteItemCommand(fakeDto.Id);
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