using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.SmsManagerModule.Application.Features;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.SmsManagerModule.Application.Tests.Features
{
    public class CampaignTypesInsertCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICampaignTypesRepository> _repositoryMock;
        private readonly Mock<ILogger<CampaignTypesInsertCommand.CampaignTypesInsertHandler>> _loggerMock;
        public CampaignTypesInsertCommandTests()
        {
            _repositoryMock = new Mock<ICampaignTypesRepository>();
            _loggerMock = new Mock<ILogger<CampaignTypesInsertCommand.CampaignTypesInsertHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CampaignTypesDto>("tr");
            var fakeResponse = new RepoResponse<CampaignTypesDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CampaignTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new CampaignTypesInsertCommand.CampaignTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new CampaignTypesInsertCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CampaignTypesDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CampaignTypesDto>("tr");
            var fakeResponse = new RepoResponse<CampaignTypesDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CampaignTypesDto>())).ReturnsAsync(fakeResponse);
            var handler = new CampaignTypesInsertCommand.CampaignTypesInsertHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new CampaignTypesInsertCommand(fakeDto);
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