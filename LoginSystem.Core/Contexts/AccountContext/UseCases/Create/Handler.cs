using LoginSystem.Core.Contexts.AccountContext.Entities;
using LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using LoginSystem.Core.Contexts.AccountContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Response;

namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01. Valida a requisição
            try
            {
                var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch (Exception e ) //ver aplicando oop na vida real
            {
                Console.WriteLine(e);
                throw;
            }

            #endregion

            #region 02. Gerar os objetos

            Email email;
            Password password;
            User user;
            
            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }
            #endregion

            #region 03. Verificar se o usuário existe no banco

            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellationToken);
                if (exists)
                    return new Response("este e-mail já esta em uso", 400);
            }
            catch (Exception)
            {
                return new Response("Falha ao verificar e-mail cadastrado", 500);
            }

            #endregion

            #region 04. Persistir os dados 
            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch (Exception)
            {
                return new Response("Falha ao persistir dados", 500);
            }
            #endregion

            #region 05. Enviar E-mail de ativação
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch (Exception)
            {
                //do nothing
            }
            #endregion

            return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}
