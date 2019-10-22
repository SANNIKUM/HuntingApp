using AFFHA.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;

namespace AFFHA.API.FunctionalTest
{
    class TestFixture : IDisposable
    {
        private readonly SqliteConnection connection;

        public AFFHADbContext context { get; }

        public TestFixture()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var options = new DbContextOptionsBuilder<AFFHADbContext>()
                .UseNpgsql(connection)
                .Options;

            context = new AFFHADbContext(options, httpContextAccessor);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}