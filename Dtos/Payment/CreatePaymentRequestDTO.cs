using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace payment_api.Dtos.Payment
{
    public class CreatePaymentRequestDTO
    {
        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Price is out of range")]
        public decimal Price { get; set; }
        [Required]
        public Guid IdempotencyKey { get; set; }
    }
}