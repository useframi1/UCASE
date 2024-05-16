namespace API.DTOs;

public class MemberDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly? Dob { get; set; }

    public string PhoneNo { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string Nationality { get; set; }

    public char? Gender { get; set; }

    public string GovName { get; set; }

    public string Area { get; set; }

    public List<FavoriteUniDto> FavoriteUniversities { get; set; } = [];

    public List<PreferredIndustryDto> PreferredIndustries { get; set; } = [];

    public List<PreferredSubjectDto> PreferredSubjects { get; set; } = [];
}
