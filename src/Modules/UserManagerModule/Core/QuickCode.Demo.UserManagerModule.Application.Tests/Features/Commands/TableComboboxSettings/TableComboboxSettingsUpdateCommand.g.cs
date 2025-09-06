using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Application.Tests.Features
{
    public class TableComboboxSettingsUpdateCommandTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private readonly Mock<ITableComboboxSettingsRepository> _repositoryMock;
        private readonly Mock<ILogger<TableComboboxSettingsUpdateCommand.TableComboboxSettingsUpdateHandler>> _loggerMock;
        public TableComboboxSettingsUpdateCommandTests()
        {
            _repositoryMock = new Mock<ITableComboboxSettingsRepository>();
            _loggerMock = new Mock<ILogger<TableComboboxSettingsUpdateCommand.TableComboboxSettingsUpdateHandler>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Valid_Command()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingsDto>("tr");
            var fakeResponse = new RepoResponse<bool>(true, "Success");
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TableComboboxSettingsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TableName)).ReturnsAsync(new RepoResponse<TableComboboxSettingsDto>());
            var handler = new TableComboboxSettingsUpdateCommand.TableComboboxSettingsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new TableComboboxSettingsUpdateCommand(fakeDto.TableName, fakeDto);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TableComboboxSettingsDto>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repository_Returns_404()
        {
            // Arrange
            var fakeDto = TestDataGenerator.CreateFake<TableComboboxSettingsDto>("tr");
            var fakeResponse = new RepoResponse<bool>
            {
                Code = ResultCodeNotFound,
                Message = "Not found"
            };
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TableComboboxSettingsDto>())).ReturnsAsync(fakeResponse);
            _repositoryMock.Setup(r => r.GetByPkAsync(fakeDto.TableName)).ReturnsAsync(new RepoResponse<TableComboboxSettingsDto>());
            var handler = new TableComboboxSettingsUpdateCommand.TableComboboxSettingsUpdateHandler(_loggerMock.Object, _repositoryMock.Object);
            var command = new TableComboboxSettingsUpdateCommand(fakeDto.TableName, fakeDto);
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