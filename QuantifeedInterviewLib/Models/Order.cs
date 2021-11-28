using QuantifeedInterviewLib.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantifeedInterviewLib.Models
{
    abstract public class Order
    {
        public string OrderId { get; set; }
        public OrderType Type { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public decimal NotionalAmount { get; set; }
        public string Destination { get; set; }
        public string ClientId { get; set; }
        public decimal Weight { get; set; }
    }

    public class Basket: Order
    {
        public IEnumerable<Stock> ChildOrders { get; set; }
    }

    public class Stock : Order
    {
    }
}
