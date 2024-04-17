﻿using LoginSystem.Core.Contexts.AccountContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts
{
    public interface IRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
