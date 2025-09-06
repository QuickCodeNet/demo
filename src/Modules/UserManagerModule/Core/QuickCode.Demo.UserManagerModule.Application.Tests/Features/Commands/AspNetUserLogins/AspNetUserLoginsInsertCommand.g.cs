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
    public class AspNetUserLoginsInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetUserLoginsRepository> _repositoryMock;
        private readonly Mock<ILogger<AspNetUserLoginsInsertCommand.AspNetUserLoginsInsertHandler>> _loggerMock;
        public AspNetUserLoginsInsertCommandTests()
        {
            _repositoryMock = new Mock<IAspNetUserLoginsRepository>();
            _loggerMock = new Mock<ILogger<AspNetUserLoginsInsertCommand.AspNetUserLoginsInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserLoginsDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserLoginsDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserLoginsDto>())).ReturnsAsync(fakeResponse);
            var handler = new AspNetUserLoginsInsertCommand.AspNetUserLoginsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetUserLoginsInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<AspNetUserLoginsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserLoginsDto>("tr");
            var fakeResponse = new RepoResponse<AspNetUserLoginsDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<AspNetUserLoginsDto>())).ReturnsAsync(fakeResponse);
            var handler = new AspNetUserLoginsInsertCommand.AspNetUserLoginsInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetUserLoginsInsertCommand(fakeDto);
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