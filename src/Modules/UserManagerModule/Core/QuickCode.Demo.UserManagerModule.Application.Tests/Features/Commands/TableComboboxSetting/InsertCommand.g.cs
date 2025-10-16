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
    public class InsertTableComboboxSettingCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ITableComboboxSettingRepository> _repositoryMock;
        private readonly Mock<ILogger<InsertTableComboboxSettingCommand.InsertTableComboboxSettingHandler>> _loggerMock;
        public InsertTableComboboxSettingCommandTests()
        {
            _repositoryMock = new Mock<ITableComboboxSettingRepository>();
            _loggerMock = new Mock<ILogger<InsertTableComboboxSettingCommand.InsertTableComboboxSettingHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingDto>("tr");
            var fakeResponse = new RepoResponse<TableComboboxSettingDto>(fakeDto, "Success");
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<TableComboboxSettingDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertTableComboboxSettingCommand.InsertTableComboboxSettingHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertTableComboboxSettingCommand(fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(fakeDto, result.Value);
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<TableComboboxSettingDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingDto>("tr");
            var fakeResponse = new RepoResponse<TableComboboxSettingDto>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<TableComboboxSettingDto>())).ReturnsAsync(fakeResponse);
            var handler = new InsertTableComboboxSettingCommand.InsertTableComboboxSettingHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new InsertTableComboboxSettingCommand(fakeDto);
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