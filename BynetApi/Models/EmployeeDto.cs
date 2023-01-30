using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BynetApi.Models;

public partial class EmployeeDto
{
    [Required]
    [StringLength(9)]
    public string IdNumber { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string LastName { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = null!;
    public string? ManagerId { get; set; }
}
