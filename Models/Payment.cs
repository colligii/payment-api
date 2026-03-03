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
        public PaymentStatus status { get; set; } = PaymentStatus.Created; 
        public PaymentType? paymentType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }
        public Guid idempotencyKey { get; set; }
    }
}