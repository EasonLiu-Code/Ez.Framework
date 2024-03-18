using Ez.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public  class ApplicationDbContext:DbContext
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .Property(e => e.IsDeleted)
            .HasDefaultValue(false);
    }
    public DbSet<Article> Article { get; set; }
}