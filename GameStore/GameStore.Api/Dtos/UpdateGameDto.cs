using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
        // validation using data annotations (for older versions of .net)
        [Required][StringLength(50)] string Name,
        [Required][StringLength(20)] string Genre,
        [Range(1,1000)] decimal Price,
        DateOnly ReleaseDate
);