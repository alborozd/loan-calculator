using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Loan
{
    public class LoanCalculator
    {
        private decimal _amount;
        private decimal _interest;
        private decimal _downpayment;
        private int _term;

        public LoanCalculator(decimal amount, decimal interest, decimal downpayment, int termYears)
        {
            Amount = amount;
            Interest = interest;
            Downpayment = downpayment;
            TermYears = termYears;

            Validate();
        }

        private void Validate()
        {
            if (Downpayment > Amount)
                throw new Exception("Downpayment cant be more than amount");
        }

        public int TermMonths => TermYears * 12;
        public decimal LoanAmount => Amount - Downpayment;
        public decimal MontlyInterest => Interest / 12;

        public decimal MonthlyPayment
        {
            get
            {
                double monthlyRate = (double)MontlyInterest / 100;
                double monthlyFactor = (monthlyRate + (monthlyRate / (Math.Pow(monthlyRate + 1, TermMonths) - 1)));

                return LoanAmount * (decimal)monthlyFactor;
            }
        }

        public LoanInfo GetLoanInfo()
        {
            decimal monthlyPayment = MonthlyPayment;
            decimal totalPayment = monthlyPayment * TermMonths;
            decimal totalInterest = totalPayment - (LoanAmount);

            return new LoanInfo()
            {
                MonthlyPayment = monthlyPayment,
                TotalInterest = totalInterest,
                TotalPayment = totalPayment
            };
        }

        public decimal Amount
        {
            get { return _amount; }
            private set
            {
                if (value <= 0)
                    throw new Exception("Amount should be more than 0");

                _amount = value;
            }
        }

        public decimal Interest
        {
            get { return _interest; }
            private set
            {
                if (value <= 0 || value > 100)
                    throw new Exception("Interest should be more than 0 and less than 100");

                _interest = value;
            }
        }

        public decimal Downpayment
        {
            get { return _downpayment; }
            private set
            {
                if (value <= 0)
                    throw new Exception("Downpayment should be more than 0");

                _downpayment = value;
            }
        }

        public int TermYears
        {
            get { return _term; }
            private set
            {
                if (value <= 0)
                    throw new Exception("Term should be more than 0");

                _term = value;
            }
        }        
    }
}
