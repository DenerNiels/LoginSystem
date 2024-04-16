using MediatR;
using System.Runtime.CompilerServices;

namespace LoginSystem.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region Create

            builder.Services.AddTransient<
            LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
                LoginSystem.Infra.Contexts.AccountContext.UseCases.Create.Repository
                >();

            builder.Services.AddTransient<
                LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                LoginSystem.Infra.Contexts.AccountContext.UseCases.Create.Service
                >();

            #endregion 
        }
        public static void MapAccountEndpoints(this WebApplication app) 
        {
            #region Create
            app.MapPost("api/v1/users",async (LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Request request,
                IRequestHandler<
                    LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Request,
                    LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Response> handler)=>
            {
                var result= await handler.Handle(request, new CancellationToken());
                return result.IsSuccess
                    ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                    : Results.Json(result, statusCode: result.Status);
            });
            #endregion
        }
    }
}
