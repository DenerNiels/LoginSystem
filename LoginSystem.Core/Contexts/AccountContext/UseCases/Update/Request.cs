using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Update
{
    public record Request(
        string Name,
        string Email,
        string Password
        ) : IRequest<Response>;
}
