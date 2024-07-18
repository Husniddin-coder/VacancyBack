using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Service.DTOs.AuthDtos.AccountDtos;

public class ChangePasswordDto
{
    public int UserId { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmPasswrd { get; set; }
}
