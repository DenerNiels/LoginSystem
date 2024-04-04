using LoginSystem.Core.Contexts.AccountContext.ValueObjects;
using LoginSystem.Core.Contexts.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string email, string password)
        {
            Email = email;
            Password = new Password(password);
        }

        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;
        public List<Role> Roles { get; set; }

        public void UpdatePassword(string PlainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.OrdinalIgnoreCase))
                throw new Exception("Código de verificação inválido");
            var password = new Password(PlainTextPassword);
            Password = password;
        }
        public void UpdateEmail(Email email)
        {
            Email = email;
        }
        public void ChancePassword(string PlainTextPassword)
        {
            var password = new Password(PlainTextPassword);
            Password = password;
        }


    }
}
