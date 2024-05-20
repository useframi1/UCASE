namespace API.DTOs;

public class UniversityDto
{
    public string UniName { get; set; }

    public string Description { get; set; }

    public int? Ranking { get; set; }

    public double? AcceptanceRate { get; set; }

    public string Link { get; set; }

    public string GovName { get; set; }

    public string Area { get; set; }

    public string GeneralRequirements { get; set; }

    public byte[] Logo { get; set; }
}
