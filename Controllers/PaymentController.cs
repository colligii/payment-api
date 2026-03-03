using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using payment_api.Dtos.Payment;
using payment_api.Enums;
using payment_api.Interface;
using payment_api.Mappers;
using payment_api.Models;

namespace payment_api.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepo;
        public PaymentController(IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var paymentList = await _paymentRepo.GetAllAsync();

            return Ok(paymentList.Select(p => p.ToPaymentResponse()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var paymentModel = await _paymentRepo.GetById(id);

            if(paymentModel == null)
            {
                return NotFound();
            }

            return Ok(paymentModel.ToPaymentResponse());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequestDTO paymentDto)
        {
            var paymentModel = paymentDto.ToPaymentModel();
            var paymentReponse = await _paymentRepo.Create(paymentModel);
            
            if(paymentReponse != null)
                return CreatedAtAction(nameof(GetById), new { id = paymentModel.Id }, paymentModel);

            var paymentByIdempotency = await _paymentRepo.GetByIdempotencyKey(paymentDto.IdempotencyKey);

            if(paymentByIdempotency == null)
                return NotFound();
                
            return Ok(paymentByIdempotency.ToPaymentResponse());
        }
        

        [HttpPatch("pay/{id:guid}")]
        public async Task<IActionResult> Pay([FromRoute] Guid id, [FromBody] PayPaymentRequestDto payPaymentDto)
        {
            // You must add validation of payment on this layer
            // also is better use an service to put all logic inside it
            // right now i just gonna make it simple
            var paymentPay = await _paymentRepo.GetPaymentForPay(id);

            if(paymentPay == null)
                return BadRequest("Payment not found or already paid");

            // proccess the payment this is idempotent
            // in this case i gonna mock as already paid with api

            paymentPay.Status = PaymentStatus.Paid;
            paymentPay.PaymentType = (PaymentType)payPaymentDto.paymentType;

            var payment = await _paymentRepo.UpdateAsync(paymentPay);
            return Ok(payment);
        }
    }
}