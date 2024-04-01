using System;
using LoginSystem.Core.Contexts.SharedContext.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.ValueObjects
{
    public class Password : ValueObject
    {
        private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Special = "!@#$%ˆ&*(){}[];";

        public string Hash {  get;} = string.Empty;
        public string ResetCode {  get;} = Guid.NewGuid().ToString("N")[..8].ToUpper();

        private static string Generate(
            short length = 16,
            bool includeSpecialChars = true,
            bool upperCase = false)
        {
            var chars = includeSpecialChars ? Valid + Special : Valid;
            var startRandom = upperCase ? 26 : 0;
            var index = 0;
            var res = new char[length];
            var rnd = new Random();

            while (index < length)
                res[index++] = chars[rnd.Next(startRandom, chars.Length)];

            return new string(res);
        }

        


    }
}
