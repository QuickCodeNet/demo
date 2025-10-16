﻿using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuickCode.Demo.UserManagerModule.Persistence.Repositories;
using QuickCode.Demo.UserManagerModule.Persistence.Contexts;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.Common.Helpers;
using Xunit.Abstractions;

namespace QuickCode.Demo.UserManagerModule.Persistence.Tests.Repositories
{
    public class PortalPermissionGroupRepositoryTests : IDisposable
    {
        private const int ResultCodeSuccess = 0;
        private const int ResultCodeNotFound = 404;
        private DbContextOptions<WriteDbContext> writeOptions = null;
        private DbContextOptions<ReadDbContext> readOptions = null;
        private Mock<ILogger<PortalPermissionGroupRepository>> loggerMock = null;
        private readonly ITestOutputHelper _output;
        public PortalPermissionGroupRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            CreateDatabase();
        }

        private PortalPermissionGroupRepository CreateRepository()
        {
            var writeContext = new WriteDbContext(writeOptions);
            var readContext = new ReadDbContext(readOptions);
            return new PortalPermissionGroupRepository(loggerMock.Object, writeContext, readContext);
        }

        private void CreateDatabase()
        {
            var databaseName = $"TestDb_{Guid.NewGuid()}";
            writeOptions = new DbContextOptionsBuilder<WriteDbContext>().UseInMemoryDatabase(databaseName).Options;
            readOptions = new DbContextOptionsBuilder<ReadDbContext>().UseInMemoryDatabase(databaseName).Options;
            loggerMock = new Mock<ILogger<PortalPermissionGroupRepository>>();
        }

