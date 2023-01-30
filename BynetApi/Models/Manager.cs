using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BynetApi.Models;


public class Manager
{
    [Key]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}
