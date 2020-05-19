using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_ms.ViewModel;
using rabbitmq;
using rabbitmq_messages;

namespace order_ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderController(
          ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel orderModel)
        {
            var endPoint = await _sendEndpointProvider.
                GetSendEndpoint(new Uri("queue:" + BusConstants.OrderQueue));

            await endPoint.Send<IOrderMessage>(new
            {
                OrderId = orderModel.OrderId,
                ProductName = orderModel.ProductName,
                PaymentCardNumber = orderModel.CardNumber
            });

            return Ok("Success");
        }
    }
}