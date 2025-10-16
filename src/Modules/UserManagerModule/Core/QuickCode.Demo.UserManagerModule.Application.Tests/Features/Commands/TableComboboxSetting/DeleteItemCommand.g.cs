﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features.TableComboboxSetting;
using QuickCode.Demo.UserManagerModule.Application.Dtos.TableComboboxSetting;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features.TableComboboxSetting
{
    public class DeleteItemTableComboboxSettingCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ITableComboboxSettingRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteItemTableComboboxSettingCommand.DeleteItemTableComboboxSettingHandler>> _loggerMock;
        public DeleteItemTableComboboxSettingCommandTests()
        {
            _repositoryMock = new Mock<ITableComboboxSettingRepository>();
            _loggerMock = new Mock<ILogger<DeleteItemTableComboboxSettingCommand.DeleteItemTableComboboxSettingHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<TableComboboxSettingDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TableName)).ReturnsAsync(new RepoResponse<TableComboboxSettingDto>());
            var handler = new DeleteItemTableComboboxSettingCommand.DeleteItemTableComboboxSettingHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemTableComboboxSettingCommand(fakeDto.TableName);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<TableComboboxSettingDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found",
                Value = false
            };
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<TableComboboxSettingDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TableName)).ReturnsAsync(new RepoResponse<TableComboboxSettingDto>());
            var handler = new DeleteItemTableComboboxSettingCommand.DeleteItemTableComboboxSettingHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new DeleteItemTableComboboxSettingCommand(fakeDto.TableName);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.False(result.Value);
        }

        public void Dispose()
        {
        // Cleanup handled by xUnit
        }
    }
}