﻿using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Api.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Helpers;

namespace QuickCode.Demo.UserManagerModule.Api.Tests
{
    public class PortalPermissionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<ILogger<PortalPermissionsController>> _loggerMock = new();
        private readonly PortalPermissionsController _controller;
        public PortalPermissionsControllerTests()
        {
            _controller = new PortalPermissionsController(_mediatorMock.Object, null, _loggerMock.Object);
        }

        [Fact]
        public async Task ListAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeList = TestDataGenerator.CreateFakes<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListPortalPermissionQuery>(), default)).ReturnsAsync(new Response<List<PortalPermissionDto>> { Code = 0, Value = fakeList });
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListPortalPermissionQuery>(), default)).ReturnsAsync(new Response<List<PortalPermissionDto>> { Code = 1, Message = "Error" });
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<TotalCountPortalPermissionQuery>(), default)).ReturnsAsync(new Response<int> { Code = 0, Value = 5 });
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<TotalCountPortalPermissionQuery>(), default)).ReturnsAsync(new Response<int> { Code = 1, Message = "Error" });
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
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetItemPortalPermissionQuery>(), default)).ReturnsAsync(new Response<PortalPermissionDto> { Code = 0, Value = fakeDto });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.Name);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(fakeDto, okResult.Value);
        }

        [Fact]
        public async Task GetItemAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetItemPortalPermissionQuery>(), default)).ReturnsAsync(new Response<PortalPermissionDto> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.Name);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetItemAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetItemPortalPermissionQuery>(), default)).ReturnsAsync(new Response<PortalPermissionDto> { Code = 1, Message = "Error" });
            // Act
            var result = await _controller.GetItemAsync(fakeDto.Name);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Created_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<InsertPortalPermissionCommand>(), default)).ReturnsAsync(new Response<PortalPermissionDto> { Code = 0, Value = fakeDto });
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
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<InsertPortalPermissionCommand>(), default)).ReturnsAsync(new Response<PortalPermissionDto> { Code = 1, Message = "Insert failed" });
            // Act
            var result = await _controller.InsertAsync(fakeDto);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 0, Value = true });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.Name, fakeDto);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.Name, fakeDto);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 1, Message = "Update failed" });
            // Act
            var result = await _controller.UpdateAsync(fakeDto.Name, fakeDto);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_Ok_When_Success()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteItemPortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 0, Value = true });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.Name);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_NotFound_When_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteItemPortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 404, Message = "Not found" });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.Name);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_BadRequest_When_Fail()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<PortalPermissionDto>("tr");
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteItemPortalPermissionCommand>(), default)).ReturnsAsync(new Response<bool> { Code = 1, Message = "Delete failed" });
            // Act
            var result = await _controller.DeleteAsync(fakeDto.Name);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}