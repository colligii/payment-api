using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using payment_api.Data;
using payment_api.Interface;
using payment_api.Models;

namespace payment_api.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDBContext _context;
        private const int DB_DUPLICATE_STATUS_CODE = 2601;
        private const int DB_UNIQUE_CONSTRAINT_CODE = 2601;
        public PaymentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetById(Guid Id)
        {
            return await _context.Payment.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Payment?> Create(Payment Data)
        {
            try
            {
                await _context.Payment.AddAsync(Data);
                await _context.SaveChangesAsync();
                return Data;    
            } catch(DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == DB_DUPLICATE_STATUS_CODE || sqlEx.Number == DB_UNIQUE_CONSTRAINT_CODE))
            {
                return null;
            }
        }

        public async Task<Payment?> GetByIdempotencyKey(Guid IdempotencyKey)
        {
            return await _context.Payment.FirstOrDefaultAsync(x => x.IdempotencyKey == IdempotencyKey);
        }
    }
}