using System.Runtime.CompilerServices;

namespace LoginSystem.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<
                LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
                LoginSystem.Infra.Contexts.AccountContext.UseCases.Create.Repository
                >();

            builder.Services.AddTransient<
                LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                LoginSystem.Infra.Contexts.AccountContext.UseCases.Create.Service
                >();
        }
        public static void MapAccountEndpoints(this WebApplication app) 
        {

        }
    }
}
