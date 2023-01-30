using System.ComponentModel.DataAnnotations;

namespace BynetApi.Models;

public partial class V_Employee
{
    [Key]
    public string IdNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? ManagerId { get; set; }
    public string? ManagerName { get; set; }
}
