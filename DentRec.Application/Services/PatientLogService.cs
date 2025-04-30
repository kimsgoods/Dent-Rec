using DentRec.Application.DTOs.PatientLog;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DentRec.Application.Services
{
    public class PatientLogService(IRepository<PatientLog> patientLogRepo,
        IRepository<Patient> patientRepository, IRepository<Dentist> dentistRepository,
        IRepository<Procedure> procedureRepository) : IPatientLogService
    {

        private readonly Func<IQueryable<PatientLog>, IQueryable<PatientLog>> includes = 
            x => x.Include(p => p.Patient)
                  .Include(p => p.Dentist)
                  .Include(p => p.PatientLogProcedures)
                    .ThenInclude(plp => plp.Procedure)
                  .Include(p => p.Payments);

        public async Task<int> CreatePatientLogAsync(CreatePatientLogDto dto)
        {

            var patient = await patientRepository.GetByIdAsync(dto.PatientId) ??
                throw new KeyNotFoundException($"Patient with Id {dto.PatientId} does not exist."); 

            var dentistExists = await dentistRepository.ExistsAsync(dto.DentistId);            
            if (!dentistExists) throw new KeyNotFoundException($"Dentist with Id {dto.DentistId} does not exist.");

            var totalProcedureFee = 0.0m;
            var newPatientLog = dto.ToEntity();

            foreach (var inputProcedure in dto.Procedures)
            {
                var procedure = await procedureRepository.GetByIdAsync(inputProcedure.Id) ??
                    throw new KeyNotFoundException($"Procedure with Id {inputProcedure.Id} does not exist.");

                var newPatientLogProcedure = new PatientLogProcedure
                {
                    ProcedureId = procedure.Id,
                    Procedure = procedure,
                    Notes = inputProcedure.Notes,
                    Quantity = inputProcedure.Quantity ?? 1,                    
                };
                newPatientLogProcedure.CalculatedAdjustedFee();
                newPatientLog.PatientLogProcedures.Add(newPatientLogProcedure);
                totalProcedureFee += newPatientLogProcedure.AdjustedFee;
            }
            newPatientLog.Fee = totalProcedureFee;
            newPatientLog.PatientAge = patient.Age;
            patientLogRepo.Add(newPatientLog);

            return await patientLogRepo.SaveAsync(newPatientLog);
        }


        public async Task<bool> DeletePatientLogAsync(int id)
        {
            var patientLog = await patientLogRepo.GetByIdAsync(id, includes)
                ?? throw new KeyNotFoundException($"Could not find PatientLog with Id: {id}");

            patientLogRepo.Remove(patientLog);

            return await patientLogRepo.SaveAsync(patientLog) > 0;
        }

        public async Task<GetPatientLogDetailsDto> GetPatientLogByIdAsync(int id)
        {
            var patientLog = await patientLogRepo.GetByIdAsync(id, includes)
                ?? throw new KeyNotFoundException($"Could not find PatientLog with Id: {id}");

            return patientLog.ToDetailsDto();
        }

        public async Task<Paging<GetPatientLogDto>> GetPatientLogsAsync(GridifyQuery gridifyQuery)
        {

            var patientLogs = await patientLogRepo.GetPaginatedRecordsAsync(gridifyQuery, includes);
            var result = new Paging<GetPatientLogDto>
            {
                Count = patientLogs.Count,
                Data = patientLogs.Data.Select(x => x.ToDto())
            };

            return result;
        }

        public async Task<int> UpdatePatientLogAsync(UpdatePatientLogDto dto)
        {
            var patientLog = await patientLogRepo.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find PatientLog with Id: {dto.Id}");

            if (dto.PatientId.HasValue)
            {
                var patientExists = await patientRepository.ExistsAsync(dto.PatientId.Value);
                if (!patientExists)
                {
                    throw new KeyNotFoundException($"Patient with Id {dto.PatientId.Value} does not exist.");
                }
                patientLog.PatientId = dto.PatientId.Value;
            }
            if (dto.DentistId.HasValue)
            {
                var dentistExists = await dentistRepository.ExistsAsync(dto.DentistId.Value);
                if (!dentistExists)
                {
                    throw new KeyNotFoundException($"Dentist with Id {dto.DentistId.Value} does not exist.");
                }
                patientLog.DentistId = dto.DentistId.Value;
            }

            if (!string.IsNullOrEmpty(dto.Notes)) patientLog.Notes = dto.Notes;
            if (dto.ProcedureDate.HasValue) patientLog.ProcedureDate = dto.ProcedureDate.Value;

            try
            {
                patientLogRepo.Update(patientLog);
                var result = await patientLogRepo.SaveAsync(patientLog);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the PatientLog.", ex);
            }
        }
    }
}
