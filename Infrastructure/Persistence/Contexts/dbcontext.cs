using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Transaction>? Transactions { get; set; } = null;
    public DbSet<User> Users { get; set; } = null;
    public DbSet<Category> Category { get; set; } = null;
}