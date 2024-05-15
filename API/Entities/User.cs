using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class User
{
    public string Email { get; set; }

    public byte[] Passwordhash { get; set; }

    public byte[] Passwordsalt { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly? Dob { get; set; }

    public string Phoneno { get; set; }

    public string Addressline1 { get; set; }

    public string Addressline2 { get; set; }

    public string Nationality { get; set; }

    public char? Gender { get; set; }

    public string GovName { get; set; }

    public string Area { get; set; }

    public int? StartUni { get; set; }

    public virtual ICollection<FavoriteUniversity> FavoriteUniversities { get; set; } = new List<FavoriteUniversity>();

    public virtual ICollection<PreferredIndustry> PreferredIndustries { get; set; } = new List<PreferredIndustry>();

    public virtual ICollection<PreferredSubject> PreferredSubjects { get; set; } = new List<PreferredSubject>();
}
