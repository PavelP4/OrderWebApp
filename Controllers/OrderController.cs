using Microsoft.AspNetCore.Mvc;
using OrderWebApp.Models;
using OrderWebApp.Services;

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpPost]
        public async Task<OrderDto> AddOrder(OrderDto newOrder)
        {
            return await _orderService.AddOrder(newOrder);
        }

        [HttpPut]
        public async Task<OrderDto> UpdateOrder(OrderDto updatedOrder)
        {
            return await _orderService.UpdateOrder(updatedOrder);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrder(id);
        }
    }
}
