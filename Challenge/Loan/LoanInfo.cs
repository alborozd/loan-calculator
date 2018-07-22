using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Loan
{
    public class LoanInfo
    {
        private const int RoundTo = 2;

        [JsonIgnore]
        public decimal MonthlyPayment { get; set; }

        [JsonIgnore]
        public decimal TotalInterest { get; set; }

        [JsonIgnore]
        public decimal TotalPayment { get; set; }

        [JsonProperty("monthly payment")]
        public decimal MonthlyPaymentRounded => Math.Round(MonthlyPayment, RoundTo);

        [JsonProperty("total interest")]
        public decimal TotalInterestRounded => Math.Round(TotalInterest, RoundTo);

        [JsonProperty("total payment")]
        public decimal TotalPaymentRounded => Math.Round(TotalPayment, RoundTo);


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}

