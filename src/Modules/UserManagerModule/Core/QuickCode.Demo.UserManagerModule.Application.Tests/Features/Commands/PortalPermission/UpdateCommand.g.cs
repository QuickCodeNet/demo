using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.PortalPermission
{
    public class UpdatePortalPermissionCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPortalPermissionRepository> _repositoryMock;
        private readonly Mock<ILogger<UpdatePortalPermissionCommand.UpdatePortalPermissionHandler>> _loggerMock;
        public UpdatePortalPermissionCommandTests()
        {
            _repositoryMock = new Mock<IPortalPermissionRepository>();
            _loggerMock = new Mock<ILogger<UpdatePortalPermissionCommand.UpdatePortalPermissionHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PortalPermissionDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(new RepoResponse<PortalPermissionDto>());
            var handler = new UpdatePortalPermissionCommand.UpdatePortalPermissionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdatePortalPermissionCommand(fakeDto.Name, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PortalPermissionDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PortalPermissionDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Name)).ReturnsAsync(new RepoResponse<PortalPermissionDto>());
            var handler = new UpdatePortalPermissionCommand.UpdatePortalPermissionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdatePortalPermissionCommand(fakeDto.Name, fakeDto);
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