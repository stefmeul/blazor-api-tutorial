namespace GameStore.Api.Entities;

public class Genre
{
    public int Id { get; set; }

    public required string Name { get; set; } // make name required in table (not nullable)

}
