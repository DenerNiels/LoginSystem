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

        }
    }
}
