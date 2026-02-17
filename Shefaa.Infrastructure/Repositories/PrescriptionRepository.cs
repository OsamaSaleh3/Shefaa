using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Prescriptions;
using Shefaa.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shefaa.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ShefaaDbContext _dbContext;

        public PrescriptionRepository(ShefaaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Prescription prescription)
        {
            await _dbContext.Prescriptions.AddAsync(prescription);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prescription prescription)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Prescription?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Prescriptions
                .Include(p=>p.Doctor)
                .Include(p=>p.Patient)
                .Include(p => p.MedicalRecord)
                .Include(p => p.PrescriptionMedications)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Prescription?> GetByIdWithMedicationsAsync(Guid id)
        {
            return await _dbContext.Prescriptions
                .Include(p => p.PrescriptionMedications)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<List<Prescription>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbContext.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.PrescriptionMedications)
                .Where(p => p.PatientId == patientId && !p.IsDeleted)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<List<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId)
        {
            return await _dbContext.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.PrescriptionMedications)
                .Where(p => p.MedicalRecordId == medicalRecordId && !p.IsDeleted)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }
    }
}
