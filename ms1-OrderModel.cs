using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order_ms.ViewModel
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public string CardNumber { get; set; }
        public string UserId { get; set; }
    }
}
