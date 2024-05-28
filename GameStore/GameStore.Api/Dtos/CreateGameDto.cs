using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(

            // validation using data annotations (for older versions of .net)
        [Required][StringLength(50)] string Name,
        [Required][StringLength(20)] string Genre,
        [Range(1,1000)] decimal Price,
        DateOnly ReleaseDate
        );
                        // add nuget package https://www.nuget.org/packages/MinimalApis.Extensions 
                    // validation using filters (for older versions of .net)
