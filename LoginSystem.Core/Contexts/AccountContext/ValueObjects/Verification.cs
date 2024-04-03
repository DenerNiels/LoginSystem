using LoginSystem.Core.Contexts.SharedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.ValueObjects
{
    public class Verification : ValueObject
    {
        public Verification() 
        {
        }

        public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(10);
        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public void Verify(string code) 
        {
            if (IsActive)
            {
                throw new Exception("Este item ja está ativo");
            }
            if (ExpiresAt > DateTime.UtcNow)
            {
                throw new Exception("Este código já expirou");
            }
            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("O código de verificação está inválido");
            }

            ExpiresAt = null;
            VerifiedAt=DateTime.UtcNow;
        }

    }
}
