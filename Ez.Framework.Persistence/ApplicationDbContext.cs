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
}