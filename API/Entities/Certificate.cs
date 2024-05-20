using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Certificate
{
    public string Email { get; set; }

    public string CertificateName { get; set; }

    public byte[] CertificatePhoto { get; set; }

    public virtual Application EmailNavigation { get; set; }
}
