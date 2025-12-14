using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.PortalPermissionGroup
{
    public class UpdatePortalPermissionGroupCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPortalPermissionGroupRepository> _repositoryMock;
        private readonly Mock<ILogger<UpdatePortalPermissionGroupCommand.UpdatePortalPermissionGroupHandler>> _loggerMock;
        public UpdatePortalPermissionGroupCommandTests()
        {
            _repositoryMock = new Mock<IPortalPermissionGroupRepository>();
            _loggerMock = new Mock<ILogger<UpdatePortalPermissionGroupCommand.UpdatePortalPermissionGroupHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PortalPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.PortalPermissionName, fakeDto.PermissionGroupName, fakeDto.PortalPermissionTypeId)).ReturnsAsync(new RepoResponse<PortalPermissionGroupDto>());
            var handler = new UpdatePortalPermissionGroupCommand.UpdatePortalPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdatePortalPermissionGroupCommand(fakeDto.PortalPermissionName, fakeDto.PermissionGroupName, fakeDto.PortalPermissionTypeId, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PortalPermissionGroupDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<PortalPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.PortalPermissionName, fakeDto.PermissionGroupName, fakeDto.PortalPermissionTypeId)).ReturnsAsync(new RepoResponse<PortalPermissionGroupDto>());
            var handler = new UpdatePortalPermissionGroupCommand.UpdatePortalPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new UpdatePortalPermissionGroupCommand(fakeDto.PortalPermissionName, fakeDto.PermissionGroupName, fakeDto.PortalPermissionTypeId, fakeDto);
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