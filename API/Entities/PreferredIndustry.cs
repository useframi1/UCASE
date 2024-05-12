using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class PreferredIndustry
{
    public string Email { get; set; }

    public string Industry { get; set; }

    public virtual User EmailNavigation { get; set; }
}
