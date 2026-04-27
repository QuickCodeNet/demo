using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.ProductCatalogModule.Api.Controllers;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariantAttribute;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Helpers;
using ProductStatus = QuickCode.Demo.ProductCatalogModule.Domain.Enums.ProductStatus;

namespace QuickCode.Demo.ProductCatalogModule.Api.Tests
{
    public class ProductVariantAttributesControllerTests
    {
        private readonly Mock<IProductVariantAttributeService> _serviceMock = new();
        private readonly Mock<ILogger<ProductVariantAttributesController>> _loggerMock = new();
        private readonly ProductVariantAttributesController _controller;
        public ProductVariantAttributesControllerTests()
        {
            _controller = new ProductVariantAttributesController(_serviceMock.Object, null, _loggerMock.Object);
        }

        [Fact]
        public async Task ListAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeList = TestDataGenerator.CreateFakes<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.ListAsync(It.IsAny<int?>(), It.IsAny<int?>())).ReturnsAsync(new Response<List<ProductVariantAttributeDto>> { Code = 0, Value = fakeList });
            // Act
            var result = await _controller.ListAsync(1, 10);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(fakeList, okResult.Value);
        }

        [Fact]
        public async Task ListAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            _serviceMock.Setup(s => s.ListAsync(It.IsAny<int?>(), It.IsAny<int?>())).ReturnsAsync(new Response<List<ProductVariantAttributeDto>> { Code = 1, Message = "Error" });
            // Act
            var result = await _controller.ListAsync(1, 10);
            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Error", badRequest.Value.ToString());
        }

        [Fact]
        public async Task ListAsync_Should_Return_NotFound_When_Page_Less_Than_1()
        {
            // Act
            var result = await _controller.ListAsync(0, 10);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task TotalCountAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            _serviceMock.Setup(s => s.TotalItemCountAsync()).ReturnsAsync(new Response<int> { Code = 0, Value = 5 });
            // Act
            var result = await _controller.CountAsync();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(5, okResult.Value);
        }

        [Fact]
        public async Task TotalCountAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            _serviceMock.Setup(s => s.TotalItemCountAsync()).ReturnsAsync(new Response<int> { Code = 1, Message = "Error" });
            // Act
            var result = await _controller.CountAsync();
            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Error", badRequest.Value.ToString());
        }

        [Fact]
        public async Task GetItemAsync_Should_Return_Ok_When_Found()
        {
            // Arrange 
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.GetItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<ProductVariantAttributeDto> { Code = 0, Value = fakeDto });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(fakeDto, okResult.Value);
        }

        [Fact]
        public async Task GetItemAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.GetItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<ProductVariantAttributeDto> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetItemAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.GetItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<ProductVariantAttributeDto> { Code = 1, Message = "Error" });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Created_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.InsertAsync(It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(new Response<ProductVariantAttributeDto> { Code = 0, Value = fakeDto });
            // Act
            var result = await _controller.InsertAsync(fakeDto);
            // Assert
            var created = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(fakeDto, created.Value);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.InsertAsync(It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(new Response<ProductVariantAttributeDto> { Code = 1, Message = "Insert failed" });
            // Act
            var result = await _controller.InsertAsync(fakeDto);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(new Response<bool> { Code = 0, Value = true });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.VariantId, fakeDto.AttributeValueId, fakeDto);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(new Response<bool> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.VariantId, fakeDto.AttributeValueId, fakeDto);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ProductVariantAttributeDto>())).ReturnsAsync(new Response<bool> { Code = 1, Message = "Update failed" });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.VariantId, fakeDto.AttributeValueId, fakeDto);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.DeleteItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<bool> { Code = 0, Value = true });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.DeleteItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<bool> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<ProductVariantAttributeDto>("tr");
            _serviceMock.Setup(s => s.DeleteItemAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<bool> { Code = 1, Message = "Delete failed" });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.VariantId, fakeDto.AttributeValueId);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}