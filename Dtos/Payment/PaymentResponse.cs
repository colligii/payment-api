using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using payment_api.Enums;

namespace payment_api.Dtos.Payment
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? PaymentType { get; set; }
        public decimal Price { get; set; }
        public Guid IdempotencyKey { get; set; }
    }
}