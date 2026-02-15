using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Commands.DeletePatient
{
    public sealed record DeletePatientCommand(Guid Id):IRequest<ErrorOr<Success>>;
}
