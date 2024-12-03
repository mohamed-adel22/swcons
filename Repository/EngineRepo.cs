using engine_store.Interface;
using Microsoft.EntityFrameworkCore;

public class EngineRepo : IEngineRepo
{
    private readonly EngineStoreContext _context;

    public EngineRepo(EngineStoreContext context)
    {
        _context = context;
    }

    public async Task<Engine> AddEngine(Engine engine)
    {
        engine.Id = 0; // Ensures EF Core treats it as a new record
        await _context.Engines.AddAsync(engine);
        _context.SaveChanges();
        return engine;

    }

    public async Task<Engine> UpdateEngine(Engine engine){
        
        // Find the existing Engine by Id
        var existingEngine = await _context.Engines.FindAsync(engine.Id);

        if (existingEngine == null)
        {
            throw new KeyNotFoundException($"Engine with Id {engine.Id} not found.");
        }

        // Update the properties
        existingEngine.Name = engine.Name;
        existingEngine.Price = engine.Price;

        // Save changes to the database
        await _context.SaveChangesAsync();

        return existingEngine;
    }

    public async Task<bool> DeleteEngine(int id){

        var engine = await _context.Engines.FindAsync(id);
    if (engine == null)
    {
        return false; // Engine not found
    }

    // Remove the engine from the database
    _context.Engines.Remove(engine);
    await _context.SaveChangesAsync(); // Commit the deletion

    // Reseed the auto-increment counter to fill the gap
    int? maxId = await _context.Engines.MaxAsync(e => (int?)e.Id);
    int newSeed = maxId.HasValue ? maxId.Value : 0; // Reset to 0 if the table is empty
    await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('Engines', RESEED, {newSeed})");

    return true; // Engine deleted and ID reseeded successfully
    }

    public async Task<IEnumerable<Engine>> GetAllEngines()
    {
        return await _context.Engines.ToListAsync();
    }

    public async Task<Engine?> GetEngineById(int id)
    {
        return await _context.Engines.FindAsync(id);
    }

}