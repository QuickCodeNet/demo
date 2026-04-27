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
    public class CampaignApplicabilityServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ICampaignApplicabilityRepository> _repositoryMock;
        private readonly Mock<ILogger<CampaignApplicabilityService>> _loggerMock;
        private readonly CampaignApplicabilityService _service;
        public CampaignApplicabilityServiceDeleteTests()
        {
            _repositoryMock = new Mock<ICampaignApplicabilityRepository>();
            _loggerMock = new Mock<ILogger<CampaignApplicabilityService>>();
            _service = new CampaignApplicabilityService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<CampaignApplicabilityDto>("tr");
            var fakeGetResponse = new RepoResponse<CampaignApplicabilityDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<CampaignApplicabilityDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CampaignApplicabilityDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<CampaignApplicabilityDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<CampaignApplicabilityDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.CampaignId, fakeDto.Scope, fakeDto.ScopeId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<CampaignApplicabilityDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}