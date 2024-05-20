namespace API.DTOs;

public class ApplicationDto
{
    public string Email { get; set; }

    public byte[] NationalId { get; set; }

    public byte[] Passport { get; set; }

    public string GuardianName { get; set; }

    public string GuardianProfession { get; set; }

    public string GuardianCompany { get; set; }

    public string GuardianNumber { get; set; }

    public string GuardianEmail { get; set; }

    public string SchoolCountry { get; set; }

    public string SchoolCity { get; set; }

    public string SchoolName { get; set; }

    public int? YearOfGraduation { get; set; }

    public byte[] BirthCertificate { get; set; }

    public byte[] Transcript { get; set; }

    public byte[] RecommendationLetter { get; set; }

    public byte[] PersonalPhoto { get; set; }

    public byte[] MilitaryForm2 { get; set; }

    public byte[] MilitaryForm6 { get; set; }

    public byte[] ResidencyCopy { get; set; }

    public byte[] PersonalStatement { get; set; }

    public List<CertificateDto> Certificates { get; set; } = [];

    public List<UniversityDto> UniversityChoices { get; set; } = [];
}
