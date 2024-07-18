namespace Vacancy.Service.DTOs.AuthDtos.RoleDtos;

public class AssignPermissionsIntoRoleDto
{
    public int RoleId { get; set; }

    public int[] PermissionIds { get; set; }
}
