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
    public class AspNetUserRolesUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IAspNetUserRolesRepository> _repositoryMock;
        private readonly Mock<ILogger<AspNetUserRolesUpdateCommand.AspNetUserRolesUpdateHandler>> _loggerMock;
        public AspNetUserRolesUpdateCommandTests()
        {
            _repositoryMock = new Mock<IAspNetUserRolesRepository>();
            _loggerMock = new Mock<ILogger<AspNetUserRolesUpdateCommand.AspNetUserRolesUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRolesDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<AspNetUserRolesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.RoleId)).ReturnsAsync(new RepoResponse<AspNetUserRolesDto>());
            var handler = new AspNetUserRolesUpdateCommand.AspNetUserRolesUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetUserRolesUpdateCommand(fakeDto.UserId, fakeDto.RoleId, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<AspNetUserRolesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<AspNetUserRolesDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<AspNetUserRolesDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.RoleId)).ReturnsAsync(new RepoResponse<AspNetUserRolesDto>());
            var handler = new AspNetUserRolesUpdateCommand.AspNetUserRolesUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new AspNetUserRolesUpdateCommand(fakeDto.UserId, fakeDto.RoleId, fakeDto);
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