        [Fact]
        public async Task InsertAsync_Should_Insert_Item_Successfully()
        {
            // Arrange
            var repository = CreateRepository();
            var itemDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            // Act
            var result = await repository.InsertAsync(itemDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            itemDto.AssertPropertiesEqual(result.Value);
        }

        [Fact]
        public async Task InsertAsync_Should_Generate_Unique_Ids()
        {
            // Arrange
            var repository = CreateRepository();
            var fakeItem1 = TestDataGenerator.CreateFake<PortalPermissionGroupDto>();
            var fakeItem2 = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            // Act
            var result1 = await repository.InsertAsync(fakeItem1);
            var result2 = await repository.InsertAsync(fakeItem2);
            // Assert
            Assert.Equal(ResultCodeSuccess, result1.Code);
            Assert.Equal(ResultCodeSuccess, result2.Code);
            Assert.NotEqual(result1.Value.PortalPermissionName, result2.Value.PortalPermissionName);
        }

        [Fact]
        public async Task GetByPkAsync_Should_Return_Item_When_Exists()
        {
            // Arrange
            var repository = CreateRepository();
            var itemDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>();
            var insertedItem = await repository.InsertAsync(itemDto);
            Assert.Equal(ResultCodeSuccess, insertedItem.Code);
            // Act
            var result = await repository.GetByPkAsync(insertedItem.Value.PortalPermissionName, insertedItem.Value.PermissionGroupName, insertedItem.Value.PortalPermissionTypeId);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            itemDto.AssertPropertiesEqual(result.Value);
        }

        [Fact]
        public async Task GetByPkAsync_Should_Return_NotFound_When_Item_Not_Exists()
        {
            // Arrange
            var repository = CreateRepository();
            var notExistsItem = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            // Act
            var result = await repository.GetByPkAsync(notExistsItem.PortalPermissionName, notExistsItem.PermissionGroupName, notExistsItem.PortalPermissionTypeId);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.Equal("Not found in PortalPermissionGroup", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Item_Successfully()
        {
            // Arrange
            var repository = CreateRepository();
            var itemDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            var insertResult = await repository.InsertAsync(itemDto);
            Assert.Equal(ResultCodeSuccess, insertResult.Code);
            var insertedItem = insertResult.Value;
            // Update the item using with expression
            var updates = insertedItem.GenerateRandomUpdates(["PortalPermissionName", "PermissionGroupName", "PortalPermissionTypeId"]);
            var updateItemDto = insertedItem.UpdateProperties(updates);
            repository = CreateRepository();
            // Act
            var result = await repository.UpdateAsync(updateItemDto);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            // Verify the update directly from writeContext
            var updatedItem = await repository.GetByPkAsync(insertedItem.PortalPermissionName, insertedItem.PermissionGroupName, insertedItem.PortalPermissionTypeId);
            Assert.Equal(ResultCodeSuccess, updatedItem.Code);
            foreach (var update in updates)
            {
                var property = updatedItem.Value.GetType().GetProperty(update.Key);
                Assert.NotNull(property);
                Assert.Equal(update.Value, property.GetValue(updatedItem.Value));
            }
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Item_Successfully()
        {
            // Arrange
            var repository = CreateRepository();
            var itemDto = TestDataGenerator.CreateFake<PortalPermissionGroupDto>();
            var insertedItem = await repository.InsertAsync(itemDto);
            Assert.Equal(ResultCodeSuccess, insertedItem.Code);
            repository = CreateRepository();
            // Act
            var result = await repository.DeleteAsync(insertedItem.Value);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.True(result.Value);
            // Verify deletion directly from writeContext
            var deletedItem = await repository.GetByPkAsync(insertedItem.Value.PortalPermissionName, insertedItem.Value.PermissionGroupName, insertedItem.Value.PortalPermissionTypeId);
            Assert.Equal(ResultCodeNotFound, deletedItem.Code);
        }

        [Fact]
        public async Task ListAsync_Should_Return_All_PortalPermissionGroup()
        {
            // Arrange
            CreateDatabase();
            var repository = CreateRepository();
            var fakeItems = TestDataGenerator.CreateFakes<PortalPermissionGroupDto>("tr");
            foreach (var fakeItem in fakeItems)
            {
                var insertResponse = await repository.InsertAsync(fakeItem);
                if (insertResponse.Code != ResultCodeSuccess)
                {
                    _output.WriteLine(insertResponse.Message);
                }

                Assert.Equal(ResultCodeSuccess, insertResponse.Code);
            }

            // Act
            var result = await repository.ListAsync();
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(3, result.Value.Count);
        }

        [Fact]
        public async Task ListAsync_With_Pagination_Should_Return_Correct_Page()
        {
            // Arrange
            var repository = CreateRepository();
            var fakeItems = TestDataGenerator.CreateFakes<PortalPermissionGroupDto>("en", 5);
            foreach (var fakeItem in fakeItems)
            {
                var insertResponse = await repository.InsertAsync(fakeItem);
                if (insertResponse.Code != ResultCodeSuccess)
                {
                    _output.WriteLine(insertResponse.Message);
                }

                Assert.Equal(ResultCodeSuccess, insertResponse.Code);
            }

            // Act
            var result = await repository.ListAsync(pageNumber: 1, pageSize: 2);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task ListAsync_With_PageNumber_Less_Than_One_Should_Return_Error()
        {
            // Arrange
            var repository = CreateRepository();
            // Act
            var result = await repository.ListAsync(pageNumber: 0, pageSize: 10);
            // Assert
            Assert.Equal(ResultCodeNotFound, result.Code);
            Assert.Contains("Page Number must be greater than 1", result.Message);
        }

        [Fact]
        public async Task ListAsync_With_Null_Pagination_Should_Return_All_Records()
        {
            // Arrange
            CreateDatabase();
            var repository = CreateRepository();
            var fakeItems = TestDataGenerator.CreateFakes<PortalPermissionGroupDto>("tr");
            foreach (var fakeItem in fakeItems)
            {
                var insertResponse = await repository.InsertAsync(fakeItem);
                if (insertResponse.Code != ResultCodeSuccess)
                {
                    _output.WriteLine(insertResponse.Message);
                }

                Assert.Equal(ResultCodeSuccess, insertResponse.Code);
            }

            // Act
            var result = await repository.ListAsync(pageNumber: null, pageSize: null);
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(3, result.Value.Count);
        }

        [Fact]
        public async Task CountAsync_Should_Return_Correct_Count()
        {
            // Arrange
            CreateDatabase();
            var repository = CreateRepository();
            var fakeItems = TestDataGenerator.CreateFakes<PortalPermissionGroupDto>("tr");
            foreach (var fakeItem in fakeItems)
            {
                var insertResponse = await repository.InsertAsync(fakeItem);
                if (insertResponse.Code != ResultCodeSuccess)
                {
                    _output.WriteLine(insertResponse.Message);
                }

                Assert.Equal(ResultCodeSuccess, insertResponse.Code);
            }

            // Act
            var result = await repository.CountAsync();
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(3, result.Value);
        }

        [Fact]
        public async Task CountAsync_Should_Return_Zero_When_No_Records()
        {
            // Arrange
            var repository = CreateRepository();
            // Act
            var result = await repository.CountAsync();
            // Assert
            Assert.Equal(ResultCodeSuccess, result.Code);
            Assert.Equal(ResultCodeSuccess, result.Value);
        }

        [Fact]
        public async Task UpdateAsync_Should_Handle_NonExistent_Item()
        {
            // Arrange
            var repository = CreateRepository();
            var notExistsItem = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            // Act
            var result = await repository.UpdateAsync(notExistsItem);
            // Assert
            // Repository should return 404 for non-existent entity
            Assert.Equal(ResultCodeNotFound, result.Code);
        }

        [Fact]
        public async Task DeleteAsync_Should_Handle_NonExistent_Item()
        {
            // Arrange
            var repository = CreateRepository();
            var notExistsItem = TestDataGenerator.CreateFake<PortalPermissionGroupDto>("tr");
            // Act
            var result = await repository.DeleteAsync(notExistsItem);
            // Assert
            // Repository should return 404 for non-existent entity
            Assert.Equal(ResultCodeNotFound, result.Code);
        }

        public void Dispose()
        {
        // Cleanup is handled by using statements
        }
    }
}