using LoginSystem.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            #endregion

            #region 03. Verificar o usuário

            #endregion

            #region 04. Persistir os dados 

            #endregion

            #region 05. Enviar E-mail de ativação

            #endregion

        }
    }
}
