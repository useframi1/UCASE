using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class FavoriteUniversity
{
    public string Email { get; set; }

    public string University { get; set; }

    public virtual User EmailNavigation { get; set; }
}
