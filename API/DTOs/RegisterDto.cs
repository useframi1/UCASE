﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
