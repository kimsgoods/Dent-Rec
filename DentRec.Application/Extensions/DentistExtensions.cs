using DentRec.Application.DTOs.Dentist;
using DentRec.Domain.Entities;

namespace DentRec.Application.Extensions
{
    public static class DentistExtensions
    {
        public static GetDentistDto ToDto(this Dentist dentist)
        {
            return new GetDentistDto
            {
                Id = dentist.Id,
                FirstName = dentist.FirstName,
                LastName = dentist.LastName,
                Email = dentist.Email,
                Phone = dentist.Phone,
                CreatedBy = dentist.CreatedBy,
                ModifiedOn = dentist.ModifiedOn,
                CreatedOn = dentist.CreatedOn,
                ModifiedBy = dentist.ModifiedBy
            };
        }

        public static Dentist ToEntity(this CreateDentistDto dto)
        {
            return new Dentist
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone
            };
        }
    }
}
