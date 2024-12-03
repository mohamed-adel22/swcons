namespace engine_store.Dtos;

public record class CreateEngineDto
(
    string Code,
    string Name,
    string CarModel,
    DateOnly ManufactureDate,
    int Price
    );