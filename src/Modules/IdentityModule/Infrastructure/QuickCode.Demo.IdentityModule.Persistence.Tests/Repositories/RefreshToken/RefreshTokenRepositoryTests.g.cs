using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using QuickCode.Demo.IdentityModule.Persistence.Repositories;
using QuickCode.Demo.Common.Data;
using Xunit.Abstractions;

namespace QuickCode.Demo.IdentityModule.Persistence.Tests.Repositories
{
    public class RefreshTokenRepositoryTests : IDisposable
    {
        private readonly Mock<IDbConnectionFactory> connectionFactoryMock;
        private readonly Mock<ILogger<RefreshTokenRepository>> loggerMock;
        private readonly ITestOutputHelper _output;
        public RefreshTokenRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            connectionFactoryMock = new Mock<IDbConnectionFactory>();
            loggerMock = new Mock<ILogger<RefreshTokenRepository>>();
        }

        private RefreshTokenRepository CreateRepository()
        {
            return new RefreshTokenRepository(loggerMock.Object, connectionFactoryMock.Object);
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