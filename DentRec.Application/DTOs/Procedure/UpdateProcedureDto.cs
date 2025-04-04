namespace DentRec.Application.DTOs.Procedure
{
    public class UpdateProcedureDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Fee { get; set; }
    }
}
