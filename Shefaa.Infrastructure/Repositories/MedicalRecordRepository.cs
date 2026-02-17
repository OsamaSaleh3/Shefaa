using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Infrastructure.Common.Persistence;

namespace Shefaa.Infrastructure.Repositories;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly ShefaaDbContext _context;

    public MedicalRecordRepository(ShefaaDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(MedicalRecord medicalRecord)
    {
        await _context.MedicalRecords.AddAsync(medicalRecord);
        await _context.SaveChangesAsync();
        return medicalRecord.Id;
    }

    public async Task UpdateAsync(MedicalRecord medicalRecord)
    {
        _context.MedicalRecords.Update(medicalRecord);
        await _context.SaveChangesAsync();
    }

    public async Task<MedicalRecord?> GetAppointmentMedicalRecord(Guid AppointmentId)
    {
        return await _context.MedicalRecords
            .Include(m => m.Doctor)
            .Include(m => m.Patient)
            .Include(m => m.Prescriptions) 
            .FirstOrDefaultAsync(a => a.AppointmentId == AppointmentId);
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid Id)
    {
        return await _context.MedicalRecords
           .Include(m => m.Doctor)
           .Include(m => m.Patient)
           .Include(m => m.Prescriptions) 
           .FirstOrDefaultAsync(a => a.Id == Id);
    }

    public async Task<List<MedicalRecord>> GetPatientMedicalRecords(Guid PatientId)
    {
        return await _context.MedicalRecords
           .AsNoTracking() 
           .Include(m => m.Doctor)
           .Include(m => m.Patient) 
           .Include(m => m.Prescriptions)
           .Where(a => a.PatientId == PatientId)
           .OrderByDescending(a => a.VisitDate) 
           .ToListAsync();
    }
}