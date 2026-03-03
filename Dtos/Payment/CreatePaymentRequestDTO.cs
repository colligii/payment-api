using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Dtos.Payment
{
    public class CreatePaymentRequestDTO
    {
        public decimal Price { get; set; }
        public Guid IdempotencyKey { get; set; }
    }
}