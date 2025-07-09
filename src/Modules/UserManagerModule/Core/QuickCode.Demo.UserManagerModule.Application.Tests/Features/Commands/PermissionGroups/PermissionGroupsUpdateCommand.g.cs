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
    public class PermissionGroupsUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPermissionGroupsRepository> _repositoryMock;
        private readonly Mock<ILogger<PermissionGroupsUpdateCommand.PermissionGroupsUpdateHandler>> _loggerMock;
        public PermissionGroupsUpdateCommandTests()
        {
            _repositoryMock = new Mock<IPermissionGroupsRepository>();
            _loggerMock = new Mock<ILogger<PermissionGroupsUpdateCommand.PermissionGroupsUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<PermissionGroupsDto>());
            var handler = new PermissionGroupsUpdateCommand.PermissionGroupsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PermissionGroupsUpdateCommand(fakeDto.Id, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PermissionGroupsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PermissionGroupsDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PermissionGroupsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<PermissionGroupsDto>());
            var handler = new PermissionGroupsUpdateCommand.PermissionGroupsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new PermissionGroupsUpdateCommand(fakeDto.Id, fakeDto);
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