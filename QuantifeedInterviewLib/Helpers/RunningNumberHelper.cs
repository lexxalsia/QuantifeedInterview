using FluentValidation;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace QuantifeedInterviewLib.Helpers
{
    public static class RunningNumberHelper
    {
        public static BigInteger RunningNumber { get; private set; }

        public static void IncreaseRunningNumber()
        {
            RunningNumber += 1;
        }

        public static bool IsNextRunningNumber<T>(BigInteger check, ValidationContext<T> context)
        {
            var retValue = (RunningNumber + 1) == check;
            if (!retValue)
            {
                context.AddFailure($"Index {check} is not in sequence of next Running Number.");
            }

            return retValue;
        }

        public static bool IsNextRunningNumber<T>(string orderId, ValidationContext<T> context)
        {
            var orderIndex = GetOrderIndex(orderId);

            return IsNextRunningNumber(orderIndex, context);
        }

        private static BigInteger GetOrderIndex(string orderId)
        {
            return BigInteger.Parse(orderId.Substring(orderId.LastIndexOf('-') + 1));
        }
    }
}
