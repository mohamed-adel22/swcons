using engine_store.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/engines")]
public class EngineController : ControllerBase
{
    private readonly IEngineRepo _EngineRepo;

    public EngineController(IEngineRepo EngineRepo)
    {
        _EngineRepo = EngineRepo;
    }

    [HttpPost("AddEngine")]
    public async Task<IActionResult> AddEngine([FromBody] Engine engine)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(engine.Name) ||
            string.IsNullOrWhiteSpace(engine.CarModel) ||
            string.IsNullOrWhiteSpace(engine.Code))
        {
            return BadRequest(new { message = "Name, CarModel, and Code are required fields." });
        }

        await _EngineRepo.AddEngine(engine);
        return CreatedAtAction(nameof(GetEngineById), new { id = engine.Id }, engine);
    }

    [HttpPut("UpdateEngine/{id}")]
    public async Task<IActionResult> UpdateEngine(int id, [FromBody] Engine engine)
    {
        if (id != engine.Id)
        {
            return BadRequest(new { message = "Engine ID mismatch." });
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(engine.Name) ||
            string.IsNullOrWhiteSpace(engine.CarModel) ||
            string.IsNullOrWhiteSpace(engine.Code))
        {
            return BadRequest(new { message = "Name, CarModel, and Code are required fields." });
        }

        var existingEngine = await _EngineRepo.GetEngineById(id);
        if (existingEngine == null)
        {
            return NotFound(new { message = "Engine not found with the provided ID." });
        }

        await _EngineRepo.UpdateEngine(engine);
        return Ok(engine);
    }

    [HttpDelete("DeleteEngine/{id}")]
    public async Task<IActionResult> DeleteEngine(int id)
    {
        var result = await _EngineRepo.DeleteEngine(id);

        if (!result)
        {
            return NotFound(new { message = "Engine not found with the provided ID." });
        }

        return Ok(new { message = "Engine deleted successfully." });
    }

    [HttpGet("GetAllEngines")]
    public async Task<IActionResult> GetAllEngines()
    {
        var engines = await _EngineRepo.GetAllEngines();
        return Ok(engines);
    }

    [HttpGet("GetEngineById/{id}")]
    public async Task<IActionResult> GetEngineById(int id)
    {
        var engine = await _EngineRepo.GetEngineById(id);

        if (engine == null)
        {
            return NotFound(new { message = "Engine not found with the provided ID." });
        }

        return Ok(engine);
    }
}
