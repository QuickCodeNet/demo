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
    public class DeleteItemAspNetUserRoleCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetUserRoleRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemAspNetUserRoleCommand.DeleteItemAspNetUserRoleHandler>> _loggerMock;
        public DeleteItemAspNetUserRoleCommandTests()
        {
            _repositoryMock = new Mock<IAspNetUserRoleRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemAspNetUserRoleCommand.DeleteItemAspNetUserRoleHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRoleDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<AspNetUserRoleDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.RoleId)).ReturnsAsync(new RepoResponse<AspNetUserRoleDto>());
            var handler = new DeleteItemAspNetUserRoleCommand.DeleteItemAspNetUserRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemAspNetUserRoleCommand(fakeDto.UserId, fakeDto.RoleId);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<AspNetUserRoleDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRoleDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<AspNetUserRoleDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.RoleId)).ReturnsAsync(new RepoResponse<AspNetUserRoleDto>());
            var handler = new DeleteItemAspNetUserRoleCommand.DeleteItemAspNetUserRoleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemAspNetUserRoleCommand(fakeDto.UserId, fakeDto.RoleId);
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