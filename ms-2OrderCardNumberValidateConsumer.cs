using MassTransit;
using rabbitmq_messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace card_validate_ms.Consumer
{
    public class OrderCardNumberValidateConsumer :
     IConsumer<IOrderMessage>
    {
        public async Task Consume(ConsumeContext<IOrderMessage> context)
        {
            var data = context.Message;
            if(data.PaymentCardNumber != "test")
            {
                // invalid
            }
        }
    }
}
