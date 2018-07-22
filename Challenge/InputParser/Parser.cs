using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Challenge.InputParser
{
    public class Parser
    {
        private string LineTokensDelimiter => ":";

        public object ParseLine(string line, out Tokens token)
        {
            line = Regex.Replace(line, @"\s+", "");

            string[] lineTokens = ParseLineTokens(line);
            Tokens property = ParsePropertyToken(lineTokens[0]);

            token = property;

            return ParseValue(property, lineTokens[1]);
        }

        private string[] ParseLineTokens(string line)
        {
            string[] lineTokens = line.Split(LineTokensDelimiter);
            if (lineTokens.Count() != 2)
                throw new Exception("Invalid line format. It has to be like property:value");

            return lineTokens;
        }

        private Tokens ParsePropertyToken(string property)
        {
            Tokens inputToken = Tokens.Amount;            
            if (!property.TryParseToToken(out inputToken))
                throw new Exception("Invalid property: " + property);

            return inputToken;
        }

        private object ParseValue(Tokens token, string value)
        {
            value = Regex.Replace(value, @"%+", "");

            switch (token)
            {
                case Tokens.Term:
                    return ParseVal<int>(value);

                case Tokens.Amount:
                case Tokens.Downpayment:
                case Tokens.Interest:
                    return ParseVal<decimal>(value);

                default:
                    throw new Exception("Unknown token value");

            }            
        }

        private object ParseVal<T>(string val)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return converter.ConvertFrom(val);                
            }
            catch (Exception)
            {
                throw new FormatException($"Can't parse value {val}. Expected type: {typeof(T).FullName}");
            }
        }      
    }
}
