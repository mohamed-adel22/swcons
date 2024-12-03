namespace engine_store.Dtos;

public record class EnginesDto(
    int Id,
    string Code,
    string Name,
    string CarModel,
    DateOnly ManufactureDate,
    int Price
    );