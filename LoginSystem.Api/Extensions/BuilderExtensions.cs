using LoginSystem.Core;
using LoginSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
            Configuration.Secrets.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
            Configuration.Secrets.PasswordSaltKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;

        }
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                Configuration.Database.ConnectionString,
                b => b.MigrationsAssembly("LoginSystem.Api")));
        }
    }
}
