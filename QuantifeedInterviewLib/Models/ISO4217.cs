using System;
using System.Collections.Generic;
using System.Text;

namespace QuantifeedInterviewLib.Models
{
    public class ISO4217
    {
        public string AlphabeticCode { get; private set; }
        public int NumericCode { get; private set; }
        public int MinorUnit { get; private set; }

        public ISO4217(string alphabeticCode, int numericCode, int minorUnit)
        {
            AlphabeticCode = alphabeticCode;
            NumericCode = numericCode;
            MinorUnit = minorUnit;
        }
    }
}
