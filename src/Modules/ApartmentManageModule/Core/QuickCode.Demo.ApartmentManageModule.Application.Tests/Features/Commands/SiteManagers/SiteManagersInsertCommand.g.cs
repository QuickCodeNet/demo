using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ApartmentManageModule.Application.Features;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.ApartmentManageModule.Application.Tests.Features
{
    public class SiteManagersInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ISiteManagersRepository> _repositoryMock;
        private readonly Mock<ILogger<SiteManagersInsertCommand.SiteManagersInsertHandler>> _loggerMock;
        public SiteManagersInsertCommandTests()
        {
            _repositoryMock = new Mock<ISiteManagersRepository>();
            _loggerMock = new Mock<ILogger<SiteManagersInsertCommand.SiteManagersInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SiteManagersDto>("tr");
            var fakeResponse = new RepoResponse<SiteManagersDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SiteManagersDto>())).ReturnsAsync(fakeResponse);
            var handler = new SiteManagersInsertCommand.SiteManagersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new SiteManagersInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<SiteManagersDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<SiteManagersDto>("tr");
            var fakeResponse = new RepoResponse<SiteManagersDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<SiteManagersDto>())).ReturnsAsync(fakeResponse);
            var handler = new SiteManagersInsertCommand.SiteManagersInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new SiteManagersInsertCommand(fakeDto);
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