using DentRec.Application.DTOs.PatientProcedure;
using DentRec.Application.Extensions;
using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using System.Linq.Expressions;

namespace DentRec.Application.Services
{
    public class PatientProcedureService(IRepository<PatientProcedure> patientProcedureRepo,
        IRepository<Patient> patientRepository, IRepository<Dentist> dentistRepository,
        IRepository<Procedure> procedureRepository) : IPatientProcedureService
    {
        #pragma warning disable CS8603  // Disable warning for possible null reference return.
        private static readonly Expression<Func<PatientProcedure, object>>[] includes =
        {
            x => x.Patient,
            x => x.Dentist,
            x => x.Procedure
        };
        #pragma warning restore CS8603

        public async Task<int> CreatePatientProcedureAsync(CreatePatientProcedureDto dto)
        {
            var patientExists = await patientRepository.ExistsAsync(dto.PatientId);
            var dentistExists = await dentistRepository.ExistsAsync(dto.DentistId);

            if (!patientExists) throw new KeyNotFoundException($"Patient with Id {dto.PatientId} does not exist.");
            if (!dentistExists) throw new KeyNotFoundException($"Dentist with Id {dto.DentistId} does not exist.");

            var procedure = await procedureRepository.GetByIdAsync(dto.ProcedureId) ?? 
                throw new KeyNotFoundException($"Procedure with Id {dto.ProcedureId} does not exist.");

            var newPatientProcedure = dto.ToEntity();
            newPatientProcedure.Cost = procedure.Cost; //Get current cost for procedure

            patientProcedureRepo.Add(newPatientProcedure);

            return await patientProcedureRepo.SaveAsync(newPatientProcedure);
        }


        public async Task<bool> DeletePatientProcedureAsync(int id)
        {
            var patientProcedure = await patientProcedureRepo.GetByIdAsync(id, includes)
                ?? throw new KeyNotFoundException($"Could not find PatientProcedure with Id: {id}");

            patientProcedureRepo.Remove(patientProcedure);

            return await patientProcedureRepo.SaveAsync(patientProcedure) > 0;
        }

        public async Task<GetPatientProcedureDto> GetPatientProcedureByIdAsync(int id)
        {
            var patientProcedure = await patientProcedureRepo.GetByIdAsync(id, includes)
                ?? throw new KeyNotFoundException($"Could not find PatientProcedure with Id: {id}");

            return patientProcedure.ToDto();
        }

        public async Task<Paging<GetPatientProcedureDto>> GetPatientProceduresAsync(GridifyQuery gridifyQuery)
        {

            var patientProcedures = await patientProcedureRepo.GetPaginatedRecordsAsync(gridifyQuery, includes);
            var result = new Paging<GetPatientProcedureDto>
            {
                Count = patientProcedures.Count,
                Data = patientProcedures.Data.Select(x => x.ToDto())
            };

            return result;
        }

        public async Task<int> UpdatePatientProcedureAsync(UpdatePatientProcedureDto dto)
        {
            var patientProcedure = await patientProcedureRepo.GetByIdAsync(dto.Id)
                    ?? throw new KeyNotFoundException($"Could not find PatientProcedure with Id: {dto.Id}");

            if (dto.PatientId.HasValue)
            {
                var patientExists = await patientRepository.ExistsAsync(dto.PatientId.Value);
                if (!patientExists)
                {
                    throw new KeyNotFoundException($"Patient with Id {dto.PatientId.Value} does not exist.");
                }
                patientProcedure.PatientId = dto.PatientId.Value;
            }
            if (dto.DentistId.HasValue)
            {
                var dentistExists = await dentistRepository.ExistsAsync(dto.DentistId.Value);
                if (!dentistExists)
                {
                    throw new KeyNotFoundException($"Dentist with Id {dto.DentistId.Value} does not exist.");
                }
                patientProcedure.DentistId = dto.DentistId.Value;
            }

            if (dto.ProcedureId.HasValue)
            {
                var procedureExists = await procedureRepository.ExistsAsync(dto.ProcedureId.Value);
                if (!procedureExists)
                {
                    throw new KeyNotFoundException($"Procedure with Id {dto.ProcedureId.Value} does not exist.");
                }
                patientProcedure.ProcedureId = dto.ProcedureId.Value;
            }
            if (!string.IsNullOrEmpty(dto.Notes)) patientProcedure.Notes = dto.Notes;
            if (dto.ProcedureDate.HasValue) patientProcedure.ProcedureDate = dto.ProcedureDate.Value;

            try
            {
                patientProcedureRepo.Update(patientProcedure);
                var result = await patientProcedureRepo.SaveAsync(patientProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the PatientProcedure.", ex);
            }
        }
    }
}
