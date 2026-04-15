using worksquare.Enum;

namespace worksquare.DTO
{
    public class CompanyDetailsDTO
    {
        public string? TaxIdentificationNumber { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? Industry { get; set; }
        public int CompanyId { get; set; }
        public CompanySizeEnum CompanySize { get; set; }

        //below fields are for future use
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string? ServicesOffered { get; set; }
    }
}
