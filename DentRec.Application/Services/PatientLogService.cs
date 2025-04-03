using DentRec.Application.DTOs.PatientLog;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using System.Linq.Expressions;

namespace DentRec.Application.Services
{
    public class PatientLogService(IRepository<PatientLog> patientLogRepo,
        IRepository<Patient> patientRepository, IRepository<Dentist> dentistRepository,
        IRepository<Procedure> procedureRepository) : IPatientLogService
    {
#pragma warning disable CS8603  // Disable warning for possible null reference return.
        private static readonly Expression<Func<PatientLog, object>>[] includes =
        {
            x => x.Patient,
            x => x.Dentist,
            x => x.Procedures
        };
#pragma warning restore CS8603

        public async Task<int> CreatePatientLogAsync(CreatePatientLogDto dto)
        {

            var patientExists = await patientRepository.ExistsAsync(dto.PatientId);
            var dentistExists = await dentistRepository.ExistsAsync(dto.DentistId);

            if (!patientExists) throw new KeyNotFoundException($"Patient with Id {dto.PatientId} does not exist.");
            if (!dentistExists) throw new KeyNotFoundException($"Dentist with Id {dto.DentistId} does not exist.");

            var totalProcedureFee = 0.0m;
            var newPatientLog = dto.ToEntity();

            foreach (var procedureId in dto.ProcedureIds)
            {
                var procedure = await procedureRepository.GetByIdAsync(procedureId) ??
                    throw new KeyNotFoundException($"Procedure with Id {procedureId} does not exist.");

                newPatientLog.Procedures.Add(procedure);
                totalProcedureFee += procedure.Fee;
            }
            newPatientLog.Fee = totalProcedureFee;
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
