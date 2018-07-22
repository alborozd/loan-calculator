using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Challenge.InputParser
{
    public static class Extensions
    {
        public static bool TryParseToToken(this string str, out Tokens token)
        {
            token = Tokens.Amount;

            foreach(string enumName in Enum.GetNames(typeof(Tokens)))
            {
                var member = ((EnumMemberAttribute[])typeof(Tokens)
                    .GetField(enumName)
                    .GetCustomAttributes(typeof(EnumMemberAttribute), true))
                    .First();

                if (member.Value.Equals(str, StringComparison.InvariantCultureIgnoreCase))
                {
                    token = (Tokens)Enum.Parse(typeof(Tokens), enumName);
                    return true;
                }                    
            }

            return false;
        }

        public static string GetEnumMember(this Enum val)
        {
            return ((EnumMemberAttribute[])val.GetType()
                    .GetField(val.ToString())
                    .GetCustomAttributes(typeof(EnumMemberAttribute), true))
                    .FirstOrDefault()?.Value ?? "";
        }
    }
}
