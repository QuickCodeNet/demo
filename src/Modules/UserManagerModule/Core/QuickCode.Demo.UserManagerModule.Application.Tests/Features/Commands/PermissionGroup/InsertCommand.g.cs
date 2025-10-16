using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.PermissionGroup
{
    public class InsertPermissionGroupCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPermissionGroupRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertPermissionGroupCommand.InsertPermissionGroupHandler>> _loggerMock;
        public InsertPermissionGroupCommandTests()
        {
            _repositoryMock = new Mock<IPermissionGroupRepository>();
            _loggerMock = new Mock<ILogger<InsertPermissionGroupCommand.InsertPermissionGroupHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<PermissionGroupDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PermissionGroupDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPermissionGroupCommand.InsertPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPermissionGroupCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PermissionGroupDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<PermissionGroupDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PermissionGroupDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPermissionGroupCommand.InsertPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPermissionGroupCommand(fakeDto);
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