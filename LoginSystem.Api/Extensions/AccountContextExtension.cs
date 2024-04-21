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

            #region Authenticate

            builder.Services.AddTransient<
            LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
                LoginSystem.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository
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

            #region Authenticate

            app.MapPost("api/v1/authenticate", async (LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
                IRequestHandler<
                    LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                    LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());
                if(!result.IsSuccess)
                    return Results.Json(result, statusCode: result.Status);
                if (result.Data is null)
                    return Results.Json(result, statusCode: 500);
                result.Data.Token = LsExtension.Generate(result.Data);
                return Results.Ok(result);
            });
            #endregion
        }
    }
}
