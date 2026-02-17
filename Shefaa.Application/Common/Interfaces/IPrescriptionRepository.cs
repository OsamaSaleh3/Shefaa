using Shefaa.Domain.Prescriptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task AddAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        Task<Prescription?> GetByIdWithMedicationsAsync(Guid id);
        Task<List<Prescription>> GetByPatientIdAsync(Guid patientId);
        Task<List<Prescription>> GetByMedicalRecordIdAsync(Guid medicalRecordId);
        Task<Prescription?> GetByIdAsync(Guid id);
    }
}
