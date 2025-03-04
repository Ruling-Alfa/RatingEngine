using Microsoft.EntityFrameworkCore;

namespace RatingEngine.Configurations
{
    public static class ConfigureMigrations
    {
        public static async Task EnableMigrationTirgger(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            await using (var dbContext = scope.ServiceProvider.GetService<DbContext>())
            {
                await dbContext!.Database.EnsureCreatedAsync();
            }
        }
    }
}
