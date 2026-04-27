using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using QuickCode.Demo.ProductCatalogModule.Persistence.Repositories;
using QuickCode.Demo.Common.Data;
using Xunit.Abstractions;

namespace QuickCode.Demo.ProductCatalogModule.Persistence.Tests.Repositories
{
    public class AttributeValueRepositoryTests : IDisposable
    {
        private readonly Mock<IDbConnectionFactory> connectionFactoryMock;
        private readonly Mock<ILogger<AttributeValueRepository>> loggerMock;
        private readonly ITestOutputHelper _output;
        public AttributeValueRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            connectionFactoryMock = new Mock<IDbConnectionFactory>();
            loggerMock = new Mock<ILogger<AttributeValueRepository>>();
        }

        private AttributeValueRepository CreateRepository()
        {
            return new AttributeValueRepository(loggerMock.Object, connectionFactoryMock.Object);
        }

        [Fact]
        public void Constructor_Should_Create_Repository_With_Connection_Factory()
        {
            var repository = CreateRepository();
            Assert.NotNull(repository);
        }

        public void Dispose()
        {
            _output.WriteLine("Repository tests use IDbConnectionFactory. Provider-specific integration coverage belongs in relational DB test fixtures.");
        }
    }
}