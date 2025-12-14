using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.OnlineShopModule.Application.Services.UserCoupon;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.UserCoupon;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.OnlineShopModule.Application.Tests.Services.UserCoupon
{
    public class UserCouponServiceDeleteTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<IUserCouponRepository> _repositoryMock;
        private readonly Mock<ILogger<UserCouponService>> _loggerMock;
        private readonly UserCouponService _service;
        public UserCouponServiceDeleteTests()
        {
            _repositoryMock = new Mock<IUserCouponRepository>();
            _loggerMock = new Mock<ILogger<UserCouponService>>();
            _service = new UserCouponService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_Success_When_Item_Exists()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<UserCouponDto>("tr");
            var fakeGetResponse = new RepoResponse<UserCouponDto>(fakeDto, "Success");
            var fakeDeleteResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.CouponId)).ReturnsAsync(fakeGetResponse);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<UserCouponDto>())).ReturnsAsync(fakeDeleteResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.UserId, fakeDto.CouponId);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.CouponId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UserCouponDto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_Should_Return_NotFound_When_Item_Does_Not_Exist()
        {
            var fakeDto = TestDataGenerator.CreateFake<UserCouponDto>("tr");
            // Arrange
            var fakeGetResponse = new RepoResponse<UserCouponDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.CouponId)).ReturnsAsync(fakeGetResponse);
            // Act
            var result = await _service.DeleteItemAsync(fakeDto.UserId, fakeDto.CouponId);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
            _repositoryMock.Verify(r => r.GetByPkAsync(fakeDto.UserId, fakeDto.CouponId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UserCouponDto>()), Times.Never);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}