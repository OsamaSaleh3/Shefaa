using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Patients;
using Shefaa.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shefaa.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ShefaaDbContext _dbContext;

        public PatientRepository(ShefaaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Patient patient)
        {
            await _dbContext.Patients.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _dbContext.Patients
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Patients
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task UpdateAsync(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();
        }
    }
}
