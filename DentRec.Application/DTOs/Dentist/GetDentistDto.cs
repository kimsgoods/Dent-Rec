namespace DentRec.Application.DTOs.Dentist
{
    public class GetDentistDto : AuditFields
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
