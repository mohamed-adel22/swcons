using System;

namespace engine_store.Interface;

public interface IEngineRepo
{
    

    public Task<Engine> AddEngine(Engine engine);
    public Task<Engine> UpdateEngine(Engine engine);

    Task<bool> DeleteEngine(int id);

    Task<IEnumerable<Engine>> GetAllEngines(); // Get all Engines
    Task<Engine?> GetEngineById(int id);       // Get a Engine by ID

}


