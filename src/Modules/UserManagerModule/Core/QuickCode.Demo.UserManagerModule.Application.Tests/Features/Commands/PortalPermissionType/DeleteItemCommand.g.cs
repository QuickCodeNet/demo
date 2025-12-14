using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.PortalPermissionType
{
    public class DeleteItemPortalPermissionTypeCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPortalPermissionTypeRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemPortalPermissionTypeCommand.DeleteItemPortalPermissionTypeHandler>> _loggerMock;
        public DeleteItemPortalPermissionTypeCommandTests()
        {
            _repositoryMock = new Mock<IPortalPermissionTypeRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemPortalPermissionTypeCommand.DeleteItemPortalPermissionTypeHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionTypeDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<PortalPermissionTypeDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<PortalPermissionTypeDto>());
            var handler = new DeleteItemPortalPermissionTypeCommand.DeleteItemPortalPermissionTypeHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemPortalPermissionTypeCommand(fakeDto.Id);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<PortalPermissionTypeDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionTypeDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<PortalPermissionTypeDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.Id)).ReturnsAsync(new RepoResponse<PortalPermissionTypeDto>());
            var handler = new DeleteItemPortalPermissionTypeCommand.DeleteItemPortalPermissionTypeHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemPortalPermissionTypeCommand(fakeDto.Id);
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