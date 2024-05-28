using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// class to only build database migration when starting app
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>(); // declare object
        dbContext.Database.Migrate(); // invoke method

    }

}
