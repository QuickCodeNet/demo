using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.PricingEngineModule.Application.Services.PromotionalCampaign;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.PromotionalCampaign;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.PricingEngineModule.Application.Tests.Services.PromotionalCampaign
{
    public class InsertPromotionalCampaignCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IPromotionalCampaignRepository> _repositoryMock;
        private readonly Mock<ILogger<PromotionalCampaignService>> _loggerMock;
        private readonly PromotionalCampaignService _service;
        public InsertPromotionalCampaignCommandTests()
        {
            _repositoryMock = new Mock<IPromotionalCampaignRepository>();
            _loggerMock = new Mock<ILogger<PromotionalCampaignService>>();
            _service = new PromotionalCampaignService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PromotionalCampaignDto>("tr");
            var fakeResponse = new RepoResponse<PromotionalCampaignDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PromotionalCampaignDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<PromotionalCampaignDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PromotionalCampaignDto>("tr");
            var fakeResponse = new RepoResponse<PromotionalCampaignDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<PromotionalCampaignDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
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