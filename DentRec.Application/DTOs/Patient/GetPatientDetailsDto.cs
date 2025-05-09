﻿using DentRec.Application.DTOs.PatientLog;
using DentRec.Application.DTOs.Payments;

namespace DentRec.Application.DTOs.Patient
{
    public class GetPatientDetailsDto : AuditFields
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public List<GetPaymentDto> Payments { get; set; } = [];
        public List<GetPatientLogDto> PatientLogs { get; set; } = [];

    }
}
