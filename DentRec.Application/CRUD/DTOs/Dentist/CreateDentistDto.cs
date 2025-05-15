namespace DentRec.Application.CRUD.DTOs.Dentist
{
    public class CreateDentistDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
