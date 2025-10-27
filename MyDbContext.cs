using Microsoft.EntityFrameworkCore;

namespace demowithsieve;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
        
    }
    
    
    public DbSet<Post> Posts { get; set; }
}