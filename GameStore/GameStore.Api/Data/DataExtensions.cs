using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// class to only build database migration when starting app
public static class DataExtensions
{
    // make async task
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>(); // declare object
        await dbContext.Database.MigrateAsync(); // invoke method

    }

}
