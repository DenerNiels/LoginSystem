using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public record Request(
        string Email,
        string Password
        ) : IRequest<Response>;
}
