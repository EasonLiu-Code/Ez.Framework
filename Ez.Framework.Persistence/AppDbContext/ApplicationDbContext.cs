using Ez.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.AppDbContext;

public sealed class ApplicationDbContext:DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .Property(e => e.IsDeleted)
            .HasDefaultValue(false);
    }
    public DbSet<Article> Articles { get; set; }
}