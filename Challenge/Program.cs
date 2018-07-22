using Challenge.InputParser;
using Challenge.Loan;
using System;
using System.Collections.Generic;

namespace Challenge
{
    class Program
    {
        static List<Tokens> _expectedTokens = new List<Tokens>()
        {
            Tokens.Amount, Tokens.Downpayment, Tokens.Interest, Tokens.Term
        };

        static Parser parser = new Parser();


        /*
             copy and paste next lines into console : 

                amount: 100000
                interest: 5.5%
                downpayment: 20000
                term: 30

            and press enter twice
        */
        static void Main(string[] args)
        {            
            Dictionary<Tokens, object> currentValues = new Dictionary<Tokens, object>();
            PrintDelimiter();

            while(true)
            {
                try
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        ValidateExpectedTokens(currentValues);

                        var calculator = new LoanCalculator(
                            (decimal)currentValues[Tokens.Amount],
                            (decimal)currentValues[Tokens.Interest],
                            (decimal)currentValues[Tokens.Downpayment],
                            (int)currentValues[Tokens.Term]);

                        Console.WriteLine(calculator.GetLoanInfo());

                        currentValues = new Dictionary<Tokens, object>();
                        PrintDelimiter();

                        continue;                        
                    }

                    ParseAndSaveTo(ref currentValues, line);
                }
                catch (Exception ex)
                {
                    PrintError(ex);                    
                    currentValues = new Dictionary<Tokens, object>();

                    continue;
                }
            }
        }

        static void ParseAndSaveTo(ref Dictionary<Tokens, object> values, string line)
        {
            Tokens prop = Tokens.Amount;
            object value = parser.ParseLine(line, out prop);

            if (values.ContainsKey(prop))
                values[prop] = value;
            else
                values.Add(prop, value);
        }

        static void ValidateExpectedTokens(Dictionary<Tokens, object> actual)
        {
            foreach (Tokens token in _expectedTokens)
            {
                if (!actual.ContainsKey(token))
                    throw new Exception($"Expected property {token.GetEnumMember()} not found");
            }
        }

        static void PrintDelimiter()
        {
            Console.WriteLine(new string('.', 3));
        }

        static void PrintError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.WriteLine("Please, try again");
            Console.ResetColor();
            PrintDelimiter();
        }        
    }
}
