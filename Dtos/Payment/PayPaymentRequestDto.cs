using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using payment_api.Enums;

namespace payment_api.Dtos.Payment
{
    public class PayPaymentRequestDto
    {
        [Required]
        public PaymentType paymentType { get; set; }
    }
}