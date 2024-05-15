using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Governorate
{
    public string GovName { get; set; }

    public string Area { get; set; }

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
