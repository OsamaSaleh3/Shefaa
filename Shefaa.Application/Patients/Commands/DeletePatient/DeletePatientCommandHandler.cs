using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, ErrorOr<Success>>
{
    private readonly IPatientRepository _patientRepository;

    public DeletePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<Success>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            return Error.Validation(
                code: "PatientId",
                description: "Patient ID is required");
        }


        var patient = await _patientRepository.GetByIdAsync(request.Id);
        if (patient is null)
        {
            return Error.NotFound(code: "Patient.NotFound", description: $"Patient with ID {request.Id} not found");
        }

        patient.SoftDelete();
        await _patientRepository.UpdateAsync(patient);

        return Result.Success;
    }
       
    
}

