using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using payment_api.Enums;

namespace payment_api.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Created; 
        public PaymentType? PaymentType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public Guid IdempotencyKey { get; set; }
    }
}