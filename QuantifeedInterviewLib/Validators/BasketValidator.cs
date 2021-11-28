using FluentValidation;
using QuantifeedInterviewLib.Constants;
using QuantifeedInterviewLib.Helpers;
using QuantifeedInterviewLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace QuantifeedInterviewLib.Validators
{
    public class BasketValidator : AbstractValidator<Basket>
    {
        private readonly string _orderIdRegex = string.Format($@"QF-{DateTime.Today.ToString("yyyyMMdd")}-\d+");

        public BasketValidator()
        {
            // Global Rules
            RuleFor(x => x.OrderId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches(new Regex(_orderIdRegex))
                .Custom((orderId, context) => RunningNumberHelper.IsNextRunningNumber(orderId, context));

            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Custom((cur, context) => ISO4217Helper.Contains(cur, context)).When(x => !string.IsNullOrEmpty(x.Currency), ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.Symbol).NotEmpty();
            RuleFor(x => x.NotionalAmount).NotEmpty();
            RuleFor(x => x.Destination).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();

            RuleFor(x => x.Weight).NotEmpty().Equal(1.0M);

            #region Client Specify Rules
            When(x => x.ClientId == "Client A", () => 
            {
                RuleFor(x => x.Type).Equal(OrderType.Market);
                RuleFor(x => x.Currency).Equal("HKD");
                RuleFor(x => x.Destination).Equal("DestinationA");
            });

            When(x => x.ClientId == "Client B", () =>
            {
                RuleFor(x => x.Type).Equal(OrderType.Limit);
                RuleFor(x => x.Currency).Equal("USD");
                RuleFor(x => x.Destination).Equal("DestinationB");
                RuleFor(x => x.NotionalAmount).GreaterThanOrEqualTo(10000.0M);
            });

            When(x => x.ClientId == "Client C", () =>
            {
                RuleFor(x => x.Currency).Equal("USD");
                RuleFor(x => x.Destination).Equal("DestinationA").When(x => x.Type == OrderType.Market);
                RuleFor(x => x.Destination).Equal("DestinationB").When(x => x.Type == OrderType.Limit);
                RuleFor(x => x.NotionalAmount).LessThanOrEqualTo(100000.0M);
            });
            #endregion

            RuleFor(x => x.ChildOrders)
                .NotEmpty()
                .WithMessage("ChildOrders is null or empty");

            // Child consolidations should be tally with parent
            RuleFor(x => x.ChildOrders.Sum(o => o.Weight))
                .Equal(1.0M).WithMessage("Sum of ChildOrders Weight must be equal to '{ComparisonValue}'.");
            RuleFor(x => x.ChildOrders.Sum(o => o.NotionalAmount))
                .Equal(x => x.NotionalAmount).WithMessage("Sum of ChildOrders NotionalAmount must be equal to '{ComparisonValue}'");

            // Child
            RuleForEach(x => x.ChildOrders).SetValidator(x => new OrderValidator(x));
        }
    }
}
