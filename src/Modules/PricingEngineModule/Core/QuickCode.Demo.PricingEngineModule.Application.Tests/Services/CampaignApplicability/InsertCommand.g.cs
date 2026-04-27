using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.PricingEngineModule.Application.Services.CampaignApplicability;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CampaignApplicability;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.PricingEngineModule.Application.Tests.Services.CampaignApplicability
{
    public class InsertCampaignApplicabilityCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICampaignApplicabilityRepository> _repositoryMock;
        private readonly Mock<ILogger<CampaignApplicabilityService>> _loggerMock;
        private readonly CampaignApplicabilityService _service;
        public InsertCampaignApplicabilityCommandTests()
        {
            _repositoryMock = new Mock<ICampaignApplicabilityRepository>();
            _loggerMock = new Mock<ILogger<CampaignApplicabilityService>>();
            _service = new CampaignApplicabilityService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CampaignApplicabilityDto>("tr");
            var fakeResponse = new RepoResponse<CampaignApplicabilityDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CampaignApplicabilityDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<CampaignApplicabilityDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CampaignApplicabilityDto>("tr");
            var fakeResponse = new RepoResponse<CampaignApplicabilityDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<CampaignApplicabilityDto>())).ReturnsAsync(fakeResponse);
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