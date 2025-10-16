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
    public class InsertPortalPermissionTypeCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPortalPermissionTypeRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertPortalPermissionTypeCommand.InsertPortalPermissionTypeHandler>> _loggerMock;
        public InsertPortalPermissionTypeCommandTests()
        {
            _repositoryMock = new Mock<IPortalPermissionTypeRepository>();
            _loggerMock = new Mock<ILogger<InsertPortalPermissionTypeCommand.InsertPortalPermissionTypeHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionTypeDto>("tr");
            var fakeResponse = new RepoResponse<PortalPermissionTypeDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PortalPermissionTypeDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPortalPermissionTypeCommand.InsertPortalPermissionTypeHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPortalPermissionTypeCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PortalPermissionTypeDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionTypeDto>("tr");
            var fakeResponse = new RepoResponse<PortalPermissionTypeDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PortalPermissionTypeDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPortalPermissionTypeCommand.InsertPortalPermissionTypeHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPortalPermissionTypeCommand(fakeDto);
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