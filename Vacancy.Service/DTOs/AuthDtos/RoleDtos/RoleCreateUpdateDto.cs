using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Service.DTOs.AuthDtos.RoleDtos;

public class RoleCreateUpdateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool isDefault { get; set; }
}
