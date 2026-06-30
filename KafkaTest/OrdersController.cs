using KafkaTestCommon;
using Microsoft.AspNetCore.Mvc;

namespace KafkaTest
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IKafkaProducer _producer;

        public OrdersController(IKafkaProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreated order)
        {
            await _producer.PublishAsync("orders", order.Id.ToString(), order);

            return Ok(order);
        }
    }
}
