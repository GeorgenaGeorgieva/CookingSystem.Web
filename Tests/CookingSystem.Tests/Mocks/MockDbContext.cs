namespace CookingSystem.Tests.Mocks
{
    using CookingSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class MockDbContext
    {
        public static CookingSystemDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<CookingSystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new CookingSystemDbContext(options);

            return dbContext;
        }
    }
}
