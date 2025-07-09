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
    public class ApiPermissionGroupsInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiPermissionGroupsRepository> _repositoryMock;
        private readonly Mock<ILogger<ApiPermissionGroupsInsertCommand.ApiPermissionGroupsInsertHandler>> _loggerMock;
        public ApiPermissionGroupsInsertCommandTests()
        {
            _repositoryMock = new Mock<IApiPermissionGroupsRepository>();
            _loggerMock = new Mock<ILogger<ApiPermissionGroupsInsertCommand.ApiPermissionGroupsInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<ApiPermissionGroupsDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiPermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            var handler = new ApiPermissionGroupsInsertCommand.ApiPermissionGroupsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ApiPermissionGroupsInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ApiPermissionGroupsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<ApiPermissionGroupsDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiPermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            var handler = new ApiPermissionGroupsInsertCommand.ApiPermissionGroupsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new ApiPermissionGroupsInsertCommand(fakeDto);
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