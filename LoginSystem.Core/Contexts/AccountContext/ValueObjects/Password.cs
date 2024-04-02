using System;
using LoginSystem.Core.Contexts.SharedContext.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        private static string Hashing(
            string password,
            short saltSize = 16,
            short keySize = 32,
            int interations = 10000,
            char splitChar = '.')
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("O passwor não pode ser nulo ou vazio");

            password += Configuration.Secrets.PasswordSaltKey;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                saltSize,
                interations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{interations}{splitChar}{salt}{splitChar}{key}";
        }

        private static bool Verify(
            string hash,
            string password,
            short keySize = 32,
            int interations = 10000,
            char splitChar = '.')
        {
            password += Configuration.Secrets.PasswordSaltKey;
            var parts = hash.Split(splitChar, 3);
            if (parts .Length != 3)
                return false;

            var hashInterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            if (hashInterations != interations)
                return false;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                interations,
                HashAlgorithmName.SHA256);
            var KeyToCheck = algorithm.GetBytes(keySize);
            return KeyToCheck.SequenceEqual(key);
        }
    }
}
