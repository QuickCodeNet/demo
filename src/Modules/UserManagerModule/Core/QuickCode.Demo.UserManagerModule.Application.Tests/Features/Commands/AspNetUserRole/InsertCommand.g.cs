﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.AspNetUserRole
{
    public class InsertAspNetUserRoleCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetUserRoleRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertAspNetUserRoleCommand.InsertAspNetUserRoleHandler>> _loggerMock;
        public InsertAspNetUserRoleCommandTests()
        {
            _repositoryMock = new Mock<IAspNetUserRoleRepository>();
            _loggerMock = new Mock<ILogger<InsertAspNetUserRoleCommand.InsertAspNetUserRoleHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRoleDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserRoleDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserRoleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetUserRoleCommand.InsertAspNetUserRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetUserRoleCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetUserRoleDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRoleDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserRoleDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserRoleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetUserRoleCommand.InsertAspNetUserRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetUserRoleCommand(fakeDto);
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