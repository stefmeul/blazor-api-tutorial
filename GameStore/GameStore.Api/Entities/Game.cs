namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; } // make name required in table (not nullable)

    public int GenreId { get; set; }

    public Genre? Genre { get; set; } // ? to define or not define a genre

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
    
}
