using Shefaa.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<bool> ExistsAsync(string InvoiceNumber);
        Task<string?> GetLastInvoiceNumberForYearAsync(int year);
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdWithItemsAsync(Guid id);

        Task UpdateAsync(Invoice invoice);
        Task<Invoice?> GetByIdWithDetailsAsync(Guid id);
        Task<List<Invoice>> GetByPatientIdAsync(Guid PatientId);
        Task<List<Invoice>> GetUnpaidInvoicesAsync();
    }
}
