using DentRec.Application.CRUD.DTOs.Prescription;
using DentRec.Domain.Entities;

namespace DentRec.Application.CRUD.Extensions
{
    public static class PrescriptionExtensions
    {
        public static GetPrescriptionDto ToDto(this Prescription prescription)
        {
            return new GetPrescriptionDto
            {
                Id = prescription.Id,
                Name = prescription.Name,
                Description = prescription.Description,
                Dosage = prescription.Dosage,
                Instructions = prescription.Instructions,
                CreatedBy = prescription.CreatedBy,
                ModifiedOn = prescription.ModifiedOn,
                CreatedOn = prescription.CreatedOn,
                ModifiedBy = prescription.ModifiedBy
            };
        }

        public static Prescription ToEntity(this CreatePrescriptionDto dto)
        {
            return new Prescription
            {
                Name = dto.Name,
                Description = dto.Description,
                Dosage = dto.Dosage,
                Instructions = dto.Instructions,
            };
        }
    }
}
