using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Appointments;
using Shefaa.Domain.Appointments.enums;
using Shefaa.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shefaa.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly ShefaaDbContext _context;

        public AppointmentRepository(ShefaaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
             _context.Appointments.Update(appointment);
             await _context.SaveChangesAsync();
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a=>a.Patient)
                .Include(a=>a.Doctor)
                .FirstOrDefaultAsync(a=>a.Id == id);
        }

        public async Task<bool> IsSlotBusyAsync(string doctorId, DateTime appointmentDate, int durationMinutes)
        {
            var newAppointmentDate = appointmentDate;
            var newAppointmentEndDate = appointmentDate.AddMinutes(durationMinutes);

            return  await _context.Appointments.AnyAsync(a =>
            a.DoctorId == doctorId &&
                a.Status != AppointmentStatus.Cancelled &&

                a.AppointmentDate < newAppointmentEndDate &&
                a.AppointmentDate.AddMinutes(a.DurationMinutes) > newAppointmentDate

                );
        }

        public async Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime From, DateTime To, string? DoctorId)
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a=>a.AppointmentDate >= From && a.AppointmentDate <= To);

            if (!string.IsNullOrEmpty(DoctorId))
            {
                appointments = appointments.Where(a => a.DoctorId == DoctorId);
            }

            return await appointments.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid PatientId)
        {
            return await _context.Appointments
                 .Include(a => a.Doctor)
                 .Where(a => a.PatientId == PatientId)
                 .OrderByDescending(a => a.AppointmentDate)
                 .ToListAsync();
        }

      

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(string DoctorId, DateTime Date)
        {
            return await _context.Appointments
                .Include(a=>a.Patient)
                .Where(a =>
                    a.DoctorId==DoctorId &&
                    a.AppointmentDate.Date==Date.Date
                )
                .OrderBy(a=>a.AppointmentDate)
                .ToListAsync();
        }

      

       
    }
}
