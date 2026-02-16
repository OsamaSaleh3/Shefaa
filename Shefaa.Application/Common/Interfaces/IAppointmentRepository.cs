using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> IsSlotBusyAsync(string doctorId, DateTime appointmentDate, int durationMinutes);
        Task<Guid> AddAsync(Appointment appointment);

        Task<Appointment?> GetByIdAsync(Guid id);
        Task UpdateAsync(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime From, DateTime To, string? DoctorId=null);
        Task<List<Appointment>> GetDoctorAppointmentsAsync(string DoctorId,DateTime Date);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid PatientId);

    }
}
