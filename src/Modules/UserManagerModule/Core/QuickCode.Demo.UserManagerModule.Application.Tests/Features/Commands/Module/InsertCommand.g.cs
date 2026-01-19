using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.Module;
using QuickCode.Demo.UserManagerModule.Application.Dtos.Module;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.Module
{
    public class InsertModuleCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IModuleRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertModuleCommand.InsertModuleHandler>> _loggerMock;
        public InsertModuleCommandTests()
        {
            _repositoryMock = new Mock<IModuleRepository>();
            _loggerMock = new Mock<ILogger<InsertModuleCommand.InsertModuleHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ModuleDto>("tr");
            var fakeResponse = new RepoResponse<ModuleDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ModuleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertModuleCommand.InsertModuleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertModuleCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<ModuleDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ModuleDto>("tr");
            var fakeResponse = new RepoResponse<ModuleDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<ModuleDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertModuleCommand.InsertModuleHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertModuleCommand(fakeDto);
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