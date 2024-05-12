using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class PreferredSubject
{
    public string Email { get; set; }

    public string Subject { get; set; }

    public virtual User EmailNavigation { get; set; }
}
