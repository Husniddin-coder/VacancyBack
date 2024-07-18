using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Service.DTOs.AuthDtos.RoleDtos;

public class RoleGetDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool isDefault { get; set; }
}
