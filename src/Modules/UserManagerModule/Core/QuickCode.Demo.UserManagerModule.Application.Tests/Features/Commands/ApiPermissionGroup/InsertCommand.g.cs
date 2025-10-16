﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.ApiPermissionGroup
{
    public class InsertApiPermissionGroupCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IApiPermissionGroupRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertApiPermissionGroupCommand.InsertApiPermissionGroupHandler>> _loggerMock;
        public InsertApiPermissionGroupCommandTests()
        {
            _repositoryMock = new Mock<IApiPermissionGroupRepository>();
            _loggerMock = new Mock<ILogger<InsertApiPermissionGroupCommand.InsertApiPermissionGroupHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<ApiPermissionGroupDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertApiPermissionGroupCommand.InsertApiPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertApiPermissionGroupCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ApiPermissionGroupDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ApiPermissionGroupDto>("tr");
            var fakeResponse = new RepoResponse<ApiPermissionGroupDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ApiPermissionGroupDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertApiPermissionGroupCommand.InsertApiPermissionGroupHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertApiPermissionGroupCommand(fakeDto);
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