using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.CQRS.PatientLogs.Commands.CreatePatientLog
{
    public class CreatePatientLogCommandHandler(
        IExtendedRepository<PatientLog> patientLogRepository,
        IExtendedRepository<Patient> patientRepository,
        IExtendedRepository<Dentist> dentistRepository,
        IExtendedRepository<Procedure> procedureRepository,
        ILogger<CreatePatientLogCommandHandler> logger
    ) : IRequestHandler<CreatePatientLogCommand, int>
    {
        public async Task<int> Handle(CreatePatientLogCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Handling CreatePatientLogCommand: PatientId:{PatientId}, DentistId:{DentistId}, Procedures:{ProcedureIds}", request.PatientId, request.DentistId, string.Join(",", request.Procedures.Select(x => x.Id)));

            var patient = await patientRepository.GetByIdAsync(request.PatientId);
            if (patient is null)
            {
                logger.LogError("Patient with Id {PatientId} does not exist", request.PatientId);
                throw new KeyNotFoundException($"Patient with Id {request.PatientId} does not exist.");
            }

            var dentistExists = await dentistRepository.ExistsAsync(request.DentistId);
            if (!dentistExists)
            {
                logger.LogError("Dentist with Id {DentistId} does not exist.", request.DentistId);
                throw new KeyNotFoundException($"Dentist with Id {request.DentistId} does not exist.");
            }

            try
            {
                var totalProcedureFee = 0.0m;
                var newPatientLog = new PatientLog
                {
                    PatientId = request.PatientId,
                    DentistId = request.DentistId,
                    ProcedureDate = DateTime.Now,
                    Notes = request.Notes,
                    PaymentStatus = "Pending"
                };

                foreach (var inputProcedure in request.Procedures)
                {
                    var procedure = await procedureRepository.GetByIdAsync(inputProcedure.Id);
                    if (procedure is null)
                    {
                        logger.LogWarning("Procedure with Id {ProcedureId} does not exist.", inputProcedure.Id);
                        throw new KeyNotFoundException($"Procedure with Id {inputProcedure.Id} does not exist.");
                    }

                    var newPatientLogProcedure = new PatientLogProcedure
                    {
                        ProcedureId = procedure.Id,
                        Procedure = procedure,
                        Notes = inputProcedure.Notes,
                        Quantity = inputProcedure.Quantity ?? 1,
                    };
                    newPatientLogProcedure.CalculateAdjustedFee();
                    newPatientLog.PatientLogProcedures.Add(newPatientLogProcedure);
                    totalProcedureFee += newPatientLogProcedure.AdjustedFee;
                }

                newPatientLog.Fee = totalProcedureFee;
                newPatientLog.PatientAge = patient.Age;

                patientLogRepository.Add(newPatientLog);

                var result = await patientLogRepository.SaveAsync(newPatientLog);

                logger.LogInformation("Handled CreatePatientLogCommand. Create new log with Id:{newLogId}", result);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating PatientLog for PatientId: {PatientId}", request.PatientId);
                throw new ApplicationException("An error occurred while creating the PatientLog.", ex);
            }


        }
    }
}
