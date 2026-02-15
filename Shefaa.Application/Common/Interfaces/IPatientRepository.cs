using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IPatientRepository
    {
        Task CreateAsync(Patient Patient);
        Task<Patient?> GetByIdAsync(Guid Id);
        Task UpdateAsync(Patient patient);
        Task<List<Patient>> GetAllAsync();
    }
}
