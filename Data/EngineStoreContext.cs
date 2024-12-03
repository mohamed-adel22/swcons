
using Microsoft.EntityFrameworkCore;

public class EngineStoreContext:DbContext
{
    public EngineStoreContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
    public DbSet<Engine> Engines => Set<Engine>();


}
