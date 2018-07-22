using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Challenge.InputParser
{
    public enum Tokens
    {
        [EnumMember(Value = "amount")]
        Amount,

        [EnumMember(Value = "interest")]
        Interest,

        [EnumMember(Value = "downpayment")]
        Downpayment,

        [EnumMember(Value = "term")]
        Term
    }
}
