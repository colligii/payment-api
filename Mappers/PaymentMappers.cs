using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using payment_api.Dtos.Payment;
using payment_api.Models;

namespace payment_api.Mappers
{
    public static class PaymentMappers
    {
        public static Payment ToPaymentModel(this CreatePaymentRequestDTO paymentDto)
        {
            return new Payment
            {
                IdempotencyKey = paymentDto.IdempotencyKey,
                Price = paymentDto.Price
            };
        }

        public static PaymentResponse ToPaymentResponse(this Payment paymentModel)
        {
            return new PaymentResponse
            {
                Id = paymentModel.Id,
                IdempotencyKey = paymentModel.IdempotencyKey,
                PaymentType = paymentModel.PaymentType,
                Price = paymentModel.Price,
                Status = paymentModel.Status
            };
        }
    }
}