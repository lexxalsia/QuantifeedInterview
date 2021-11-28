using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuantifeedInterviewLib.Constants;
using QuantifeedInterviewLib.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuantifeedInterview.Tests
{
    [TestClass]
    public class TestOrderController
    {
        [TestMethod]
        public async Task PostOrder_ValidClientA_ShouldSuccess()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateValidBasketClientA());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task PostOrder_ValidClientB_ShouldSuccess()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateValidBasketClientB());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task PostOrder_ValidClientCMarket_ShouldSuccess()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateValidBasketClientCMarket());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task PostOrder_ValidClientCLimit_ShouldSuccess()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateValidBasketClientCLimit());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task PostOrder_InvalidClientA_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidBasketClientA());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PostOrder_InvalidClientB_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidBasketClientB());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PostOrder_InvalidClientC_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidBasketClientC());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }


        [TestMethod]
        public async Task PostOrder_InvalidOrderId_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidOrderId());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PostOrder_InvalidCurrency_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidCurrency());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PostOrder_InvalidWeight_ShouldFail()
        {
            var sPayload = JsonConvert.SerializeObject(GenerateInvalidWeight());
            var httpContent = new StringContent(sPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var httpResponse = await client.PostAsync("https://localhost:5001/api/Order", httpContent);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        private Basket GenerateValidBasketClientA()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "HKD",
                Symbol = "$",
                NotionalAmount = 200.0M,
                Destination = "DestinationA",
                ClientId = "Client A",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                }
            };
        }
        private Basket GenerateValidBasketClientB()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Limit,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 10000.0M,
                Destination = "DestinationB",
                ClientId = "Client B",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 1000.0M,
                        Destination = "DestinationB",
                        ClientId = "Client B",
                        Weight = 0.1M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 9000.0M,
                        Destination = "DestinationB",
                        ClientId = "Client B",
                        Weight = 0.9M
                    },
                }
            };
        }
        private Basket GenerateValidBasketClientCMarket()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 100000.0M,
                Destination = "DestinationA",
                ClientId = "Client C",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 50000.0M,
                        Destination = "DestinationA",
                        ClientId = "Client C",
                        Weight = 0.5M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 50000.0M,
                        Destination = "DestinationA",
                        ClientId = "Client C",
                        Weight = 0.5M
                    },
                }
            };
        }
        private Basket GenerateValidBasketClientCLimit()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Limit,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 100000.0M,
                Destination = "DestinationB",
                ClientId = "Client C",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 50000.0M,
                        Destination = "DestinationB",
                        ClientId = "Client C",
                        Weight = 0.5M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "USD",
                        Symbol = "$",
                        NotionalAmount = 50000.0M,
                        Destination = "DestinationB",
                        ClientId = "Client C",
                        Weight = 0.5M
                    },
                }
            };
        }

        private Basket GenerateInvalidBasketClientA()
        {
            return new Basket
            {
                OrderId = $"F-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Limit,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 1.0M,
                Destination = "DestinationB",
                ClientId = "Client A",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$$",
                        NotionalAmount = 10.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    }
                }
            };
        }
        private Basket GenerateInvalidBasketClientB()
        {
            return new Basket
            {
                OrderId = $"F-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "USD",
                Symbol = "S$",
                NotionalAmount = 1.0M,
                Destination = "DestinationB",
                ClientId = "Client B",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "HKD",
                        Symbol = "$$",
                        NotionalAmount = 10.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.99M
                    }
                }
            };
        }
        private Basket GenerateInvalidBasketClientC()
        {
            return new Basket
            {
                OrderId = $"F-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 1.0M,
                Destination = "DestinationB",
                ClientId = "Client C",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Limit,
                        Currency = "HKD",
                        Symbol = "$$",
                        NotionalAmount = 10.0M,
                        Destination = "DestinationA",
                        ClientId = "Client C",
                        Weight = 1.0M
                    }
                }
            };
        }

        private Basket GenerateInvalidOrderId()
        {
            return new Basket
            {
                OrderId = $"{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "HKD",
                Symbol = "$",
                NotionalAmount = 200.0M,
                Destination = "DestinationA",
                ClientId = "Client A",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:ddMMyyyy}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-2",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                }
            };
        }
        private Basket GenerateInvalidCurrency()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "USD",
                Symbol = "$",
                NotionalAmount = 200.0M,
                Destination = "DestinationA",
                ClientId = "Client A",
                Weight = 1.0M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "KD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.5M
                    },
                }
            };
        }
        private Basket GenerateInvalidWeight()
        {
            return new Basket
            {
                OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                Type = OrderType.Market,
                Currency = "HKD",
                Symbol = "$",
                NotionalAmount = 200.0M,
                Destination = "DestinationA",
                ClientId = "Client A",
                Weight = 0.99M,
                ChildOrders = new List<Stock>
                {
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.3M
                    },
                    new Stock
                    {
                        OrderId = $"QF-{DateTime.Today:yyyyMMdd}-1",
                        Type = OrderType.Market,
                        Currency = "HKD",
                        Symbol = "$",
                        NotionalAmount = 100.0M,
                        Destination = "DestinationA",
                        ClientId = "Client A",
                        Weight = 0.4M
                    },
                }
            };
        }
    }
}