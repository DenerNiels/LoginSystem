using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.SharedContext.Exntensions
{
    public static class StringExtensions
    {
        public static string Tobase64(this string text)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
    }
}
