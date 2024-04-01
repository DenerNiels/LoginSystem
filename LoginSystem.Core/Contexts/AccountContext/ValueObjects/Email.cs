using LoginSystem.Core.Contexts.SharedContext.Exntensions;
using LoginSystem.Core.Contexts.SharedContext.ValueObjects;
using LoginSystem.Core.Contexts.AccountContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.ValueObjects
{
    public class Email
    {
        public partial class Email : ValueObject
        {
            private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            public Email(string address)
            {
                if (string.IsNullOrEmpty(address))
                    throw new Exception("E-mail inválido");
                Address = address.Trim().ToLower();
                if (address.Length < 10)
                    throw new Exception("E-mail inválido");
                if (!EmailRegex().IsMatch(address))
                    throw new Exception("E-mail inválido");
            }

        }
        public string Address { get; }
        public string Hash => Address.Tobase64();
        public Verification Verification { get; private set; } = new();

        public void ResendVerification()
        {
            Verification = new Verification();
        }

        public static implicit operator string(Email email)
            => email.ToString();

        public static implicit operator Email(string address)
            => new Email(address);

        public override string ToString()
            => Address;

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
