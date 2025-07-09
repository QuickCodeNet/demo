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
    public class AspNetRoleClaimsInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetRoleClaimsRepository> _repositoryMock;
        private readonly Mock<ILogger<AspNetRoleClaimsInsertCommand.AspNetRoleClaimsInsertHandler>> _loggerMock;
        public AspNetRoleClaimsInsertCommandTests()
        {
            _repositoryMock = new Mock<IAspNetRoleClaimsRepository>();
            _loggerMock = new Mock<ILogger<AspNetRoleClaimsInsertCommand.AspNetRoleClaimsInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleClaimsDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleClaimsDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleClaimsDto>())).ReturnsAsync(fakeResponse);
            var handler = new AspNetRoleClaimsInsertCommand.AspNetRoleClaimsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetRoleClaimsInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetRoleClaimsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetRoleClaimsDto>("tr");
            var fakeResponse = new RepoResponse<AspNetRoleClaimsDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetRoleClaimsDto>())).ReturnsAsync(fakeResponse);
            var handler = new AspNetRoleClaimsInsertCommand.AspNetRoleClaimsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetRoleClaimsInsertCommand(fakeDto);
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