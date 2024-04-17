using LoginSystem.Core.Contexts.AccountContext.Entities;
using LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;
using static LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Response;


namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository) => _repository = repository;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01. Valida a requisição
            try
            {
                var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch (Exception)
            {
                return new Response("Não foi possível validar sua requisição",500);
            }
            #endregion

            #region 02. Recupera o perfil
            User? user;
            try
            {
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
                if (user is null)
                    return new Response("Perfil não encontrado", 404);
            }
            catch (Exception)
            {
                return new Response("Não foi possível recuperar seu perfil", 500);
            }
            #endregion

            #region 03. Verifica se a senha é válida
            if (!user.Password.Challenge(request.Password))
                return new Response("Usuário ou senha inválidos", 400);
            #endregion

            #region 04. Busca se a conta esta verificada
            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Conta inátiva", 400);
            }
            catch (Exception)
            {
                return new Response("Não foi possível verificar seu perfil", 500);
            }
            #endregion

            #region 05. Retoma os dados
            try
            {
                var data = new ResponseData
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = Array.Empty<string>()
                };
                return new Response(string.Empty, data);
            }
            catch (Exception)
            {
                return new Response("Não foi possivel recuperar os dados do perfil", 500);
            }
            #endregion
        }
    }
}
