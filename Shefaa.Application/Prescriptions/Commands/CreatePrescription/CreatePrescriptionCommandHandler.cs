using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Prescriptions;

namespace Shefaa.Application.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, ErrorOr<Guid>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _petientRepository;

    public CreatePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository, IMedicalRecordRepository medicalRecordRepository, IUserRepository userRepository, IPatientRepository petientRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _medicalRecordRepository = medicalRecordRepository;
        _userRepository = userRepository;
        _petientRepository = petientRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var doctor =await _userRepository.GetByIdAsync(request.DoctorId);
        if (doctor is null)
            return Error.NotFound($"Doctor with Id:{request.DoctorId} not found");

        var patient = await _petientRepository.GetByIdAsync(request.PatientId);
        if (patient is null)
            return Error.NotFound($"Patient with Id:{request.PatientId} not found");

        var medicalRecord=await _medicalRecordRepository.GetByIdAsync(request.MedicalRecordId);
        if(medicalRecord is null)
            return Error.NotFound($"Medical record with Id:{request.MedicalRecordId} not found");

        var prescriptionResult = Prescription.Create(request.MedicalRecordId, request.PatientId, request.DoctorId, request.Notes);

        if(prescriptionResult.IsError)
            return prescriptionResult.Errors;

        var prescription = prescriptionResult.Value;
        await _prescriptionRepository.AddAsync(prescription);
        return prescription.Id;
    }
}
