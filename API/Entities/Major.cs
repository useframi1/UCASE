namespace API.Entities;

public partial class Major
{
    public string MajorName { get; set; }

    public string MajorRequirements { get; set; }

    public virtual ICollection<University> UniNames { get; set; } = new List<University>();
}
