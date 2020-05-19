using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitmq
{
    public class BusConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string OrderQueue = "validate-order-queue";
    }
}
