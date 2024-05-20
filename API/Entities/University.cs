using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class University
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

    public virtual ICollection<User> Emails { get; set; } = new List<User>();

    public virtual ICollection<Application> EmailsNavigation { get; set; } = new List<Application>();
}
