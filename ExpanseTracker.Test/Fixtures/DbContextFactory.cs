using Infrastructure.Persistence.contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpanseTracker.Fixtures;

public static class DbContextFactory
{
    public static ApplicationDbContext CreateInMemoryOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("ExpanseTracker")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}