using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Invoices;
using Shefaa.Domain.Invoices.enums;
using Shefaa.Infrastructure.Common.Persistence;

namespace Shefaa.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ShefaaDbContext _dbContext;

    public InvoiceRepository(ShefaaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(string invoiceNumber)
    {
        return await _dbContext.Invoices
            .AnyAsync(i => i.InvoiceNumber == invoiceNumber);
    }

    public async Task<string?> GetLastInvoiceNumberForYearAsync(int year)
    {
        var prefix = $"INV-{year}-";

        return await _dbContext.Invoices
            .Where(i => i.InvoiceNumber.StartsWith(prefix))
            .OrderByDescending(i => i.InvoiceNumber)
            .Select(i => i.InvoiceNumber)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Invoice?> GetByIdWithItemsAsync(Guid id)
    {
        return await _dbContext.Invoices
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Invoice?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _dbContext.Invoices
            .Include(i => i.Patient)
            .Include(i => i.InvoiceItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
    }

    public async Task<List<Invoice>> GetByPatientIdAsync(Guid patientId)
    {
        return await _dbContext.Invoices
            .Include(i => i.Patient)
            .Include(i => i.InvoiceItems)
            .Where(i => i.PatientId == patientId && !i.IsDeleted)
            .OrderByDescending(i => i.InvoiceDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetUnpaidInvoicesAsync()
    {
        return await _dbContext.Invoices
            .Include(i => i.Patient)
            .Where(i => (i.Status == InvoiceStatus.Unpaid || i.Status == InvoiceStatus.PartiallyPaid)
                        && !i.IsDeleted)
            .OrderByDescending(i => i.InvoiceDate)
            .AsNoTracking()
            .ToListAsync();
    }
}