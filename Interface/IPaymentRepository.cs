using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using payment_api.Models;

namespace payment_api.Interface
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetById(Guid Id);
        Task<Payment?> Create(Payment Data); 
        Task<Payment?> GetByIdempotencyKey(Guid IdempotencyKey);
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetPaymentForPay(Guid id);
        Task<Payment> UpdateAsync(Payment Data);
    }
}