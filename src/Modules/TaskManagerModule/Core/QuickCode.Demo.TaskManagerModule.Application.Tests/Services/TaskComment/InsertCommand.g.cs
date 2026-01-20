using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.TaskManagerModule.Application.Services.TaskComment;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.TaskComment;
using QuickCode.Demo.TaskManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.TaskManagerModule.Application.Tests.Services.TaskComment
{
    public class InsertTaskCommentCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ITaskCommentRepository> _repositoryMock;
        private readonly Mock<ILogger<TaskCommentService>> _loggerMock;
        private readonly TaskCommentService _service;
        public InsertTaskCommentCommandTests()
        {
            _repositoryMock = new Mock<ITaskCommentRepository>();
            _loggerMock = new Mock<ILogger<TaskCommentService>>();
            _service = new TaskCommentService(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_Success_When_Valid_Request()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TaskCommentDto>("tr");
            var fakeResponse = new RepoResponse<TaskCommentDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<TaskCommentDto>())).ReturnsAsync(fakeResponse);
            // Act
            var result = await _service.InsertAsync(fakeDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<TaskCommentDto>()), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TaskCommentDto>("tr");
            var fakeResponse = new RepoResponse<TaskCommentDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<TaskCommentDto>())).ReturnsAsync(fakeResponse);
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