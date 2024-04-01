using LoginSystem.Core.Contexts.AccountContext.ValueObjects;
using LoginSystem.Core.Contexts.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.Entities
{
    public  class User : Entity
    {
        protected User() { }

        public string Name { get; private set; }
        public Email Email { get; private set; }

        
    }
}
