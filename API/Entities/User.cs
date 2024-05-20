using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class User
{
    public string Email { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

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

    public int? StartUni { get; set; }

    public virtual Application Application { get; set; }

    public virtual ICollection<PreferredIndustry> PreferredIndustries { get; set; } = new List<PreferredIndustry>();

    public virtual ICollection<PreferredSubject> PreferredSubjects { get; set; } = new List<PreferredSubject>();

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
