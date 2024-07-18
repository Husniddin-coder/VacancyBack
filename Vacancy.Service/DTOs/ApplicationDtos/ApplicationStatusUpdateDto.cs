using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Domain.Enums.Applications;

namespace Vacancy.Service.DTOs.ApplicationDtos;

public class ApplicationStatusUpdateDto
{
    public int id { get; set; }

    public Status Status { get; set; }
}
