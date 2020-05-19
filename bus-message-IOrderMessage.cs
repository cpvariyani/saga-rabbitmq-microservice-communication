using System;

namespace rabbitmq_messages
{
    public class IOrderMessage
    {
        public Guid OrderId { get; set; }
        public string PaymentCardNumber { get; set; }
        public string ProductName { get; set; }
    }
}
