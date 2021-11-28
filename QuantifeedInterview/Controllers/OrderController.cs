using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantifeedInterviewLib.Models;
using System;
using System.Threading.Tasks;

namespace QuantifeedInterview.Controllers
{
    /*
     Assumptions: 
    1. Except for NotionalAmount, the rest of child orders should be the same as parent order
    2. Order type should be either one (only Market or Limit) for the bucket and orders.
    3. OrderId should be the same across Parent and Children, in fact there should be another unique Id for Children?
    4. There's no other processing needed except model validations
     */
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Basket basket)
        {
            try
            {
                if (!ModelState.IsValid){
                    return BadRequest();
                }

                // Running number usually store in Database. 
                // This is just to simulate the increment of RunningNumber. 
                // Commented out for unit test.
                //RunningNumberHelper.IncreaseRunningNumber();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
