namespace API.DTOs;

public class DetailsDto
{
    public string Email { get; set; }

    public DateOnly? Dob { get; set; }

    public char? Gender { get; set; }

    public string Phoneno { get; set; }

    public string Nationality { get; set; }

    public int? StartUni { get; set; }
}
