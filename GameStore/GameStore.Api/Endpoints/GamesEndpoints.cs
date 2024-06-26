﻿using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // map groupbuilder for "games"
        RouteGroupBuilder group = app.MapGroup("games")
                                     .WithParameterValidation(); // run validation from nuget package MinimalApi

        // GET/games 
        //function to map games list to get games from api
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                     .Include(game => game.Genre) // include genre in selection
                     .Select(game => game.ToGameSummaryDto())
                     .AsNoTracking() // optimize query
                     .ToListAsync()); // make asynchronous task


        // GET games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => // add dbContext to lambda function
        {
            Game? game = await dbContext.Games.FindAsync(id); // search to find game id in dbContext // asynchronously

            // boolean ? if found game id or not
            //  GameDto? game = games.Find(game => game.Id == id);

            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndPointName);


        // POST /games  ---> inject dependencies dbContext
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            //  game.Genre = dbContext.Genres.Find(newGame.GenreId); // find genre id in creategame dto

            dbContext.Games.Add(game); // add game
            await dbContext.SaveChangesAsync(); // save changes to database

            return Results.CreatedAtRoute(
                GetGameEndPointName,
                new { id = game.Id },
                game.ToGameDetailsDto()); // turn game in gameDto through ToGameDetailsDto method to return from server
        });

        // PUT /games   
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id); // look for game id in database

            if (existingGame is null) // if not found
            {
                return Results.NotFound();
            }

            // update game
            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id));

            // save to db
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games // batch delete on id
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }

}
