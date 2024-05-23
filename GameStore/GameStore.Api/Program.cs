

using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPointName = "GetGame";

List<GameDto> games = [
    new (
        1,
        "Street Dancer",
        "Fun",
        19.99M,                // add M for a decimal type
        new DateOnly(1993, 8, 15)),
    new (
        2,
        "Fantasia",
        "Roleplay",
        9.99M,
        new DateOnly(1985, 5, 25)),
    new (
        3,
        "Pong",
        "Sports",
        29.99M,
        new DateOnly(1973, 2, 7))
];


// GET/games 
//function to map games list to get games from api
app.MapGet("games", () => games);


// GET games/1
app.MapGet("games/{id}", (int id) =>
{
    // boolean ? if found game id or not
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndPointName);


// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
});

// PUT /games   (not safe for concurrent PUT requests in list games)
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});


// DELETE /games/1
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

// display text
//app.MapGet("/", () => "Hello World!");

app.Run();
