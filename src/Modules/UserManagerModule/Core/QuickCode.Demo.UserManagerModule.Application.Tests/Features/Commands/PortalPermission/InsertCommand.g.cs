﻿using System;
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
    public class InsertPortalPermissionCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPortalPermissionRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertPortalPermissionCommand.InsertPortalPermissionHandler>> _loggerMock;
        public InsertPortalPermissionCommandTests()
        {
            _repositoryMock = new Mock<IPortalPermissionRepository>();
            _loggerMock = new Mock<ILogger<InsertPortalPermissionCommand.InsertPortalPermissionHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            var fakeResponse = new RepoResponse<PortalPermissionDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PortalPermissionDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPortalPermissionCommand.InsertPortalPermissionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPortalPermissionCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PortalPermissionDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            var fakeResponse = new RepoResponse<PortalPermissionDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PortalPermissionDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertPortalPermissionCommand.InsertPortalPermissionHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertPortalPermissionCommand(fakeDto);
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