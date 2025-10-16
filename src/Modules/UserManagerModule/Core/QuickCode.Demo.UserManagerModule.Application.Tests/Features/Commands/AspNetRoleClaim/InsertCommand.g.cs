﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetRoleClaim;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRoleClaim;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.AspNetRoleClaim
{
    public class InsertAspNetRoleClaimCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetRoleClaimRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertAspNetRoleClaimCommand.InsertAspNetRoleClaimHandler>> _loggerMock;
        public InsertAspNetRoleClaimCommandTests()
        {
            _repositoryMock = new Mock<IAspNetRoleClaimRepository>();
            _loggerMock = new Mock<ILogger<InsertAspNetRoleClaimCommand.InsertAspNetRoleClaimHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleClaimDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleClaimDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleClaimDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetRoleClaimCommand.InsertAspNetRoleClaimHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetRoleClaimCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetRoleClaimDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleClaimDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleClaimDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleClaimDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertAspNetRoleClaimCommand.InsertAspNetRoleClaimHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertAspNetRoleClaimCommand(fakeDto);
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