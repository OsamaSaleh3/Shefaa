using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.MedicalRecords;

namespace Shefaa.Application.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, ErrorOr<Guid>>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IPatientRepository _patientRepository; 
    private readonly IUserRepository _userRepository;
    public CreateMedicalRecordCommandHandler(IMedicalRecordRepository medicalRecordRepository, IPatientRepository patientRepository, IUserRepository userRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _patientRepository = patientRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
        if(doctor is null)
        {
            return Error.NotFound($"Doctor with Id :{request.DoctorId} Not Dound");
        }
        var patient=await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient is null)
        {
            return Error.NotFound($"Patient with id: {request.PatientId} not found");
        }

        var medicalRecordResult = MedicalRecord.Create(request.PatientId,
                                                       request.DoctorId,
                                                       request.ChiefComplaint,
                                                       request.Symptoms,
                                                       request.Diagnosed,
                                                       request.AppointmentId);
        if (medicalRecordResult.IsError)
        {
            return medicalRecordResult.Errors;
        }

        var medicalRecord=medicalRecordResult.Value;

        medicalRecord.UpdateVitals(request.BloodPressure,
                                   request.Temperature,
                                   request.Pulse,
                                   request.RespiratoryRate,
                                   request.Weight,
                                   request.Height);

        await _medicalRecordRepository.AddAsync(medicalRecord);
        return medicalRecord.Id;
    }
}
