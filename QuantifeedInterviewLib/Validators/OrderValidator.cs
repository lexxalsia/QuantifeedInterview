using FluentValidation;
using QuantifeedInterviewLib.Constants;
using QuantifeedInterviewLib.Helpers;
using QuantifeedInterviewLib.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace QuantifeedInterviewLib.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        private readonly string _orderIdRegex = string.Format($@"QF-{DateTime.Today.ToString("yyyyMMdd")}-\d+");

        public OrderValidator(Basket basket)
        {
            RuleFor(o => o.ClientId).Equals(basket.ClientId);

            RuleFor(x => x.OrderId)
                .NotEmpty()
                .Matches(new Regex(_orderIdRegex))
                .Custom((orderId, context) => RunningNumberHelper.IsNextRunningNumber(orderId, context));

            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum()
                .Equal(basket.Type).WithMessage("Must be same with Basket Type");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Custom((cur, context) => ISO4217Helper.Contains(cur, context)).When(x => !string.IsNullOrEmpty(x.Currency), ApplyConditionTo.CurrentValidator)
                .Equal(basket.Currency).WithMessage("Must be same with Basket Currency");

            RuleFor(x => x.Symbol)
                .NotEmpty()
                .Equal(basket.Symbol).WithMessage("Must be same with Basket Symbol");

            RuleFor(x => x.Destination)
                .NotEmpty()
                .Equal(basket.Destination).WithMessage("Must be same with Basket Destination");

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .Equal(basket.ClientId).WithMessage("Must be same with Basket ClientId");

            switch (basket.ClientId)
            {
                case "Client A":
                    RuleFor(o => o.Type).Equal(OrderType.Market);
                    RuleFor(o => o.Currency).Equal("HKD");
                    RuleFor(o => o.Destination).Equal("DestinationA");
                    RuleFor(o => o.NotionalAmount).GreaterThanOrEqualTo(100.0M);
                    break;
                case "Client B":
                    RuleFor(o => o.Type).Equal(OrderType.Limit);
                    RuleFor(o => o.Currency).Equal("USD");
                    RuleFor(o => o.Destination).Equal("DestinationB");
                    RuleFor(o => o.NotionalAmount).GreaterThanOrEqualTo(1000.0M);
                    break;
                case "Client C":
                    RuleFor(o => o.Currency).Equal("USD");
                    RuleFor(o => o.Destination).Equal("DestinationA").When(x => x.Type == OrderType.Market);
                    RuleFor(o => o.Destination).Equal("DestinationB").When(x => x.Type == OrderType.Limit);
                    RuleFor(o => o.NotionalAmount).LessThanOrEqualTo(100000.0M);
                    break;
            }
        }
    }
}
