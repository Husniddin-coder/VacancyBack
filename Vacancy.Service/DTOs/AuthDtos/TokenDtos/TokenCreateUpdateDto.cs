using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Service.DTOs.AuthDtos.TokenDtos;

public class TokenCreateUpdateDto
{
    public int UserId { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiredDateOfRefreshToken { get; set; }
}
