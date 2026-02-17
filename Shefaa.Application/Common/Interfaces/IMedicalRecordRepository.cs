using Shefaa.Domain.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<Guid> AddAsync(MedicalRecord medicalRecord);
        Task<MedicalRecord?> GetByIdAsync(Guid Id);
        Task UpdateAsync(MedicalRecord medicalRecord);
        Task<List<MedicalRecord>> GetPatientMedicalRecords(Guid PatientId);
        Task<MedicalRecord?> GetAppointmentMedicalRecord(Guid AppointmentId);

    }
}
