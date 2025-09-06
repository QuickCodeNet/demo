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
    public class PermissionGroupsInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPermissionGroupsRepository> _repositoryMock;
        private readonly Mock<ILogger<PermissionGroupsInsertCommand.PermissionGroupsInsertHandler>> _loggerMock;
        public PermissionGroupsInsertCommandTests()
        {
            _repositoryMock = new Mock<IPermissionGroupsRepository>();
            _loggerMock = new Mock<ILogger<PermissionGroupsInsertCommand.PermissionGroupsInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<PermissionGroupsDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            var handler = new PermissionGroupsInsertCommand.PermissionGroupsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PermissionGroupsInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PermissionGroupsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<PermissionGroupsDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            var handler = new PermissionGroupsInsertCommand.PermissionGroupsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PermissionGroupsInsertCommand(fakeDto);
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